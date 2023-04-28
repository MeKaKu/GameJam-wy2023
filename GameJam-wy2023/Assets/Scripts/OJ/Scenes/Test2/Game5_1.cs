using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class Game5_1 : MonoBehaviour
    {
        public static bool T1 = false;
        public static bool T2 = false;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
           
        }
        

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.transform.name);
             if (other.transform.name == "t1" && transform.transform.name == "t1")
             {
                 T1 = true;
             }
             if (other.transform.name == "t2" && transform.transform.name == "t2" )
             {
                 T2 = true;
             }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            T1 = false; 
            T2 = false;
        }
    }
}
