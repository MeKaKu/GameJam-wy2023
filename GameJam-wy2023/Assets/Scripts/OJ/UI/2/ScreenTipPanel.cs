using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace OJ
{
    public class ScreenTipPanel : PanelBase
    {
        protected override void Awake()
        {
            base.Awake();
            Bind(
                UIEvent.SHOW_SCREEN_TIP
            );
        }

        public override void Execute(int eventCode, object arg)
        {
            if(eventCode == UIEvent.SHOW_SCREEN_TIP){
                if(arg as string  == null) return;
                GetCom<Text>("Text_Tip").text = "";
                GetCom<Text>("Text_Tip").DOText(arg as string, 1f);
                group.DOFade(1, .2f);
                group.DOFade(0, .6f).SetDelay(2.4f);
            }
        }
    }
}
