using System.Collections;
using System.Collections.Generic;
using DyeFramework.Modules;
using UnityEngine;

namespace OJ
{
    public class ArchivePanel : PanelBase
    {
        private void Start() {
            Hide();
        }
        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)){
                Hide();
                UIManager.ShowPanel("MainMenuPanel");
            }
        }

        protected override void OnClick(string name)
        {
            if(name.StartsWith("ArchiveBar_")){
                //进入存档
                int id = int.Parse(name.Split('_')[1]);

                UIManager.ShowPanel("LoadingPanel");
                DataManager.LoadArchive(id, (b)=>{
                    if(!string.IsNullOrEmpty(DataManager.gameData.playerName)){//已经创建过角色了
                        SceneManager.LoadScene(DataManager.gameData.sceneId);
                        return;
                    }
                    else SceneManager.LoadScene("1");
                });
            }
        }

        public override void Show()
        {
            List<ArchiveData> archiveDatas = DataManager.LoadArchiveDatas();
            foreach(var archiveData in archiveDatas){
                GetCom<ArchiveBar>("ArchiveBar_" + archiveData.archiveId).SetData(archiveData);
            }
            base.Show();
            //动画先不做了...
        }
    }
}
