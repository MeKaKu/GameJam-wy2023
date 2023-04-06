using System.Collections.Generic;
using UnityEngine;
using System;

namespace DyeFramework.Common{
    //类对象池
    public static class ClassPool
    {
        static Dictionary<Type, Pool<Behaviour>> dict = new Dictionary<Type, Pool<Behaviour>>();

        public static Pool<T> CreatePool<T>(T prefab, Transform parent, System.Action<T> onInstantiated = null, int initialSize = 0) where T : Behaviour{
            Type type = prefab.GetType();
            if(dict.ContainsKey(type)){
                return dict[type] as Pool<T>;
            }
            else{
                Pool<T> pool = new Pool<T>(prefab, parent, onInstantiated, initialSize);
                dict.Add(type, pool as Pool<Behaviour>);
                return pool;
            }
        }
        static Pool<T> GetPool<T>(T prefab)where T : Behaviour{
            Type type = prefab.GetType();
            Pool<Behaviour> pool;
            dict.TryGetValue(type, out pool);
            return pool as Pool<T>;
        }

        public static T Create<T>(Action<T> onCreated = null)where T : Behaviour{
            T t = null;
            return GetPool(t)?.Create(onCreated);
        }

    }
}

