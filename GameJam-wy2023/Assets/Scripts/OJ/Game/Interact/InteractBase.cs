using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class InteractBase : MonoBehaviour, IInteractObject
    {
        public int interactId = 0;
        public string interactTip = "";
        protected bool interactable = true;
        protected InteractMsg interactMsg = new InteractMsg();
        void Start() {
            Init(DataManager.gameData.completedInteractions.Contains(interactId));
        }
        public virtual void Init(bool completed){

        }
        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag == "Player"){
                Trigger();
            }
        }
        private void OnTriggerExit(Collider other) {
            if(other.gameObject.tag == "Player"){
                CancelTrigger();
            }
        }
        public void Trigger(){
            if(interactable && !DataManager.gameData.completedInteractions.Contains(interactId)){
                interactMsg.interactObject = this;
                interactMsg.tip = interactTip;
                interactMsg.active = true;
                UIManager.Handle(UIEvent.SHOW_INTERACT_TIP, interactMsg);
            }
        }
        public void CancelTrigger(){
            interactMsg.active = false;
            UIManager.Handle(UIEvent.SHOW_INTERACT_TIP, interactMsg);
        }
        public virtual void Interact()
        {
            
        }

        public void Complete(){
            interactable = false;
            if(!DataManager.gameData.completedInteractions.Contains(interactId)){
                DataManager.gameData.completedInteractions.Add(interactId);
            }
        }
    }
}
/*
1 | 门
2 | 电闸
3 | 画
*/