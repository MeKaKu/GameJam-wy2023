using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OJ
{
    public class PlayerStatePanel : PanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            Bind(
                UIEvent.PLAYER_STATE_CHANGED
            );
            Hide();
        }
        public override void Execute(int eventCode, object arg)
        {
            if(eventCode == UIEvent.PLAYER_STATE_CHANGED){
                UpdateState();
            }
        }
        Image hp;
        Image soul;
        private void Start() {
            GetCom<Image>("Img_Icon_Male").gameObject.SetActive(DataManager.gameData.isMale);
            GetCom<Image>("Img_Icon_Female").gameObject.SetActive(!DataManager.gameData.isMale);
            hp = GetCom<Image>("Img_HP");
            soul = GetCom<Image>("Img_Souls");
        }
        void UpdateState(){
            //...
            hp.DOFillAmount(DataManager.gameData.hp * 1.0f / 100.0f, .5f);
            soul.DOFillAmount(DataManager.gameData.soul / 5.0f, .2f);
        }
        public override void Show()
        {
            group.DOFade(1, .5f).OnComplete(()=>{
                UpdateState();
            });
        }
    }
}
