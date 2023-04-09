using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public static class  ExGameObject
    {
        public static void SetLayer(this GameObject obj, int layer){
            foreach(var t in obj.GetComponentsInChildren<Transform>()){
                t.gameObject.layer = layer;
            }
        }
    }
}
