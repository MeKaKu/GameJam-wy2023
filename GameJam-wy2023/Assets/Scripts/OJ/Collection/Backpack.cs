using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [System.Serializable]
    public class Backpack
    {
        public Dictionary<int, BackpackItem> itemsDict = new Dictionary<int, BackpackItem>();

        public bool HasItem(int id){
            if(itemsDict.ContainsKey(id) && itemsDict[id].count > 0){
                return true;
            }
            return false;
        }

        public void AddItem(int id, int count = 1){
            if(!itemsDict.ContainsKey(id)){
                itemsDict.Add(id, new BackpackItem(){id = id, count = count});
            }
            else{
                itemsDict[id].count += count;
            }
        }

        public bool RemoveItem(int id, int count = 1){
            BackpackItem item;
            if(itemsDict.TryGetValue(id, out item) && item.count >= count){
                item.count -= count;
                if(item.count <= 0){
                    itemsDict.Remove(id);
                }
                return true;
            }
            return false;
        }
    }

    [System.Serializable]
    public class BackpackItem{
        public int id;
        public int count;
    }
}
