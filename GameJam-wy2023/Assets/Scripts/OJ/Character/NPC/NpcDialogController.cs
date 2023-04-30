using System.Collections;
using System.Collections.Generic;
using Dye;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class NpcDialogController : MonoBehaviour, IInteractObject
    {
        [Header("NPC")]
        public int npcId = 0;
        Npc npc;
        List<NpcDialog> dialogs;
        int dialogId = 0;
        bool interactable;
        DialogMsg dialogMsg;
        InteractMsg interactMsg;
        protected void Start()
        {
            npc = DataManager.configs.TbNpc.Get(npcId);
            dialogs = npc.Dialogs;
            interactable = dialogs!=null && dialogs.Count > 0;
            dialogMsg = new DialogMsg();
            interactMsg = new InteractMsg(){
                tip = npc.Name, interactObject = this
            };
        }
        
        private void OnTriggerEnter(Collider other) {
            if(other.tag.Equals("Player")){
                interactMsg.active = true;
                UIManager.Handle(UIEvent.SHOW_INTERACT_TIP, interactMsg);
            }
        }
        private void OnTriggerExit(Collider other) {
            if(other.tag.Equals("Player")){
                interactMsg.active = false;
                UIManager.Handle(UIEvent.SHOW_INTERACT_TIP, interactMsg);
            }
        }
        // protected void Update()
        // {
        //     if(interactable && interacting && Input.GetKeyDown(KeyCode.F)){
        //         StartDialog();
        //     }
        // }
        public void Interact(){
            if(interactable){
                StartDialog();
            }
        }
        void StartDialog(){
            if(dialogId >= dialogs.Count){
                return;
            }
            NpcDialog dialog = dialogs[dialogId];

            if(!string.IsNullOrEmpty(dialog.Effect.Trim())){
                string[] effects = dialog.Effect.Split("|");
                TakeEffect(effects[0], int.Parse(effects[1]), int.Parse(effects[2]));
            }

            switch(dialog.Type){
                case "对话":{
                    dialogMsg.speaker = npc.Name;
                    dialogMsg.content = dialog.Content;
                    dialogMsg.options.Clear();
                    int nextId = dialog.Next;
                    if(dialogs[nextId].Type == "选项"){
                        while(nextId<dialogs.Count && dialogs[nextId].Type == "选项"){
                            dialogMsg.options.Add(dialogs[nextId].Content);
                            nextId++;
                        }
                    }
                    dialogMsg.callback = OnChooseOption;
                    UIManager.Handle(UIEvent.SHOW_DIALOG, dialogMsg);
                    break;
                }
                case "选项":{
                    dialogId = dialog.Next;
                    break;
                }
                case "Hide":{
                    dialogId = dialog.Next;
                    break;
                }
                case "End":{
                    interactable = false;
                    break;
                }
                case "交付":{
                    DeliveryMission();
                    break;
                }
                case "谜题":{
                    RiddleMission();
                    break;
                }
            }
        }

        void OnChooseOption(string content){
            NpcDialog dialog = dialogs[dialogId];
            int nextId = dialog.Next;
            if(dialogs[nextId].Type == "选项"){
                while(nextId<dialogs.Count && dialogs[nextId].Type == "选项"){
                    if(dialogs[nextId].Content == content){
                        dialogId = dialogs[nextId].Next;
                        StartDialog();
                        break;
                    }
                    nextId++;
                }
            }
            else if(string.IsNullOrEmpty(content)){
                dialogId = dialogs[dialogId].Next;
                StartDialog();
            }
        }
        void DeliveryMission(){
            NpcDialog dialog = dialogs[dialogId];
            Debug.Log("交付任务<"+dialog.Content+">");

            string[] conditions = dialog.Condition.Split("|");
            //检查 {} 数目是否足够
            var items = Bagdata._listBag;
            int count = 0;
            foreach(var item in items){
                if(item.Id.ToString() == conditions[1]){
                    count ++;
                }
            }
            bool complete = false;
            if(count > int.Parse(conditions[2])){
                complete = true;
                for(int i=items.Count-1; i>=0; i--){
                    if(items[i].Id.ToString() == conditions[1]){
                        count--;
                        items.RemoveAt(i);
                    }
                    if(count <= 0){
                        break;
                    }
                }
            }

            //...
            if(!complete){//next --> 失败
                dialogId = dialog.Next;
                StartDialog();
            }
            else{//next+1 --> 成功
                dialogId = dialog.Next + 1;
                StartDialog();
            }
        }
        void RiddleMission(){
            NpcDialog dialog = dialogs[dialogId];
            //Debug.Log("解谜任务<"+dialog.Content+">");
            RiddleMsg riddleMsg = new RiddleMsg();
            riddleMsg.riddleId = int.Parse(dialog.Content);
            riddleMsg.onResult = OnRiddleResult;
            UIManager.Handle(UIEvent.SHOW_RIDDLE_PANEL, riddleMsg);
        }

        void OnRiddleResult(bool result){
            NpcDialog dialog = dialogs[dialogId];
            if(!result){//next --> 失败
                dialogId = dialog.Next;
                StartDialog();
            }
            else{//next+1 --> 成功
                dialogId = dialog.Next + 1;
                StartDialog();
            }
        }

        void TakeEffect(string type, int id, int count = 1){
            if(type == "物品"){
                for(int i=0;i<count;i++)
                    Bagdata._listBag.Add(DataManager.configs.TbItem.Get(id));
            }
        }
    }
}
