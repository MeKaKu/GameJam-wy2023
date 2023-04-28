using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class HouseEnter : InteractBase
    {
        public override void Interact()
        {
            //进入小屋
            UIManager.ShowPanel("LoadingPanel");
            SceneManager.LoadScene("3-house");
        }
    }
}
