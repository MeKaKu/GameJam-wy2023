using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OJ
{
    public class GetObject : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionStay(Collision other)
        {
            //Debug.Log(transform.name);
            if (other.transform.tag == "Player")
            {
                if (Input.GetKey(KeyCode.E))
                {
                     PlayerPrefs.SetString("ObjectName",transform.name);
                     gameObject.SetActive(false);
                    // Destroy(gameObject);
                }
                
            }
        }
    }
}
