using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using DG.Tweening;

namespace OJ
{
    [RequireComponent(typeof(Collider))]
    public class Flow_2 : GameFlowBase
    {
        [SerializeField]FishController fish;
        private void OnTriggerExit(Collider other) {
            Debug.Log("Trigger");
            if(other.tag.Equals("Player")){
                TryProcess();
            }
        }

        protected override void Process()
        {
            DialogMsg dialogMsg = new DialogMsg();
            dialogMsg.speaker = "???";
            dialogMsg.content = "你已经被赋予了附身的能力，并且必须调查清楚灵魂之石才能离开这里。";
            dialogMsg.callback = Process_1;
            UIManager.Handle(UIEvent.SHOW_DIALOG, dialogMsg);
        }

        void Process_1(string opt){
            DialogMsg dialogMsg = new DialogMsg();
            dialogMsg.speaker = "???";
            dialogMsg.content = "你现在附身到它身上游到对岸";
            dialogMsg.callback = Process_2;

            ViewCamera viewCamera = FindObjectOfType<ViewCamera>();
            viewCamera.transform.DOLookAt(fish.transform.position, 1f).OnComplete(()=>{
                UIManager.Handle(UIEvent.SHOW_DIALOG, dialogMsg);
            });
            
        }
        void Process_2(string opt){
            ProcessEnd();
        }
    }
}
