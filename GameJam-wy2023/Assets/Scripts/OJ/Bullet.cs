using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform current;//当前目标
    public GameObject explosion;//爆炸
    public int speed;//子弹的速度
    public int level;//子弹等级

    public Rigidbody rgb;
    private TrailRenderer traRender;//轨迹渲染
    private ParticleSystem particle;//粒子系统

    void Start()
    {
        Debug.Log("Bullet component found: " + GetComponent<Bullet>());
        rgb = GetComponent<Rigidbody>();//获取刚体组件
        traRender = GetComponentInChildren<TrailRenderer>();//获取轨迹特效组件
        particle = transform.Find("Trail").GetComponent<ParticleSystem>();//获取特定子物体下的粒子特效组件

        traRender.widthMultiplier = level * 2;//设置轨迹特效参数
        ParticleSystem.MainModule mm = particle.main;//新建粒子特效
        mm.startSize = new ParticleSystem.MinMaxCurve(0.6f + level * 0.3f, 0.8f + level * 0.4f);
        //设置粒子特效参数

    }

    // Update is called once per frame
    void Update()
    {
        var dir = (current.position - transform.position).normalized;
        rgb.velocity = dir * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        explosion.SetActive(true);
        current = collision.transform; 
        Destroy(gameObject);
        speed = 0;

    }
}
