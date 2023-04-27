using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OJ
{
    public class InteractPainting : InteractBase
    {
        public override void Init(bool completed)
        {
            GetComponent<Rigidbody>().useGravity = completed;
        }

        public override void Interact()
        {
            Complete();
            transform.DOShakeRotation(1f, Vector3.right* 25, 6, 45, true).OnComplete(()=>{
                Init(true);
            });
        }
    }
}
