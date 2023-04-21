using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    [System.Serializable]
    public class ArchiveData
    {
        public int archiveId;
        public int percent;
        public DateTime createdTime;
        public DateTime lastTime;
        public DateTime duration;
    }
}
