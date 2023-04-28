using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUllet_LineRender : MonoBehaviour
{
    public Transform current;
    public GameObject explosion;
    //子弹等级
    public int level;

    private LineRenderer lineRender;
    //修正的位置
    private Vector3 offset_y = Vector3.up * 0.7f;
	// Use this for initialization
	void Start ()
    {
        lineRender = GetComponentInChildren<LineRenderer>();
        lineRender.widthMultiplier = Mathf.Pow(2, level);
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, current.position + offset_y);
        explosion.transform.position = current.position;
        Destroy(gameObject, 1f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(current == null)
        {
            return;
        }
        
        lineRender.SetPosition(1, current.position + offset_y);
    }
}
