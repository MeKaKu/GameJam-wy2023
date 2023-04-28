using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float up;
    private float down;
    private float left;
    private float right;

    public float speed;

    bool isL = true;
    bool isR = true;
    bool isU = true;
    bool isD = true;
    // Start is called before the first frame update
    void Start()
    {
        up = Screen.height-5;
        down = 5;
        left = 0;
        right = Screen.width-5;
    }

    // Update is called once per frame
    void Update()
    {
        CameraCtrl();
        Debug.Log(Input.mousePosition.y);
    }
    void CameraCtrl()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(Input.mousePosition.x<=left&&isL)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if(Input.mousePosition.x>=right&&isR)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if(Input.mousePosition.y<=down&&isD)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else if(Input.mousePosition.y>=up&&isU)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if(transform.position.x<=20)
        {
            isL = false;
        }
        else
        {
            isL = true;
        }
        if (transform.position.x >= 40.5f)
        {
            isR = false;
        }
        else
        {
            isR = true;
        }
        if (transform.position.z <= 3.4f)
        {
            isD = false;
        }
        else 
        {
            isD = true;
        }
        if(transform.position.z>=41.8f)
        { 
            isU = false;
        }
        else
        {
            isU = true;
        }
    }
}
