using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class OutlineEffect : SinglePostEffectBase{
        [Range(0f, 1f)]public float edgesOnly = 0f;
        public Color edgeColor = Color.black;
        public Color backgroundColor = Color.white;
        public float sampleDistance = 1f;
        public float sensitivityDepth = 1f;
        public float sensitivityNormals = 1f;
        public RenderTexture rt;
        private void OnEnable() {
            effectCamera.depthTextureMode |= DepthTextureMode.DepthNormals;
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest) {
            if(rt == null){
                rt = RenderTexture.GetTemporary(src.width, src.height);
            }
            Graphics.Blit(src, rt);
            if(material!=null){
                material.SetFloat("_EdgesOnly", edgesOnly);
                material.SetColor("_EdgeColor", edgeColor);
                material.SetColor("_BackgroundColor", backgroundColor);
                material.SetFloat("_SampleDistance", sampleDistance);
                material.SetVector("_Sensitivity", new Vector4(sensitivityNormals, sensitivityDepth, 0f, 0f));
                Graphics.Blit(src, dest, material);
            }
            else{
                Graphics.Blit(src, dest);
            }
        }
    }
}
