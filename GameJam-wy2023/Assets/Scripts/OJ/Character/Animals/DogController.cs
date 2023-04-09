using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class DogController : AnimalController
    {
        override protected void ActByAI(){
            if(points.Count<=0){
                Move(Vector3.zero);
                return;
            }
            Vector3 dist = points[pointIndex] - transform.position;
            dist = new Vector3(dist.x, 0, dist.z);
            if(dist.magnitude>0.05){
                Move(dist.normalized);
            }
            else{
                pointIndex ++;
                pointIndex %= points.Count;
            }
        }
    }
}
