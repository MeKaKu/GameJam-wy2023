using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace OJ
{
    public class MainMenuPanel : PanelBase
    {

        protected override void Awake()
        {
            base.Awake();
            Hide();
        }
        private void Start() {
            Show();
        }

        protected override void OnClick(string name)
        {
            switch(name){
                case "Btn_Option":{
                    ShowOptions();
                    break;
                }
                case "Btn_Exit":{
                    Application.Quit();
                    break;
                }
                case "Btn_Start":{
                    StartGame();
                    break;
                }
            }
        }

        void ShowOptions(){
            group.DOFade(0, .5f).OnComplete(()=>{
                Hide();
                UIManager.ShowPanel("OptionPanel");
            });
        }
        void StartGame(){
            group.DOFade(0, .5f).OnComplete(()=>{
                Hide();
                UIManager.ShowPanel("ArchivePanel");
            });
        }

        public override void Show()
        {
            group.interactable = true;
            Sequence sequence = DOTween.Sequence();
            Image img_L = GetCom<Image>("Btn_Start/Img_L");
            Image img_R = GetCom<Image>("Btn_Start/Img_R");
            img_L.DOFillAmount(0, 0);
            img_R.DOFillAmount(0, 0);

            sequence.Append(
                group.DOFade(1, .5f)
            );
            sequence.Append(
                img_L.DOFillAmount(1, .5f)
            );
            sequence.Join(
                img_R.DOFillAmount(1, .5f)
            );
            sequence.OnComplete(base.Show);
        }
    }
}
