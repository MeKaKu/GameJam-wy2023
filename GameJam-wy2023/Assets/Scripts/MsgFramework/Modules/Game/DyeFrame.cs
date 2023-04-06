using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Modules
{
    public class DyeFrame : Base.MonoBase<GameManager>
    {
        static bool isInit = false;
        static DyeFrame instant = null;
        private void Start() {
            if(instant == null){
                instant = this;
                DontDestroyOnLoad(gameObject);
                GameManager.Mono = this;
                AudioManager.Instance.GetType();
            }
            else{
                Destroy(this);
            }
        }
    }
}
