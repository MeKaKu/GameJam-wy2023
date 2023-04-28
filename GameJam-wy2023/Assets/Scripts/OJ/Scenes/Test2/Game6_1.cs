using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class Game6_1 : RiddlePanelBase
    {
        
        // Start is called before the first frame update
        void Start()
        {
            // _b
            //     _h
            // _i
            //     _u
            // _y
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(Game_6._b+"+"+Game_6._h+"+"+Game_6._i+"+"+Game_6._u+"+"+Game_6._y);
            if (Game_6._b &&Game_6. _h &&Game_6. _i &&Game_6. _u &&Game_6. _y)
            {
                Result(true);
            }
        }
        
    }
}
