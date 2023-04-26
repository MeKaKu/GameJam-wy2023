using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    //睁眼
    [RequireComponent(typeof(WinkEffect))]
    public class Flow_1 : GameFlowBase
    {
        WinkEffect winkEffect;
        private void Awake() {
            flowId = 1;
            winkEffect = GetComponent<WinkEffect>();
        }
        private void Update() {
            TryProcess();
        }
        protected override void Process(){
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
                winkEffect.transform.DOLocalRotate(Vector3.up * 60, .8f).SetRelative()
            );
            sequence.Append(
                winkEffect.transform.DOLocalRotate(-Vector3.up * 120, 1.6f).SetRelative()
            );
            sequence.Append(
                winkEffect.transform.DOLocalRotate(Vector3.up * 60, .8f).SetRelative()
            );
            sequence.OnComplete(ProcessEnd);
        }

        protected override void ProcessEnd(){
            UIManager.Handle(UIEvent.SHOW_MISSION_TIP, "黑暗之旅：失落之岛");
            base.ProcessEnd();
        }
        protected override void ProcessExit()
        {
            UIManager.ShowPanel("PlayerStatePanel");
            winkEffect.GetComponent<ViewCamera>().enabled = true;
            winkEffect.enabled = false;
            base.ProcessExit();
        }
    }
}
