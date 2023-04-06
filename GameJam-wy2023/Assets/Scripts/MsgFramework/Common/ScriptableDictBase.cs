using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DyeFramework.Common
{
    public class ScriptableDictBase<K, V> : ScriptableObject where K : class where V : class
    {
        public List<Article<K, V>> articles = new List<Article<K, V>>();
        Dictionary<K, V> dict = new Dictionary<K, V>();

        public void Init(){
            foreach(var article in articles){
                if(!dict.ContainsKey(article.id)){
                    dict.Add(article.id, article.value);
                }
            }
        }
        public V Get(K key){
            dict.TryGetValue(key, out V value);
            return value;
        }
    }
    [System.Serializable]
    public class Article<K, V>where K : class where V : class{
        public K id;
        public V value;
    }
}
