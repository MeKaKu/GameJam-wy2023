using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [RequireComponent(typeof(OutlineEffect))]
    public class OutlineCamera : MonoBehaviour
    {
        public int targetLayer;
        public int drawLayer;
        LayerMask layerMask;
        GameObject outlineGO;
        public float maxDistance = 100;
        [SerializeField]Material material;
        Camera cam;
        
        private void Awake() {
            cam = GetComponent<Camera>();
            if(cam == null){
                cam = Camera.main;
            }
            layerMask = (1<<targetLayer)|(1<<drawLayer);
        }
        public Transform Detect() {
            Ray ray = new Ray(transform.position, transform.forward);
            //Debug.DrawRay(transform.position, transform.forward, Color.red, 1);
            if(Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask)){
                // MeshFilter[] mfs = hit.transform.GetComponentsInChildren<MeshFilter>();
                // foreach(var mf in mfs){
                //     Graphics.DrawMesh(mf.sharedMesh, mf.transform.localToWorldMatrix, material, drawLayer, cam);
                // }
                // SkinnedMeshRenderer[] smrs = hit.transform.GetComponentsInChildren<SkinnedMeshRenderer>();
                // foreach(var smr in smrs){
                //     Graphics.DrawMesh(smr.sharedMesh, smr.transform.localToWorldMatrix, material, drawLayer, cam);
                // }
                if(outlineGO != hit.transform.gameObject){
                    if(outlineGO?.layer == drawLayer){
                        outlineGO.SetLayer(targetLayer);
                    }
                    outlineGO = hit.transform.gameObject;
                    if(outlineGO.layer != drawLayer) outlineGO.SetLayer(drawLayer);
                }   
                return hit.transform;
            }
            else{
                if(outlineGO?.layer == drawLayer){
                    outlineGO.SetLayer(targetLayer);
                    outlineGO = null;
                }
            }
            return null;
        }

        public void ClearTarget(){
            if(outlineGO?.layer == drawLayer){
                outlineGO.SetLayer(targetLayer);
                outlineGO = null;
            }
        }
    }
}
