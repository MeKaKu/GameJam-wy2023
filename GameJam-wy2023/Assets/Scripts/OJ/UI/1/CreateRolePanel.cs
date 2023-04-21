using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Xml.Serialization;
using System.IO;

namespace OJ
{
    public class CreateRolePanel : PanelBase
    {
        private void Start() {
            Hide();
        }
        protected override void OnClick(string name)
        {
            switch(name){
                case "Btn_Female":
                case "Btn_Male":{
                    Transform btn = GetCom<Button>(name).transform;
                    Transform img = GetCom<Image>("Img_Choose").transform;
                    img.position = btn.position;
                    img.localScale = btn.localScale;
                    break;
                }
                case "Btn_Confirm":{
                    if(isNameOk){
                        DataManager.gameData.playerName =  GetCom<InputField>("Input_Name").text;
                        UIManager.ShowPanel("LoadingPanel");
                        DataManager.GameSave((b)=>{
                            SceneManager.LoadScene("2-main");
                        });
                    }
                    break;
                }
            }
        }
        bool isNameOk;
        protected override void OnInput(string name, string value)
        {
            if(name.Equals("Input_Name")){
                isNameOk = !string.IsNullOrEmpty(value) && value.Length < 10;
                GetCom<Image>("Img_NameOk").gameObject.SetActive(isNameOk);
                GetCom<Button>("Btn_Confirm").gameObject.SetActive(isNameOk);
            }
        }

        #region UI动画
        public override void Show()
        {
            Transform btn_female = GetCom<Button>("Btn_Female").transform;
            Transform btn_male = GetCom<Button>("Btn_Male").transform;
            btn_female.localEulerAngles = btn_male.localEulerAngles = Vector3.up * 90;
            Image img_choose = GetCom<Image>("Img_Choose");
            img_choose.DOFade(0,0);
            GetCom<InputField>("Input_Name").text = "";
            isNameOk = false;
            GetCom<Image>("Img_NameOk").gameObject.SetActive(isNameOk);
            GetCom<Button>("Btn_Confirm").gameObject.SetActive(isNameOk);

            group.interactable = true;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(
                group.DOFade(1, .5f)
            );
            sequence.Append(
                btn_female.DOLocalRotate(Vector3.zero, .3f)
            );
            sequence.Join(
                btn_male.DOLocalRotate(Vector3.zero, .3f)
            );
            sequence.Append(
                img_choose.DOFade(1, 1f)
            );
            sequence.OnComplete(base.Show);
        }
        #endregion
    }
}
