using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class SinglePostEffectBase : PostEffectsBase
{
        public Shader shader;
        Material mat;
        Camera effectCam;
        public Camera effectCamera{
            get{
                if(effectCam == null){
                    effectCam = GetComponent<Camera>();
                }
                return effectCam;
            }
        }
        public Material material {
            get{
                mat = CheckAndGetMaterial(shader, mat);
                return mat;
            }
        }
    }
}