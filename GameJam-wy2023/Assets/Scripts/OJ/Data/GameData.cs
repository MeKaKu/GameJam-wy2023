using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [System.Serializable]
    public class GameData
    {
        public string playerName;
        public bool isMale;
        public float hp = 1f;
        public int soul = 3;
        public Backpack backpack = new Backpack();
    }
}
