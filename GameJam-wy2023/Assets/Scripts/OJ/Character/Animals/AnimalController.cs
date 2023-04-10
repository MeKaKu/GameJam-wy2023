using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [RequireComponent(typeof(Rigidbody))]
    public class AnimalController : CameraController
    {
        [Header(">移动")]
        [Range(0f, 20f)]
        public float walkingSpeed = 1;//行走速度
        [Tooltip("奔跑速度是行走速度的多少倍")]
        [Range(1f, 10f)]
        public float runningMultiple = 2;
        [Tooltip("加速度参数，起步、行走到奔跑的速度变化快慢")]
        [Range(0, 20)]
        [SerializeField]protected float acc = 1;
        [Tooltip("转身速度，弧度/秒")]
        [Range(0, 20)]
        [SerializeField]protected float rotateSpeed = 3.1415926f;
        [Tooltip("起跳速度")]
        [Range(0, 20)]
        [SerializeField]protected float jumpSpeed = 3;
        protected Rigidbody rigid;//刚体
        protected Transform avatar;//人物模型
        protected Animator animator;//动画控制器
        float dv = 1;//奔跑速度是行走速度的多少倍
        protected bool onGround;
        protected Vector3 thrustVelocity = Vector3.zero;//当前帧需要施加的瞬间速度
        protected Vector3 thrustDeltaPosition = Vector3.zero;//当前帧需要施加的瞬间位移
        Vector3 planeVelocity = Vector3.zero;
        protected Vector3 avatarForward = Vector3.forward;

        [Header(">AI")]
        public bool useAI = true;
        protected int pointIndex = 0;
        protected List<Vector3> points = new List<Vector3>();
        protected override void Awake() {
            base.Awake();
            rigid = GetComponent<Rigidbody>();
            avatar = transform.Find("Avatar");
            animator = avatar.GetComponent<Animator>();
            avatarForward = avatar.forward;
        }
        protected virtual void Start() {
            Transform checkPoints = transform.Find("AI/CheckPoints");
            if(checkPoints)
            for(int i=0;i<checkPoints.childCount;i++){
                points.Add(checkPoints.GetChild(i).position);
            }
        }

        protected override void Update() {
            base.Update();
            if(possessed){
                ActByPlayer();
            }
            else{
                Vector3 temp = avatar.forward;
                transform.forward = temp;
                avatar.forward = temp;
                ActByAI();
            }
        }
        protected virtual void FixedUpdate() {
            rigid.position += thrustDeltaPosition;
            rigid.velocity = new Vector3(planeVelocity.x, rigid.velocity.y, planeVelocity.z) + thrustVelocity;
            thrustDeltaPosition = Vector3.zero;
            thrustVelocity = Vector3.zero;

            DetectGround();
        }

        protected virtual void ActByPlayer(){
            if(!possessed) return;
            //平面移动局部输入方向
            Vector2 moveDir = inputHandler.GetMoveDir();
            //加速
            if(inputHandler.SpeedUp()){
                dv = Mathf.Lerp(dv, runningMultiple, acc * Time.deltaTime);
            }
            else{
                dv = Mathf.Lerp(dv, 1, acc * Time.deltaTime);
            }
            //世界空间下的输入方向
            Vector3 moveVelocity = transform.forward * moveDir.y + transform.right * moveDir.x;
            moveVelocity *= dv;
            Move(moveVelocity);
            //跳跃
            if(inputHandler.Jump()){
                Jump();
            }
        }
        protected override void OnViewChange(float rot_y)
        {
            Vector3 tempForward = avatar.forward;
            base.OnViewChange(rot_y);
            avatar.forward = tempForward;
        }
        protected virtual void ActByAI(){
            //...
            Move(Vector3.zero);
        }
        protected virtual void StartJump(){
            animator.SetTrigger("Jump_t");
        }
        protected virtual void EndJump(){
            animator.ResetTrigger("Jump_t");
        }
        protected virtual void EnterGround(){
            animator.SetBool("OnGround_b", true);
        }
        protected virtual void ExitGround(){
            animator.SetBool("OnGround_b", false);
        }
        protected virtual void OnMove(float moveSpeed){
            animator.SetFloat("Speed_f", moveSpeed);
        }

        //平面移动
        protected void Move(Vector3 moveVelocity){
            if(!onGround) return;
            //朝向
            float moveSpeed = moveVelocity.magnitude;
            if(moveSpeed > 0.01){
                avatarForward = moveVelocity.normalized;
            }
            //avatar.forward = Vector3.RotateTowards(avatar.forward, avatarForward, rotateSpeed * Time.deltaTime, walkingSpeed * Time.deltaTime);
            avatar.forward = Vector3.Slerp(avatar.forward, avatarForward, rotateSpeed * Time.deltaTime);
            //速度
            planeVelocity = moveVelocity * walkingSpeed;
            //动画
            OnMove(moveSpeed);
        }
        protected void Jump(){
            if(!onGround) return;
            //状态
            //速度
            thrustVelocity += new Vector3(0, jumpSpeed, 0);
            //动画
            StartJump();
        }

        protected void DetectGround(){
            float r = 0.1f;
            Collider[] colliders = Physics.OverlapSphere(transform.position, r);
            if(colliders.Length > 0){
                foreach(var collider in colliders){
                    if(collider.gameObject.layer != LayerMask.NameToLayer("Player")){
                        if(!onGround){
                            EnterGround();
                            onGround = true;
                            EndJump();
                        }
                        return;
                    }
                }
            }
            if(onGround){
                ExitGround();
                onGround = false;
            }
        }

        public override void PossessThis()
        {
            base.PossessThis();
            gameObject.SetLayer(LayerMask.NameToLayer("Player"));
            gameObject.tag = "Player";
        }
        public override void UnPossessThis()
        {
            base.UnPossessThis();
            gameObject.SetLayer(LayerMask.NameToLayer("Animal"));
            gameObject.tag = "Animal";
        }
    }
}
