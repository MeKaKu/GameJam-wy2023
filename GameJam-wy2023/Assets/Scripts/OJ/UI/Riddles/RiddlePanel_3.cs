using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OJ
{
    public class RiddlePanel_3 : RiddlePanelBase
    {
        private void Start() {
            riddleId = 3;
        }

        void ResetRiddle(){
            GetCom<Image>("Img_Value").enabled = false;
            for(int i=1;i<=4;i++){
                GetCom<Button>("Btn_" + i).interactable = true;
            }
            GetCom<Button>("Btn_Confirm").interactable = true;
            GetCom<Image>("Img_Bar").fillAmount = 0;
            GetCom<Slider>("Slider_Value").value = 0;
        }

        public override void Show()
        {
            ResetRiddle();
            base.Show();
        }

        protected override void OnClick(string name)
        {
            switch(name){
                case "Btn_1":{
                    Debug.Log(name);
                    GetCom<Button>(name).interactable = false;
                    break;
                }
                case "Btn_2":{
                    if(GetCom<Button>("Btn_1").interactable == false){
                        GetCom<Button>(name).interactable = false;
                    }
                    break;
                }
                case "Btn_3":{
                    if(GetCom<Button>("Btn_2").interactable == false){
                        GetCom<Button>(name).interactable = false;
                    }
                    break;
                }
                case "Btn_4":{
                    if(GetCom<Button>("Btn_3").interactable == false){
                        GetCom<Button>(name).interactable = false;
                        GetCom<Image>("Img_Value").enabled = true;
                    }
                    break;
                }
                case "Btn_Close":{
                    Result(false);
                    break;
                }
                case "Btn_Confirm":{
                    if(GetCom<Button>("Btn_4").interactable == false){
                        float value = GetCom<Slider>("Slider_Value").value;
                        if(value > 3.0f / 7f && value < 3.2f / 7f){
                            GetCom<Button>(name).interactable = false;
                            group.blocksRaycasts = false;
                            GetCom<Image>("Img_Bar").DOFillAmount(1, .8f).OnComplete(()=>{
                                Result(true);
                            });
                        }
                    }
                    break;
                }
            }
        }
    }
}
