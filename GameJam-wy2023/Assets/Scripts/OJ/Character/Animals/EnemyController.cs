using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
namespace OJ
{
    public class EnemyController : MonoBehaviour
    {
        [Header("跑步速度")]
        public float runSpeed;
        [Header("行走速度")]
        public float walkSpeed;
        [Header("攻击范围")]
        public float attackRange;
        [Header("生命值")]
        public int life;
        [Header("攻击力")]
        public int attack;
        [Header("攻击间隔")]
        public float attackTime;
        [Header("主角")]
        public GameObject player;
        [Header("视野")]
        public float fov;
        [Header("视野距离")]
        public float fovLint;
        //ai巡逻目的地，数组填
        [Header("巡逻目的地(按顺序填写)")]
        public List<Vector3> aiList;
        //下标
        private int _aiListIndex = 0;
        //是否进入视野
        private bool _isFov = false;
        //更新主角位置
        private Vector3 _playerTransform;
        //归零
        private Vector3 _restPosition = Vector3.zero;
        //寻路
        private NavMeshAgent _meshAgent;
        //碰撞tag
        private SphereCollider _sphereCollider;
        //射击开关
        private bool _isShot;
        //记录间隔
        private float _timeAdd;
        //记录射击位置
        private Vector3 _playerPosition;
        //存储动画参数
        //private HashIDs _hash;
        // Start is called before the first frame update
        void Start()
        {
            _meshAgent = GetComponent<NavMeshAgent>();
            // _sphereCollider = GetComponent<CapsuleCollider>();
            _sphereCollider = GameObject.Find("/Enemy/test").GetComponent<SphereCollider>();
            _sphereCollider.radius = fovLint;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        void LateUpdate()
        {
          
            Move();
            AutoMove();
            AutoAttack();
        }

        void Move()
        {
        }
        //寻路
        void AutoMove()
        {
            if (_isFov)
            {
                //方向 。敌人指向主角
                Vector3 vector3 = player.transform.position - transform.position;
                //Debug.Log("vector3"+111111);
                //角度差
                float playerAngle = Vector3.Angle(vector3, transform.forward);
                if (playerAngle < fov)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position+transform.up,vector3.normalized,out hit,_sphereCollider.radius))
                    {
                        if (hit.collider.transform.CompareTag("Player"))
                        {
                            //_isFov = false;
                            _isShot = true;
                            _playerTransform = player.transform.position;
                            _meshAgent.SetDestination(_playerTransform);
                            _meshAgent.speed = runSpeed;
                        }
                    }
                }

            }
            else
            {
                _isShot = false;
                _meshAgent.SetDestination(aiList[_aiListIndex]);
                if (Vector3.Distance(aiList[_aiListIndex],transform.position)<1f)
                {
                    if (aiList.Count == _aiListIndex+1)
                    {
                        _aiListIndex = 0;
                    }
                    else
                    {
                        _aiListIndex++;
                    }
                     
                }
                
            }
        }
        //攻击
        void AutoAttack()
        {
            
            if (_isShot)
            {
                if (_timeAdd == 0f)
                {
                    _playerPosition = player.transform.position;
                }
                _timeAdd = _timeAdd + Time.deltaTime;
                if (_timeAdd>attackTime)
                {
                    
                    RaycastHit hit;
                    
                    bool b=Physics.Raycast(transform.position + transform.up,
                        (_playerPosition - transform.position).normalized, out hit, attackRange);
                    if (!b)
                    {
                        if (Vector3.Distance(player.transform.position,transform.position)<1f)
                        {
                            if (life <= 0)
                            {
                                _isFov = false;
                                Debug.Log("die");
                                player.SetActive(false);
                            }
                            else
                            {
                                life = life- attack;
                            }
                        }
                        return;
                    }
                    if (hit.collider.transform.CompareTag("Player")  || Vector3.Distance(player.transform.position,transform.position)<1f)
                    {
                        if (life <= 0)
                        {
                            _isFov = false;
                            Debug.Log("die");
                            player.SetActive(false);
                        }
                        else
                        {
                            life = life- attack;
                        }
                    }
                    _timeAdd = 0f;
                }
            }
           
            // _timeAdd = 0f;

                // if (-attackRange<= vector3.x || vector3.x <= attackRange)
            // {
            //     
            // }
        }
        //进入视野
         private void OnTriggerEnter(Collider other)
         {
             //Debug.Log(other.transform.tag);
             if (other.transform.tag =="Player")
             {
                 Debug.Log(other.transform.tag+"进入视野检测区");
                 _isFov = true;
            
             }
         }
        //
        //退出视野
        private void OnTriggerExit(Collider other)
        {
            
            if (other.transform.tag == "Player")
            {
                _isFov = false;
                Debug.Log(other.transform.tag+"退出视野检测区");
                //PlayerPrefs.SetString("isObjectName",other.name);
                // PlayerPrefs.SetString("bag",JsonUtility.ToJson(prefabs));
                // Destroy(other.gameObject);
                // Debug.Log(player.transform.position.x);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            // Destroy(g);
        }
    }
}
