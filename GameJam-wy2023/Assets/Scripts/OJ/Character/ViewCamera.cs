using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [RequireComponent(typeof(Camera))]
    public class ViewCamera : MonoBehaviour
    {
        Camera cam;
        bool possessing = true;
        [SerializeField]float possessSpeed = 5;
        [SerializeField]float followSpeed = 0.5f;
        [SerializeField]float rotateSpeed = 0.5f;
        [SerializeField]float dissolveDuration = 1f;
        Vector3 speed;
        [SerializeField]OutlineCamera outlineCamera;
        public PlayerController playerController;
        CameraController followTarget;
        private void Awake() {
            cam = GetComponent<Camera>();
            if(!playerController){
                playerController = FindObjectOfType<PlayerController>();
            }
        }
        private void Start() {
            Possess(playerController);
        }
        private void Update() {
            if(!possessing && Input.GetKeyDown(KeyCode.F)){
                if(followTarget != null && followTarget != playerController){
                    //解除附身
                    playerController.transform.position = new Vector3(
                        followTarget.transform.position.x, 
                        playerController.transform.position.y, 
                        followTarget.transform.position.z
                    );
                    playerController.transform.forward = followTarget.transform.forward;
                    Possess(playerController);
                }
            }
            //Follow();
            Detect();
        }
        private void FixedUpdate() {
            Follow();
        }
        public void Possess(CameraController cameraController){
            if(!cameraController) return;
            StopCoroutine("AnimatePossess");
            StartCoroutine(AnimatePossess(cameraController));
        }
        IEnumerator AnimatePossess(CameraController cameraController){
            possessing = true;
            followTarget?.UnPossessThis();
            if(followTarget == playerController){
                //附身到动物身上
                playerController.Dissolve(dissolveDuration, true);
                yield return new WaitForSeconds(dissolveDuration * .9f);
            }
            else if(cameraController == playerController){
                //接除附身
                playerController.Dissolve(dissolveDuration, false);
                yield return new WaitForSeconds(dissolveDuration * .7f);
            }
            followTarget = cameraController;
            Transform target = followTarget.cameraPoint;
            while(Vector3.Distance(target.position, transform.position) > 2f){
                transform.position = Vector3.Lerp(transform.position, target.position, possessSpeed * Time.deltaTime);
                transform.forward = Vector3.RotateTowards(
                    transform.forward, 
                    target.forward, 
                    rotateSpeed * Time.deltaTime * 0.2f,
                    possessSpeed * Time.deltaTime * 0.2f
                );
                yield return null;
            }
            while(Vector3.Distance(target.forward, transform.forward) > 0.1f){
                transform.position = Vector3.Lerp(transform.position, target.position, possessSpeed * Time.deltaTime);
                transform.forward = Vector3.Slerp(
                    transform.forward, 
                    target.forward, 
                    rotateSpeed * Time.deltaTime
                );
                yield return null;
            }
            possessing = false;
            followTarget.PossessThis();
        }
        void Follow(){
            if(possessing) return;
            transform.position = Vector3.SmoothDamp(
                transform.position, 
                followTarget.cameraPoint.transform.position, 
                ref speed,
                1f/followSpeed
            );
            transform.LookAt(followTarget.lookAtPoint);
        }
        void Detect(){
            if(!possessing && outlineCamera){
                Transform animal = outlineCamera.Detect();
                if(animal){
                    if(Input.GetMouseButtonDown(0)){
                        CameraController cameraController = animal.GetComponent<CameraController>();
                        if(cameraController){
                            Possess(cameraController);
                        }
                    }
                }
            }
            else{
                outlineCamera?.ClearTarget();
            }
        }
    }
}
