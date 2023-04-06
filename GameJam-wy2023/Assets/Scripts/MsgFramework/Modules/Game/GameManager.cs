using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Modules
{
    public class GameManager : Base.ManagerBase<GameManager>
    {
        MonoBehaviour mono;
        public static MonoBehaviour Mono{
            get{
                return Instance.mono;
            }
            set{
                Instance.mono = value;
            }
        }
        public GameManager(){
            if(mono == null){
                mono = GameObject.FindObjectOfType<DyeFrame>();
                if(mono == null)
                    mono = new GameObject("DyeFrame").AddComponent<DyeFrame>();
            }
        }
    }
}
