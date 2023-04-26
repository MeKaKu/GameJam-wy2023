using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class GameFlowBase : MonoBehaviour
    {
        bool processing;
        public int flowId {get; protected set;}
        public void TryProcess(){
            if(processing) return;
            processing = true;
            if(DataManager.gameData.completedFlows.Contains(flowId)){
                ProcessExit();
                return;
            }
            DataManager.flowing = true;
            Process();
        }
        protected virtual void Process(){

        }
        protected virtual void ProcessExit(){
            this.enabled = false;
        }
        protected virtual void ProcessEnd(){
            DataManager.gameData.completedFlows.Add(flowId);
            DataManager.flowing = false;
            ProcessExit();
        }
    }
}
