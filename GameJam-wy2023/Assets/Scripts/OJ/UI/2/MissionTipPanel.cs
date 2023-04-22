using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OJ
{
    public class MissionTipPanel : PanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            Bind(
                UIEvent.SHOW_MISSION_TIP
            );
        }

        public override void Execute(int eventCode, object arg)
        {
            if(eventCode == UIEvent.SHOW_MISSION_TIP){
                GetCom<Text>("Img_Bg/Text_Tip").text = arg as string;
                group.DOFade(1, .6f);
                group.DOFade(0, .6f).SetDelay(2.4f);
            }
        }
    }
}
