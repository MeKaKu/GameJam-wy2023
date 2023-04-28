using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DyeFramework.Modules;

namespace OJ
{
    public class InteractLightSwitch : InteractBase
    {
        RiddleMsg riddleMsg;
        [SerializeField]GameObject houseLights;
        public override void Interact(){
            if(riddleMsg == null){
                    riddleMsg = new RiddleMsg(){
                    riddleId = 6,
                    onResult = RiddleResult
                };
            }
            //解谜游戏6，修电闸
            UIManager.Handle(UIEvent.SHOW_RIDDLE_PANEL, riddleMsg);
        }
        void RiddleResult(bool result){
            if(result){
                Complete();
                Init(true);
            }
            else{
                Trigger();
            }
        }
        public override void Init(bool completed)
        {
            houseLights?.SetActive(completed);
        }
    }
}
