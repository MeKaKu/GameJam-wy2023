using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OJ
{
    public class InteractDoor : InteractBase
    {
        [SerializeField]Transform side_1;
        [SerializeField]Transform side_2;
        public override void Interact()
        {
            Complete();
            side_1?.DOLocalRotate(-Vector3.up * 60, 1).SetRelative();
            side_2?.DOLocalRotate(Vector3.up * 60, 1).SetRelative();
        }

        public override void Init(bool completed)
        {
            if(completed){
                side_1?.DOLocalRotate(-Vector3.up * 60, 0).SetRelative();
                side_2?.DOLocalRotate(Vector3.up * 60, 0).SetRelative();
            }
        }
    }
}
