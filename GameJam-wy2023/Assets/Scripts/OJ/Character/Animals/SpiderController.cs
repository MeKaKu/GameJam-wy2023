using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class SpiderController : AnimalController
    {
        [Header(">爬墙")]
        public LayerMask walkLayer;
        public LayerMask climbLayer;
        [Range(0f, 10f)]
        [SerializeField]float changePanelTime = 1f;
        [SerializeField]float skinWidth = 0.02f;
        Collider coll;
        bool changingPanel;
        bool inClimbing;

        private void OnEnable() {
            coll = GetComponent<Collider>();
        }

        protected override void Update()
        {
            if(changingPanel) return;
            base.Update();
        }

        protected override void FixedUpdate()
        {
            if(changingPanel||inClimbing) return;
            base.FixedUpdate();
        }

        //1.视角转换
        //2.位置改变
        //  | 地->墙
        //  | 墙->墙
        //  | 墙->地
        IEnumerator AnimateChangePanel(Vector3 onWallPoint, Vector3 upDir){
            changingPanel = true;
            float percent = 0;
            Vector3 targetRot = new Vector3(89, 0, 0);
            //Vector3 upDir = Quaternion.AngleAxis(outAng, transform.right) * transform.up;
            Vector3 originPoint = transform.position;
            Vector3 originUp = transform.up;
            Debug.DrawLine(transform.position, onWallPoint, Color.red, 1.5f * changePanelTime);
            rigid.constraints = RigidbodyConstraints.None;
            rigid.useGravity = false;
            rigid.velocity = Vector3.zero;
            rigid.isKinematic = true;
            while(percent < 1){
                percent += Time.deltaTime / changePanelTime * 4;
                yield return null;
                //摄像机视角
                lookAtPoint.eulerAngles = Vector3.Slerp(lookAtPoint.eulerAngles, targetRot, percent);
                cameraPoint.LookAt(lookAtPoint);
            }
            percent = 0;
            //地-->墙
            while(percent < 1){
                percent += Time.deltaTime / changePanelTime;
                yield return null;
                //控制器旋转
                transform.up = Vector3.Slerp(originUp, upDir, percent);
                //控制器位置
                transform.position = Vector3.Lerp(originPoint, onWallPoint, percent);
            }
            inClimbing = true;
            changingPanel = false;
        }

        void DetectWall(){
            if(changingPanel) return;
            if(coll is BoxCollider){
                BoxCollider box = coll as BoxCollider;
                Vector3 origin = box.bounds.center;
                Debug.DrawLine(origin, origin + avatarForward * (box.bounds.extents.z + skinWidth), Color.blue, 1f);
                if(Physics.Raycast(origin, avatarForward, out RaycastHit hit, box.bounds.extents.z + skinWidth, climbLayer|walkLayer)){
                    Debug.Log(hit.transform.name);
                    float outAng = Mathf.Acos(Vector3.Dot(transform.up, hit.normal));
                    float onWallLength = box.bounds.extents.z - box.bounds.extents.y / Mathf.Sin(outAng) - box.center.z;
                    float upDelta = onWallLength * Mathf.Sin(outAng);
                    float forwardDelta = onWallLength * Mathf.Cos(outAng) + box.bounds.extents.z;
                    Vector3 onWallPoint = origin + upDelta * transform.up + forwardDelta * avatarForward;
                    StartCoroutine(AnimateChangePanel(onWallPoint, hit.normal));
                }
            }
        }

        private void OnCollisionEnter(Collision other) {
            if(changingPanel||inClimbing) return;
            Debug.Log("Collision");
            DetectWall();
        }
    }
}
