using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [System.Serializable]
    public class GameData
    {
        public Vector3 pos = Vector3.zero;
        public Backpack backpack = new Backpack();
    }
}
