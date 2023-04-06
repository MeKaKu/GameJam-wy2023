using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DyeFramework.Modules;
public class Test : MonoBehaviour
{
    void Awake(){
        
    }
    private void OnEnable() {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            UIManager.ShowPanel("TestUIPanel");
        }
    }
}
