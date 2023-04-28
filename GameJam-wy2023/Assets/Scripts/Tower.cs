using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gunPos;//子弹位置
    public GameObject bullet;//子弹
    public Transform current;//当前攻击目标
    public float atkRange;//攻击范围
    public float atkcoolling;//攻击冷却
    public int atkPower;//攻击力
    public Collider[] monsters;//范围内的怪物
    public int cost_Energy;//建造花费能量
    public int level_tower = 1;//防御塔等级

    private float timer;//计时器
    private int atk_lv1;//等级攻击力

    void Start()
    {
        timer = 0;//计时器归零
        atk_lv1 = atkPower;//设定等级1时的攻击力
    }

    // Update is called once per frame
    void Update()
    {
        FindMonster();//发现怪物
        AttackMonster();
    }


    //寻找敌人
    void FindMonster()
    {
        monsters = Physics.OverlapCapsule
             (transform.position, transform.position - Vector3.up * 5, atkRange, LayerMask.GetMask("Monster"));
        if(monsters.Length>0)
        {
            current = monsters[0].transform;
        }


    }

    //攻击敌人

    void AttackMonster()
    {
        timer += Time.deltaTime;//时间流逝
        if(timer>=atkcoolling&&monsters.Length>0)
        {
            //在指定的位置创建子弹预设体
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
        
        //怪物扣血
        //current.GetComponent<Monster>().GetDamage(atkPower); 

        //计时器归零
    }

   
}
