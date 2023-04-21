using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [System.Serializable]
    public class GameData
    {
        public string playerName;
        public Backpack backpack = new Backpack();
    }
}
