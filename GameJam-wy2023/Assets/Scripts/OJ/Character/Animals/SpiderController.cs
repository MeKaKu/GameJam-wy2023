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
        [Range(0f, 10f)]
        [SerializeField]float climbViewSize = 3f;
        Collider coll;
        bool changingPanel;
        bool inClimbing;
        Vector3 planeNormal = Vector3.up;

        private void OnEnable() {
            coll = GetComponent<Collider>();
        }

        protected override void Update()
        {
            if(changingPanel) return;
            else if(inClimbing){
                if(possessed){
                    ActByPlayer();
                }
                else{
                    ActByAI();
                }
                //fix up
                if(onGround) transform.up = planeNormal;
            }
            else base.Update();

            //avatar.up = transform.up;
            //lookAtPoint.localEulerAngles = new Vector3(90, 0, 0);
        }

        protected override void FixedUpdate()
        {
            if(changingPanel) return;
            rigid.position += thrustDeltaPosition;
            rigid.velocity = planeVelocity + thrustVelocity;
            thrustDeltaPosition = Vector3.zero;
            thrustVelocity = Vector3.zero;
            DetectGround();
        }
        protected override void ExitGround()
        {
            if(changingPanel) return;
            base.ExitGround();
            inClimbing = false;
        }
        protected override void Move(Vector3 moveVelocity)
        {
            if(!onGround){
                planeVelocity =  -transform.up * walkingSpeed;
                return;
            }
            //朝向
            float moveSpeed = moveVelocity.magnitude;
            if(moveSpeed > 0.01){
                avatarForward = moveVelocity.normalized;
            }
            //avatar.forward = Vector3.RotateTowards(avatar.forward, avatarForward, rotateSpeed * Time.deltaTime, walkingSpeed * Time.deltaTime);
            //avatar.forward = LerpRotateTo(avatar.forward, avatarForward, transform.up, rotateSpeed * Time.deltaTime);
            //avatar.localEulerAngles = GetEulerAnglesTo(avatar.localEulerAngles, avatarForward, transform.forward, transform.up, rotateSpeed * Time.deltaTime);
            //avatar.forward = Vector3.Slerp(avatar.forward, avatarForward, rotateSpeed * Time.deltaTime);
            avatar.localEulerAngles += Vector3.up * GetLerpRotateAngle(avatar.forward, avatarForward, transform.up, rotateSpeed * Time.deltaTime);
            //速度
            planeVelocity = moveVelocity * walkingSpeed;
            //动画
            OnMove(moveSpeed);
        }

        protected override void OnViewChange(float rot_y)
        {
            if(!changingPanel && !inClimbing)
            base.OnViewChange(rot_y);
        }

        //1.视角转换
        //2.位置改变
        //  | 地->墙
        //  | 墙->墙
        //  | 墙->地
        IEnumerator AnimateChangePanel(Vector3 onWallPoint, Vector3 upDir, bool toWall = true){
            changingPanel = true;
            if(!toWall) onWallPoint += upDir * 0.05f;
            //Debug.DrawLine(transform.position, onWallPoint, Color.red, 1.5f * changePanelTime);
            if(inClimbing != toWall){//地-->墙、墙-->地
                float percent = 0;
                Vector3 targetRot = toWall? new Vector3(80, 0, 0) : Vector3.zero;
                Vector3 cameraPos = ( toWall? climbViewSize : viewSize ) * -Vector3.forward;
                if(toWall){//地-->墙
                    rigid.constraints = RigidbodyConstraints.None;
                    rigid.useGravity = false;
                    rigid.velocity = Vector3.zero;
                }
                if(!toWall) yield return AnimateChangePosition(onWallPoint, upDir);
                while(percent < 1){
                    percent += Time.deltaTime / changePanelTime * 4;
                    yield return null;
                    //摄像机视角
                    lookAtPoint.localEulerAngles = Vector3.Slerp(lookAtPoint.localEulerAngles, targetRot, percent);
                    cameraPoint.localPosition = Vector3.Lerp(cameraPoint.localPosition, cameraPos, percent);
                    cameraPoint.LookAt(lookAtPoint);
                    if(!toWall) transform.up = upDir;
                }
                if(toWall) yield return AnimateChangePosition(onWallPoint, upDir);
            }
            else{
                yield return AnimateChangePosition(onWallPoint, upDir);
            }
            rigid.velocity = Vector3.zero;
            avatarForward = avatar.forward;
            planeNormal = upDir;

            if(!toWall){//墙-->地
                rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                rigid.useGravity = true;
                rigid.velocity = Vector3.zero;
            }
            inClimbing = toWall;
            changingPanel = false;
        }
        IEnumerator AnimateChangePosition(Vector3 onWallPoint, Vector3 upDir){
            float percent = 0;
            Vector3 originPoint = transform.position;
            Vector3 originUp = transform.up;
            while(percent < 1){
                percent += Time.deltaTime / changePanelTime;
                yield return null;
                //控制器旋转
                transform.up = Vector3.Slerp(originUp, upDir, percent);
                //控制器位置
                transform.position = Vector3.Lerp(originPoint, onWallPoint, percent);
            }
        }

        void DetectWall(){
            if(changingPanel) return;
            if(coll is BoxCollider){
                BoxCollider box = coll as BoxCollider;
                float detectDist = (new Vector2(box.bounds.size.x, box.bounds.size.z)).magnitude + skinWidth;
                Vector3 origin = box.bounds.center;
                Debug.DrawLine(origin, origin + avatarForward * detectDist, Color.blue, 1f);
                if(Physics.Raycast(origin, avatarForward, out RaycastHit hit, detectDist, climbLayer|walkLayer)){
                    Debug.Log(hit.transform.name);
                    bool toWall = (1<<hit.transform.gameObject.layer & climbLayer) > 0;
                    if(!inClimbing && !toWall){
                        return;
                    }
                    if(!Facing(hit.normal)){
                        return;
                    }
                    float outAng = Mathf.Acos(Mathf.Clamp(Vector3.Dot(transform.up, hit.normal), -1f, 1f));
                    float onWallLength = detectDist - box.bounds.extents.y / Mathf.Sin(outAng);
                    float upDelta = onWallLength * Mathf.Sin(outAng);
                    float forwardDelta = onWallLength * Mathf.Cos(outAng) + hit.distance;
                    Vector3 onWallPoint = origin + upDelta * transform.up + forwardDelta * avatarForward;
                    StartCoroutine(AnimateChangePanel(onWallPoint, hit.normal,toWall));
                }
            }
        }
        bool Facing(Vector3 normal){
            Vector3 v = Vector3.Cross(normal, avatar.forward);
            Debug.Log("v="+v.normalized);
            if(v.magnitude < 0.05f){
                return true;
            }
            Debug.Log(v.magnitude);
            Debug.Log("right=" + avatar.right);
            return Vector3.Distance(v.normalized, avatar.right) < 0.05f;
        }

        float GetLerpRotateAngle(Vector3 from, Vector3 to, Vector3 axis, float t){
            from.Normalize();
            to.Normalize();
            t = Mathf.Clamp01(t);
            if(from==to) return 0;
            float cosValue = Vector3.Dot(from, to);
            cosValue = Mathf.Clamp(cosValue, -1f, 1f);
            float angle = Mathf.Acos( cosValue );
            //Debug.Log("cos="+cosValue);
            if(angle == float.NaN) return 0;
            //angle = Mathf.Clamp(angle, 0, 180);
            angle *= Mathf.Rad2Deg;
            if(angle == 0 || angle == 180) return angle * t;
            //Debug.Log("Angle="+angle);
            Vector3 v = Vector3.Cross(from, to);
            float sign = Vector3.Dot(v, axis);
            //Debug.Log("sign="+sign);
            sign /= Mathf.Abs(sign);
            //Vector3 res = Quaternion.AngleAxis(sign * angle * t, axis) * from;
            angle *= sign;
            //Debug.Log("from="+from + " to="+to+" axis=" + axis);
            return angle * t;
        }

        Vector3 GetEulerAnglesTo(Vector3 fromEuler, Vector3 toDir, Vector3 forwardDir, Vector3 upDir, float t){
            toDir.Normalize();
            forwardDir.Normalize();
            t = Mathf.Clamp01(t);
            Debug.Log("to="+toDir + " forward="+forwardDir + " upDir="+upDir);
            float angle = Mathf.Acos( Vector3.Dot(toDir, forwardDir) );
            angle *= Mathf.Rad2Deg;
            float sign = 1;
            if(angle!=180 && angle!=0){
                Vector3 v = Vector3.Cross(forwardDir, toDir);
                Debug.Log("v="+v);
                sign = Vector3.Dot(v, upDir);
                sign /= Mathf.Abs(sign);
            }
            Debug.Log("sign="+sign);
            angle *= sign;
            if(sign < 0){
                angle += 360;
            }
            angle = Mathf.Lerp(fromEuler.y, angle, t);
            Debug.Log("angle=" + angle);
            if(fromEuler.y > 180){
                Debug.LogWarning(fromEuler.y);
            }
            return Vector3.up * angle;  
        }

        private void OnCollisionEnter(Collision other) {
            if(changingPanel) return;
            DetectWall();
        }
    }
}
