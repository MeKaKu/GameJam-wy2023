using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform current;//��ǰĿ��
    public GameObject explosion;//��ը
    public int speed;//�ӵ����ٶ�
    public int level;//�ӵ��ȼ�

    public Rigidbody rgb;
    private TrailRenderer traRender;//�켣��Ⱦ
    private ParticleSystem particle;//����ϵͳ

    void Start()
    {
        Debug.Log("Bullet component found: " + GetComponent<Bullet>());
        rgb = GetComponent<Rigidbody>();//��ȡ�������
        traRender = GetComponentInChildren<TrailRenderer>();//��ȡ�켣��Ч���
        particle = transform.Find("Trail").GetComponent<ParticleSystem>();//��ȡ�ض��������µ�������Ч���

        traRender.widthMultiplier = level * 2;//���ù켣��Ч����
        ParticleSystem.MainModule mm = particle.main;//�½�������Ч
        mm.startSize = new ParticleSystem.MinMaxCurve(0.6f + level * 0.3f, 0.8f + level * 0.4f);
        //����������Ч����

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
