using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public interface IInputHandler
    {
        //Signals
        public bool SpeedUp();
        public bool Jump();

        public Vector2 GetMoveDir();

        public Vector2 GetMouseMove();
    }
}
