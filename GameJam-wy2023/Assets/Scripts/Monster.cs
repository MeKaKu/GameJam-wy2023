using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //起始路点
    public PathNode startNode;
    //移动速度
    public float speed;
    //转向参照物
    public Transform baseTrans;
    private Transform tran;
    private bool isRotate;
    Quaternion temp;
    Animator animator;

	void Start ()
    {
        tran = transform;
        animator = GetComponent<Animator>();
 
    }
	
	// Update is called once per frame
	void Update ()
    {
   
        Move();
        Rotate();
       
        }

    //移动方法
    private void Move()
    {
            var dir = (startNode.transform.position - tran.position).normalized;
            if (Vector3.Distance(startNode.transform.position, tran.position) < 0.1)
            {
                startNode = startNode.nextNode;
                if (startNode == null)
                {
                    MonsterDie();
                    return;
                }
                isRotate = true;
                baseTrans.LookAt(startNode.transform);
                temp = baseTrans.rotation;
            }
            tran.position += dir * speed * Time.deltaTime;
        
    }
    void Rotate()
    {
        transform.rotation = Quaternion.Lerp(tran.rotation, temp, Time.deltaTime * 5);
            if(tran.rotation==baseTrans.transform.rotation)
        {
            isRotate= false;
        }
    }
    void MonsterDie()
    {
       Destroy(gameObject);
    }
}
