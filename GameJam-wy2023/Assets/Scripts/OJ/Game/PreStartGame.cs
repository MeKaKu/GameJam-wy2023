using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class PreStartGame : MonoBehaviour
    {
        static int flowId = 0;

        [SerializeField]WinkEffect winkEffect;

        bool processing;
        private void Update() {
            if(processing) return;
            Process();
        }

        void Process(){
            processing = true;
            if(DataManager.gameData.completedFlows.Contains(flowId)){
                EndProcess();
                return;
            }
            Material material = winkEffect.material;
            Sequence sequence = DOTween.Sequence();
            material.SetFloat("_Height", 0);
            sequence.Append(
                material.DOFloat(winkEffect.maxHeight, "_Height", 1f).SetDelay(1f)
            );
            sequence.Append(
                material.DOFloat(winkEffect.minHeight, "_Height", .6f).SetLoops(4, LoopType.Yoyo)
            );
            sequence.Append(
                material.DOFloat(1, "_Height", .8f).SetDelay(.6f)
            );
            sequence.Append(
                winkEffect.transform.DOLocalRotate(Vector3.up * 90, .8f).SetRelative()
            );
            sequence.Append(
                winkEffect.transform.DOLocalRotate(-Vector3.up * 180, 1.6f).SetRelative()
            );
            sequence.Append(
                winkEffect.transform.DOLocalRotate(Vector3.up * 90, .8f).SetRelative()
            );
            sequence.OnComplete(EndProcess);
        }

        void EndProcess(){
            if(!DataManager.gameData.completedFlows.Contains(flowId)){
                DataManager.gameData.completedFlows.Add(flowId);
                UIManager.Handle(UIEvent.SHOW_MISSION_TIP, "黑暗之旅：失落之岛");
                UIManager.ShowPanel("PlayerStatePanel");
            }
            winkEffect.GetComponent<ViewCamera>().enabled = true;
            winkEffect.enabled = false;
            this.enabled = false;
        }
    }
}
