using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OJ
{
    public class RiddlePanel_4 : RiddlePanelBase
    {
        float stepLen = .01f;
        float maxC = 155;
        private void Start() {
            riddleId = 4;
        }

        public override void Show()
        {
            GetCom<Image>("Img_Bar").fillAmount = .6f;
            GetCom<Text>("Text_Value").text = ((int)(.6f * maxC)) + "  C";
            base.Show();
        }

        protected override void OnClick(string name)
        {
            Image img = GetCom<Image>("Img_Bar");
            Text text = GetCom<Text>("Text_Value");
            switch(name){
                case "Btn_Up":{
                    img.fillAmount += stepLen;
                    break;
                }
                case "Btn_Down":{
                    img.fillAmount -= stepLen;
                    break;
                }
                case "Btn_Close":{
                    Result(false);
                    return;
                }
                case "Btn_Confirm":{
                    if((int)(img.fillAmount * maxC) == 117){
                        Result(true);
                    }
                    return;
                }
            }
            text.text = ((int)(img.fillAmount * maxC)) + "  C";
        }
    }
}
