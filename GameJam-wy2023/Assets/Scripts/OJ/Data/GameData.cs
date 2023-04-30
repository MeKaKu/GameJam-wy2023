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
        public int hp = 100;
        public int soul = 3;
        public int sceneId = 2;//玩家所在场景
        public string position = "-13.457375526428223,1.7020000219345093,182.16500854492188";//玩家位置信息
        public List<int> completedFlows = new List<int>();
        public List<int> completedRiddles = new List<int>();
        public List<int> completedInteractions = new List<int>();
        public Dictionary<int, int> npcDialogIds = new Dictionary<int, int>();
        //public Backpack backpack = new Backpack();
        public List<int> items = new List<int>();//背包
    }
}
