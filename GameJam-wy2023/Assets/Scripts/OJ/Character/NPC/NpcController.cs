using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class NpcController : PlayerController
    {
        private void OnTriggerEnter(Collider other) {
            if(other.tag.Equals("Player")){
                Vector3 dir = other.transform.position - transform.position;
                dir.y = 0;
                avatarForward = dir.normalized;
            }
        }
    }
}
