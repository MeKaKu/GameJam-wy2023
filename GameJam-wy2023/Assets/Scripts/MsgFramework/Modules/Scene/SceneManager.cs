
namespace DyeFramework.Modules
{
    public class SceneManager : Base.ManagerBase<SceneManager>
    {
        public SceneManager(){
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        System.Action onSceneLoaded;
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode){
            onSceneLoaded?.Invoke();
            onSceneLoaded = null;
        }
        public static void LoadScene(string sceneName, System.Action callback = null){
            Instance.onSceneLoaded = callback;
            if(!string.IsNullOrWhiteSpace(sceneName)){
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            }
        }
        public static void LoadScene(int sceneId, System.Action callback = null){
            Instance.onSceneLoaded = callback;
            if(sceneId >= 0 && sceneId < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings){
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
            }
        }
    }
}
