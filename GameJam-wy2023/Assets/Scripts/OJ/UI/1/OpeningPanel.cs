using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace OJ
{
    public class OpeningPanel : PanelBase
    {
        private void Start() {
            Show();
            Text t1 = GetCom<Text>("Text_1");
            Text t2 = GetCom<Text>("Text_2");
            Sequence sequence = DOTween.Sequence();
            sequence.Append(
                t1.DOFade(1, 1f).SetDelay(1f)
            );
            sequence.AppendInterval(2f);
            sequence.Append(
                t1.DOFade(0, 1f)
            );
            sequence.AppendInterval(.5f);
            sequence.Append(
                t2.DOFade(1, 1f)
            );
            sequence.AppendInterval(3f);
            sequence.Append(
                t2.DOFade(0, 1f)
            );
            sequence.AppendCallback(ShowDialog);
            sequence.Play();
        }

        void ShowDialog(){
            DialogMsg dialogMsg = new DialogMsg();
            dialogMsg.speaker = "???";
            dialogMsg.content = "这是什么地方？为什么我从未听说过有关这个城市的一切，但总感到一种莫名的吸引力？\n还有...我...是....谁...？";
            dialogMsg.callback = CreateRole;
            UIManager.Handle(UIEvent.SHOW_DIALOG, dialogMsg);
        }

        void CreateRole(string s){
            Hide();
            UIManager.ShowPanel("CreateRolePanel");
        }
    }
}
