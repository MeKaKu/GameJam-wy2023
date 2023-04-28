using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace OJ
{
    public class InteractTipPanel : PanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            Bind(
                UIEvent.SHOW_INTERACT_TIP
            );
        }
        public override void Execute(int eventCode, object arg)
        {
            if(eventCode == UIEvent.SHOW_INTERACT_TIP){
                if(arg as InteractMsg == null){
                    return;
                }
                Show(arg as InteractMsg);
            }
        }
        IInteractObject interactObject;
        public void Show(InteractMsg msg){
            if(msg.active){
                GetCom<Text>("Text_Tip").text = msg.tip;
                interactObject = msg.interactObject;
                Show();
            }
            else if(interactObject == msg.interactObject){
                Hide();
                interactObject = null;
            }
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.E)){
                if(interactObject != null){
                    Hide();
                    interactObject.Interact();
                    interactObject = null;
                }
            }
        }

        public override void Show()
        {
            group.alpha = 1;
        }
    }
}
