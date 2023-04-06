using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Modules
{
    public class ResManager : Base.ManagerBase<ResManager>
    {
        public static T Load<T>(string path, bool create = false) where T : Object {
            T t = Resources.Load<T>(path);
            if(t == null){
                Debug.LogWarning($"资源{path}加载失败...");
            }
            if(t is Component){
                return GameObject.Instantiate(t as T);
            }
            else{
                return t;
            }
        }

        public static void LoadAsync<T>(string path, System.Action<T> callback = null, bool create = false) where T : Object{
            GameManager.Mono.StartCoroutine(IELoad<T>(path, callback, create));
        }
        static IEnumerator IELoad<T>(string path, System.Action<T> callback = null, bool create = false) where T : Object{
            ResourceRequest rr = Resources.LoadAsync<T>(path);
            yield return rr;
            if(rr.asset == null){
                Debug.LogWarning($"资源{path}加载失败...");
            }
            if(rr.asset is Component && create){
                callback?.Invoke(GameObject.Instantiate(rr.asset as T));
            }
            else{
                callback?.Invoke(rr as T);
            }
        } 

        //Maybe AB
    }
}
