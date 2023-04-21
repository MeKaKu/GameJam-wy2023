using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OJ
{
    public class WinkEffect : SinglePostEffectBase
    {
        public float minHeight = 0f;
        public float maxHeight = 0.25f;
        [SerializeField]float height = 0.25f;
        public float width = 0.5f;
        public float blurWidth = 1f;
        public Color color = Color.black;
        private void OnRenderImage(RenderTexture src, RenderTexture dest) {
            if(material){
                material.SetFloat("_Width", width);
                //material.SetFloat("_Height", height);
                material.SetFloat("_BlurWidth", blurWidth);
                material.SetColor("_Color", color);
                Graphics.Blit(src, dest, material);
            }
            else{
                Graphics.Blit(src, dest);
            }
        }

        protected override void Start()
        {
            base.Start();

            Sequence sequence = DOTween.Sequence();
            material.SetFloat("_Height", 0);
            sequence.Append(
                material.DOFloat(maxHeight, "_Height", 1f).SetDelay(1f)
            );
            sequence.Append(
                material.DOFloat(minHeight, "_Height", .6f).SetLoops(4, LoopType.Yoyo)
            );
            sequence.Append(
                material.DOFloat(2, "_Height", .8f).SetDelay(.6f)
            );
        }

        private void OnValidate() {
            material.SetFloat("_Height", height);
        }
    }
}
