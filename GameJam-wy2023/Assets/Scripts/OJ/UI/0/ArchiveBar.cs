using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace OJ
{
    public class ArchiveBar : PanelBase
    {
        bool hasData;
        public void SetData(ArchiveData data){
            GetCom<Text>("Text_Time").text = data.duration.ToString("HH:\t mm:\t ss");
            GetCom<Text>("Text_Percent").text = data.percent + "%";
            hasData = true;
            GetCom<Text>("Text_Date").text = data.lastTime.ToString("yyyy/MM/dd\t HH:mm");
            GetCom<Text>("Text_Date").gameObject.SetActive(hasData);
        }
    }
}
