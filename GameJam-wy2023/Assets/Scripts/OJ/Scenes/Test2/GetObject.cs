using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
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
            if (other.transform.tag == "Player")
            {
                
                PlayerPrefs.SetInt("isPlayerCollision",1);
                PlayerPrefs.SetFloat("GameObjectPositionX",transform.position.x);
                PlayerPrefs.SetFloat("GameObjectPositionY",transform.position.y);
                PlayerPrefs.SetFloat("GameObjectPositionZ",transform.position.z);
                PlayerPrefs.SetString("ObjectNameMomo",transform.name);
                if (Input.GetKey(KeyCode.F))
                {
                     PlayerPrefs.SetString("ObjectName",transform.name);
                     PlayerPrefs.SetInt("isPlayerCollision",0);
                   
                     gameObject.SetActive(false);
                     // Destroy(gameObject);
                }
                
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.transform.tag == "Player")
            {
                PlayerPrefs.SetInt("isPlayerCollision",0);
            }
        }
    }
}
