using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DyeFramework.Base;
using DyeFramework.Modules;
using DG.Tweening;

namespace OJ
{
    public class GameManager_2 : MonoBase<GameManager>
    {
        bool loaded;
        ViewCamera viewCamera;
        private void Awake() {
            Bind(
                GameEvent.CHANGE_HP
            );
        }
        private void Start() {
            viewCamera = FindObjectOfType<ViewCamera>();
        }
        private void Update() {
            if(!loaded){
                LoadData();
                loaded = true;
                InvokeRepeating("SaveData", 30f, 30f);
                UIManager.HidePanel("LoadingPanel");
            }
        }
        public override void Execute(int eventCode, object arg)
        {
            switch(eventCode){
                case GameEvent.CHANGE_HP:{
                    UpdateHp(arg==null?0:(int)arg);
                    break;
                }
            }
        }

        void LoadData(){
            //玩家位置
            PlayerController playerController = viewCamera.playerController;
            string[] xyz = DataManager.gameData.position.Split(",");
            Vector3 playerPos = new Vector3(float.Parse(xyz[0]), float.Parse(xyz[1]), float.Parse(xyz[2]));
            playerController.transform.position = playerPos;
            
        }
        void SaveData(){
            DataManager.gameData.sceneId = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            Vector3 pos = viewCamera.playerController.transform.position;
            DataManager.gameData.position = $"{pos.x},{pos.y},{pos.z}";
            DataManager.GameSave(null);
        }

        void UpdateHp(int delta){
            DataManager.gameData.hp = Mathf.Clamp(DataManager.gameData.hp + delta, 0, 100);
            if(DataManager.gameData.hp <= 0){
                ViewCamera viewCamera = FindObjectOfType<ViewCamera>();
                if(viewCamera){
                    AnimalController animalController = viewCamera.followTarget as AnimalController;
                    if(animalController){
                        animalController.Die();
                        animalController.enabled = false;
                    }
                    viewCamera.enabled = false;
                    viewCamera.transform.DOLookAt(animalController.transform.position, 3f);
                    Invoke("GameOver", 6f);
                }
            }
        }
        void GameOver(){
            SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
