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
        public bool possessed {get;private set;} = false;
        public Transform lookAtPoint {get; private set;}
        public Transform cameraPoint {get;private set;}
        public IInputHandler inputHandler = new PlayerInput();
        float rotX;
        float rotY;
        protected virtual void Awake() {
            lookAtPoint = transform.Find("LookAt");
            cameraPoint = lookAtPoint?.Find("CameraPoint");
            // if(cameraPoint){
            //     Camera cam = cameraPoint.GetComponent<Camera>();
            //     cam.enabled = false;
            // }
        }
        protected virtual void Update() {
            OperateCamera();
        }
        protected void OperateCamera(){
            if(!possessed){
                return;
            }
            Vector2 mouseMove = inputHandler.GetMouseMove();
            rotY += mouseMove.x * sensitivityX;
            rotX -= mouseMove.y * sensitivityY;
            rotX = Mathf.Clamp(rotX, -up, down);
            transform.eulerAngles = new Vector3(0, rotY, 0);
            lookAtPoint.localEulerAngles = new Vector3(rotX, 0, 0);
            cameraPoint.LookAt(lookAtPoint);
        }

        public virtual void PossessThis(){
            possessed = true;
            gameObject.layer = LayerMask.NameToLayer("Player");
            gameObject.tag = "Player";
            //朝向
            rotY = transform.eulerAngles.y;
        }
        public virtual void UnPossessThis(){
            possessed = false;
            gameObject.layer = LayerMask.NameToLayer("Animal");
            gameObject.tag = "Animal";
            rotX = 0;
            lookAtPoint.localEulerAngles = new Vector3(rotX, 0, 0);
            cameraPoint.LookAt(lookAtPoint);
        }

        // protected virtual void OnValidate() {
        //     lookAtPoint = transform.Find("LookAt");
        //     cameraPoint = lookAtPoint?.Find("CameraPoint");
        //     cameraPoint.LookAt(lookAtPoint);
        // }
    }
}
