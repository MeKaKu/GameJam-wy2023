using System.Collections;
using System.Collections.Generic;
using Dye;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class NpcDialogController : MonoBehaviour
    {
        [Header("NPC")]
        public int npcId = 0;
        Npc npc;
        List<NpcDialog> dialogs;
        int dialogId = 0;
        bool interactable;
        DialogMsg dialogMsg;
        protected void Start()
        {
            npc = DataManager.configs.TbNpc.Get(npcId);
            dialogs = npc.Dialogs;
            interactable = dialogs!=null && dialogs.Count > 0;
            dialogMsg = new DialogMsg();

            GetComponentInChildren<TextMesh>().text = npc.Name;
        }
        bool interacting;
        private void OnTriggerEnter(Collider other) {
            Debug.Log(other.name);
            if(other.tag.Equals("Player")){
                interacting = true;
            }
        }
        private void OnTriggerExit(Collider other) {
            if(other.tag.Equals("Player")){
                interacting = false;
            }
        }
        protected void Update()
        {
            if(interactable && interacting && Input.GetKeyDown(KeyCode.F)){
                StartDialog();
            }
        }

        void StartDialog(){
            if(dialogId >= dialogs.Count){
                return;
            }
            NpcDialog dialog = dialogs[dialogId];
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
            bool complete = false;
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
    }
}
