using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    [RequireComponent(typeof(Collider))]
    public class Flow_3 : GameFlowBase
    {
        private void Awake() {
            flowId = 3;
        }
        private void OnTriggerEnter(Collider other) {
            if(other.tag.Equals("Player")){
                TryProcess();
            }
        }

        protected override void Process()
        {
            //屏幕中间显示文字
            string tip = "欢迎来到我的世界";
            UIManager.Handle(UIEvent.SHOW_SCREEN_TIP, tip);
            Invoke("ShowDialog", 1.5f);
        }

        void ShowDialog(){
            DialogMsg dialogMsg = new DialogMsg(){
                speaker = DataManager.gameData.playerName,
                content = "为什么这个声音让我感受到仿佛来自于我的内心深处，为什么这个世界让我既感觉熟悉又感觉扭曲和陌生。",
                callback = (s)=>{
                    ProcessEnd();
                }
            };
            UIManager.Handle(UIEvent.SHOW_DIALOG, dialogMsg);
        }
    }
}
