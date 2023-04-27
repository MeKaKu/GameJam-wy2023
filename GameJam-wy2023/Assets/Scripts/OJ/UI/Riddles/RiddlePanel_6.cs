using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OJ
{
    public class RiddlePanel_6 : RiddlePanelBase
    {
        private void Start() {
            riddleId = 6;
        }
        string preName = null;
        int cnt;
        protected override void OnClick(string name)
        {
            if(name == "Btn_Close"){
                Result(false);
                return;
            }
            if(string.IsNullOrEmpty(preName)){
                preName = name;
                return;
            }
            if(preName[0] == name[0]){
                GetCom<Image>("Img_Line_" + name[0]).gameObject.SetActive(true);
                GetCom<Button>(preName).interactable = false;
                GetCom<Button>(name).interactable = false;
                preName = "";
                cnt ++;
                if(cnt >= 4){
                    Result(true);
                }
            }
            else{
                preName = name;
            }
        }
    }
}
