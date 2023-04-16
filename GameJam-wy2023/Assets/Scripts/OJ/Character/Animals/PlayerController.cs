using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class PlayerController : AnimalController
    {
        protected override void Awake()
        {
            base.Awake();
            if(animator == null)
                animator = GetComponent<Animator>();
        }
        [Header(">特效")]
        [SerializeField]Material dissolveMaterial;
        public void Dissolve(float duration, bool dissolve = true){
            if(dissolveMaterial!=null){
                StopCoroutine("AnimateDissolve");
                if(!dissolve){
                    avatar.forward = transform.forward;
                    avatarForward = transform.forward;
                }
                gameObject.SetActive(true);
                StartCoroutine(AnimateDissolve(duration, dissolve));
            }
        }
        IEnumerator AnimateDissolve(float duration, bool dissolve){
            float percent = 0;
            dissolveMaterial.SetFloat("_Percent", dissolve?percent:1-percent);
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            List<Material> tempMats = new List<Material>();
            //gameObject.SetActive(dissolve);
            foreach(var renderer in renderers){
                tempMats.Add(renderer.material);
                renderer.material = dissolveMaterial;
            }
            while(percent < 1){
                yield return null;
                percent += Time.deltaTime / duration;
                dissolveMaterial.SetFloat("_Percent", dissolve?percent:1-percent);
            }
            yield return new WaitForSeconds(.1f);
            gameObject.SetActive(!dissolve);
            for(int i=0;i<tempMats.Count;i++){
                renderers[i].material = tempMats[i];
            }
        }
    }
}
