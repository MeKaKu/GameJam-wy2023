using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace OJ
{
    public class DialogPanel : PanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            Bind(
                UIEvent.SHOW_DIALOG
            );
        }
        public override void Execute(int eventCode, object arg)
        {
            switch(eventCode){
                case UIEvent.SHOW_DIALOG:{
                    Show(arg as DialogMsg);
                    break;
                }
            }
        }
        void Start(){
            Hide();
        }

        void Update(){
            
        }

        public void Show(DialogMsg dialogMsg){
            if(dialogMsg == null){
                Debug.LogError("dialogMsg is null");
                return;
            }
            Show();
            GetCom<Text>("Text_Speaker").text = dialogMsg.speaker;
            GetCom<Text>("Text_Content").text = dialogMsg.content;
            for(int i=0;i<3;i++){
                Button btn = GetCom<Button>("Btn_Option_" + i);
                if(i<dialogMsg.options.Count){
                    btn.GetComponentInChildren<Text>().text = dialogMsg.options[i];
                }
                else{
                    btn.gameObject.SetActive(false);
                }
            }
        }

        protected override void OnClick(string name)
        {
            Button btn = GetCom<Button>(name);
            Debug.Log(btn.GetComponentInChildren<Text>().text);
            Hide();
        }
    }

    public class DialogMsg{
        public string speaker = "";
        public string content = "";
        public List<string> options = new List<string>();
    }
}
