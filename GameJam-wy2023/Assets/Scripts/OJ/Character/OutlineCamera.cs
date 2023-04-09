using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [RequireComponent(typeof(OutlineEffect))]
    public class OutlineCamera : MonoBehaviour
    {
        public LayerMask targetLayer;
        public LayerMask drawLayer;
        public float maxDistance = 100;
        [SerializeField]Material material;
        Camera cam;
        
        private void Awake() {
            cam = GetComponent<Camera>();
            if(cam == null){
                cam = Camera.main;
            }
        }
        public Transform Detect() {
            Ray ray = new Ray(transform.position, transform.forward);
            //Debug.DrawRay(transform.position, transform.forward, Color.red, 1);
            if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, targetLayer)){
                MeshFilter[] mfs = hit.transform.GetComponentsInChildren<MeshFilter>();
                foreach(var mf in mfs){
                    Graphics.DrawMesh(mf.sharedMesh, mf.transform.localToWorldMatrix, material, 7, cam);
                }
                SkinnedMeshRenderer[] smrs = hit.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach(var smr in smrs){
                    Graphics.DrawMesh(smr.sharedMesh, smr.transform.localToWorldMatrix, material, 7, cam);
                }
                return hit.transform;
            }
            return null;
        }
    }
}
