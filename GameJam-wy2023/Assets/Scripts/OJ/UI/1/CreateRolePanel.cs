using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class CreateRolePanel : PanelBase
    {
        protected override void OnClick(string name)
        {
            switch(name){
                case "Btn_Female":{

                    break;
                }
                case "Btn_Male":{

                    break;
                }
                case "Btn_Confirm":{
                    //TODO创建角色和存档
                    break;
                }
            }
        }

        protected override void OnInput(string name, string value)
        {
            if(name.Equals("Input_Name")){
                //...
            }
        }
    }
}
