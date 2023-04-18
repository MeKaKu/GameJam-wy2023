using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class CameraController : MonoBehaviour
    {
        [Header(">视角")]
        [Range(0, 10)]
        public float sensitivityX = 1;//水平视角移动的灵敏度
        [Range(0, 10)]
        public float sensitivityY = 1;//垂直视角移动的灵敏度
        [Range(0, 90)]
        public float up = 30;//向上看的最大角度
        [Range(0, 90)]
        public float down = 20;//向下看的最大角度
        [SerializeField]bool showLocalView;
        [Range(-10f, 10f)]
        [SerializeField]protected float viewHeight = 1f;
        [Range(-10f, 10f)]
        [SerializeField]protected float viewSize = 2f;
        public bool possessed {get;private set;} = false;
        public Transform lookAtPoint {get; private set;}
        public Transform cameraPoint {get;private set;}
        public IInputHandler inputHandler = new PlayerInput();
        [Range(-90, 90)]
        [SerializeField]float rotX;
        [Range(-360, 360)]
        [SerializeField]float rotY;
        protected virtual void Awake() {
            lookAtPoint = transform.Find("LookAt");
            cameraPoint = lookAtPoint?.Find("CameraPoint");
            // if(cameraPoint){
            //     Camera cam = cameraPoint.GetComponent<Camera>();
            //     cam.enabled = false;
            // }
            ResetCameraView();
            showLocalView = false;
            cameraPoint.GetComponent<Camera>().enabled = false;
        }
        protected virtual void Update() {

        }
        protected virtual void FixedUpdate() {
            OperateCamera();
        }
        protected void OperateCamera(){
            if(!possessed){
                cameraPoint.LookAt(lookAtPoint);
                return;
            }
            Vector2 mouseMove = inputHandler.GetMouseMove();
            rotY += mouseMove.x * sensitivityX;
            rotX -= mouseMove.y * sensitivityY;
            rotX = Mathf.Clamp(rotX, -up, down);
            OnViewChange(rotY);//模型朝向会出现问题...
            lookAtPoint.localEulerAngles = new Vector3(rotX, 0, 0);
            cameraPoint.LookAt(lookAtPoint);
        }
        protected virtual void OnViewChange(float rot_y){
            transform.eulerAngles = new Vector3(0, rot_y, 0);
        }

        public virtual void PossessThis(){
            possessed = true;
            //朝向
            rotY = transform.eulerAngles.y;
        }
        public virtual void UnPossessThis(){
            possessed = false;
            rotX = 0;
            lookAtPoint.localEulerAngles = new Vector3(rotX, 0, 0);
            cameraPoint.LookAt(lookAtPoint);
        }

        protected virtual void OnValidate() {
            #if UNITY_EDITOR
            lookAtPoint = transform.Find("LookAt");
            if(!lookAtPoint){
                lookAtPoint = new GameObject("LookAt") .transform;
                lookAtPoint.SetParent(transform);
            }
            cameraPoint = lookAtPoint.Find("CameraPoint");
            if(!cameraPoint){
                cameraPoint = new GameObject("CameraPoint", typeof(Camera)) .transform;
                cameraPoint.SetParent(lookAtPoint);
            }
            if(!cameraPoint.GetComponent<Camera>()){
                cameraPoint.gameObject.AddComponent<Camera>();
            }
            lookAtPoint.localPosition = Vector3.up * viewHeight;
            lookAtPoint.localEulerAngles = new Vector3(rotX, 0, 0);
            cameraPoint.localPosition = -Vector3.forward * viewSize;
            cameraPoint.localEulerAngles = new Vector3(0, rotY, 0);
            cameraPoint.GetComponent<Camera>().enabled = showLocalView;
            #endif
        }

        [ContextMenu("ResetCameraView")]
        void ResetCameraView(){
            rotX = 0;
            rotY = 0;
            lookAtPoint.localEulerAngles = Vector3.zero;
            cameraPoint.LookAt(lookAtPoint);
        }
    }
}
