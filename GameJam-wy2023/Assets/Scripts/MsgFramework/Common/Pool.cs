using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Common{
    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T">MonoBehaviour的子类</typeparam>
    public class Pool<T> where T : Behaviour
    {
        T prefab;//池中对象的预制体
        Queue<T> q;//可复用的物体队列
        Transform parent;//创建时的默认父物体
        System.Action<T> onInstantiated;//创建新对象时的回调
        public int RemainCount => q.Count;//剩余可复用对象数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="prefab">预制体</param>
        /// <param name="parent">父物体</param>
        /// <param name="onInstantiated">创建时的回调(预制体实例化时)</param>
        public Pool(T prefab, Transform parent, System.Action<T> onInstantiated = null, int initialSize = 0){
            if(prefab == null){
                Debug.LogError("对象池对象预制体为空，创建对象池失败！");
                return;
            }
            this.prefab = prefab;
            this.parent = parent;
            this.onInstantiated = onInstantiated;
            q = new Queue<T>();
            
            List<T> initedObjects = new List<T>();
            for(int i=0; i<initialSize; i++){
                initedObjects.Add(Create());
            }
            foreach(var obj in initedObjects){
                Destroy(obj);
            }
        }

        /// <summary>
        /// 创建（获取）一个对象
        /// </summary>
        /// <returns></returns>
        public T Create(System.Action<T> onCreated = null){
            T t = null;
            while(t == null && q.Count > 0){
                t = q.Dequeue();
            }
            if(t == null){
                t = GameObject.Instantiate<T>(prefab, parent);
                onInstantiated?.Invoke(t);
            }
            t.gameObject.SetActive(true);
            onCreated?.Invoke(t);
            return t;
        }

        /// <summary>
        /// 销毁（回收）一个对象
        /// </summary>
        /// <param name="t"></param>
        public void Destroy(T t){
            if(t == null){
                return;
            }
            t.transform.parent = parent;
            t.gameObject.SetActive(false);
            q.Enqueue(t);
        }
    }
}
