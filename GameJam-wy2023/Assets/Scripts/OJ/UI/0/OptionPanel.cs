using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OJ
{
    public class OptionPanel : PanelBase
    {
        Transform activeOptions;
        Image activeImg;
        private void Start() {
            Hide();
            activeOptions = transform.Find("AudioOptions");
            activeOptions.gameObject.SetActive(true);
            activeImg = GetCom<Image>("Btn_Audio/Img_Line");

            GetCom<Slider>("AudioOptions/Slider_Main").value = AudioManager.Instance.mainVolumePercent;
            GetCom<Slider>("AudioOptions/Slider_Music").value = AudioManager.Instance.musicVolumePercent;
            GetCom<Slider>("AudioOptions/Slider_Sound").value = AudioManager.Instance.soundVolumePercent;
        }

        protected override void OnClick(string name)
        {
            Button btn = GetCom<Button>(name);
            activeOptions.gameObject.SetActive(false);
            Image newImg = activeImg;
            switch(name){
                case "Btn_Audio":{
                    activeOptions = transform.Find("AudioOptions");
                    newImg = GetCom<Image>(name+"/Img_Line");
                    break;
                }
                case "Btn_Lang":{
                    activeOptions = transform.Find("LangOptions");
                    newImg = GetCom<Image>(name+"/Img_Line");
                    break;
                }
                case "Btn_Key":{
                    activeOptions = transform.Find("KeyOptions");
                    newImg = GetCom<Image>(name+"/Img_Line");
                    break;
                }
                case "Btn_Save":{
                    ShowMainMenu();
                    break;
                }
            }
            activeOptions.gameObject.SetActive(true);
            if(newImg != activeImg){
                activeImg.DOFillAmount(0, .3f);
                newImg.DOFillAmount(1, .3f);
                activeImg = newImg;
            }
        }

        void ShowMainMenu(){
            Hide();
            AudioManager.ChangeVolume(AudioManager.AudioType.Main, (int)(GetCom<Slider>("AudioOptions/Slider_Main").value * 10));
            AudioManager.ChangeVolume(AudioManager.AudioType.Music, (int)(GetCom<Slider>("AudioOptions/Slider_Music").value * 10));
            AudioManager.ChangeVolume(AudioManager.AudioType.Sound, (int)(GetCom<Slider>("AudioOptions/Slider_Sound").value * 10));
            UIManager.ShowPanel("MainMenuPanel");
        }
    }
}
