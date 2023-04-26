using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class FlowBase : DyeFramework.Base.MonoBase<GameManager>
    {
        int flowId = 0;
        int nextFlowId = -1;
        bool processing;
        protected virtual void Awake() {
            Bind(
                GameEvent.TRY_FLOW
            );
        }

        protected void SetFlow(int flowId, int nextFlowId = -1){
            this.flowId = flowId;
            this.nextFlowId = nextFlowId;
        }

        public override void Execute(int eventCode, object arg)
        {
            if(GameEvent.TRY_FLOW == eventCode && (int)arg == flowId){
                processing = true;
                if(!DataManager.gameData.completedFlows.Contains(flowId)){
                    OnProcessEnded(false);
                }
                else{
                    Process();
                }
            }
        }

        protected virtual void Process(){
            GameManager.Handle(GameEvent.FLOW_STARED);
            //...
            //OnProcessEnded(true);
        }
        protected virtual void OnProcessEnded(bool firstTime){
            if(firstTime){
                DataManager.gameData.completedFlows.Add(flowId);
            }
            if(nextFlowId != -1){
                GameManager.Handle(GameEvent.TRY_FLOW, nextFlowId);
            }
            else{
                GameManager.Handle(GameEvent.FLOW_ENDED);
            }
        }
    }
}
