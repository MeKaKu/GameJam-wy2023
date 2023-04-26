using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class RiddlePanelBase : PanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            Bind(
                UIEvent.SHOW_RIDDLE_PANEL
            );
            Hide();
        }
        protected RiddleMsg riddleMsg;
        protected int riddleId;
        public override void Execute(int eventCode, object arg)
        {
            if(eventCode == UIEvent.SHOW_RIDDLE_PANEL){
                RiddleMsg msg = arg as RiddleMsg;
                if(msg == null || msg.riddleId != riddleId){
                    return;
                }
                Show(msg);
            }
        }

        void Show(RiddleMsg msg){
            riddleMsg = msg;
            Show();
        }

        public void Result(bool result){
            Hide();
            if(!DataManager.gameData.completedRiddles.Contains(riddleId)){
                DataManager.gameData.completedRiddles.Add(riddleId);
            }
            riddleMsg.onResult?.Invoke(result);
        }
    }
}
