using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [System.Serializable]
    public class GameData
    {
        public string playerName = "";
        public bool isMale;
        public float hp = 1f;
        public int soul = 3;
        public int sceneId = 2;//玩家所在场景
        public string position = "0,0,0";//玩家位置信息
        public List<int> completedFlows = new List<int>();
        public List<int> completedRiddles = new List<int>();
        public List<int> completedInteractions = new List<int>();
        public Backpack backpack = new Backpack();
    }
}
