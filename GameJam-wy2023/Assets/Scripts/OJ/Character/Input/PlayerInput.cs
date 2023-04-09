using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class PlayerInput : IInputHandler
    {
        public Vector2 GetMouseMove()
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }

        public Vector2 GetMoveDir()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            SquareToCircle(ref x, ref y);
            return new Vector2(x, y);
        }

        public bool Jump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool SpeedUp()
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }

        //将正方形内坐标转换为单位圆内坐标
        public static void SquareToCircle(ref float u,ref float v){
            float x = u;
            float y = v;
            u = x*Mathf.Sqrt(1 - (y * y) / 2.0f);
            v = y*Mathf.Sqrt(1 - (x * x) / 2.0f);
        }
    }
}
