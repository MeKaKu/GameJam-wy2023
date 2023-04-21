using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class SimplerPostEffect : SinglePostEffectBase
    {
        private void OnRenderImage(RenderTexture src, RenderTexture dest) {
            if(material){
                Graphics.Blit(src, dest, material);
            }
            else{
                Graphics.Blit(src, dest);
            }
        }
    }
}
