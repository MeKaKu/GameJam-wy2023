using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using OJ;
using UnityEngine;
using UnityEngine.UI;

public class TestUIPanel : PanelBase
{
    protected override void OnClick(string name)
    {
        switch(name){
            case "Btn_Radom":{
                GetCom<Text>("Text_Title").text = Random.Range(0, 5).ToString();
                foreach(var dialog in DataManager.configs.TbNpc[1].Dialogs){
                    Debug.Log(dialog);
                }
                break;
            }
            case "Btn_Close":{
                Hide();
                break;
            }
            case "Btn_Reload":{
                SceneManager.LoadScene(0, ()=>{
                    Debug.Log("Reloaded.");
                });
                break;
            }
        }
    }
}
