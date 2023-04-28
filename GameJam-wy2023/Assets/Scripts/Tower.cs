using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gunPos;//�ӵ�λ��
    public GameObject bullet;//�ӵ�
    public Transform current;//��ǰ����Ŀ��
    public float atkRange;//������Χ
    public float atkcoolling;//������ȴ
    public int atkPower;//������
    public Collider[] monsters;//��Χ�ڵĹ���
    public int cost_Energy;//���컨������
    public int level_tower = 1;//�������ȼ�

    private float timer;//��ʱ��
    private int atk_lv1;//�ȼ�������

    void Start()
    {
        timer = 0;//��ʱ������
        atk_lv1 = atkPower;//�趨�ȼ�1ʱ�Ĺ�����
    }

    // Update is called once per frame
    void Update()
    {
        FindMonster();//���ֹ���
        AttackMonster();
    }


    //Ѱ�ҵ���
    void FindMonster()
    {
        monsters = Physics.OverlapCapsule
             (transform.position, transform.position - Vector3.up * 5, atkRange, LayerMask.GetMask("Monster"));
        if(monsters.Length>0)
        {
            current = monsters[0].transform;
        }


    }

    //��������

    void AttackMonster()
    {
        timer += Time.deltaTime;//ʱ������
        if(timer>=atkcoolling&&monsters.Length>0)
        {
            //��ָ����λ�ô����ӵ�Ԥ����
            GameObject temp = Instantiate(bullet, gunPos.position, Quaternion.identity);
            if(temp.tag=="Bullet")
            {
                Bullet bullet = temp.GetComponent<Bullet>();    
                bullet.current = current;
                bullet.level = level_tower; 
            }

            if(temp.tag=="Line")
            {
                BUllet_LineRender bullet_LineRender = temp.GetComponent<BUllet_LineRender>();
                bullet_LineRender.current = current;
                bullet_LineRender.level = level_tower;
            }
            timer = 0;

        }
        
        //�����Ѫ
        //current.GetComponent<Monster>().GetDamage(atkPower); 

        //��ʱ������
    }

   
}
