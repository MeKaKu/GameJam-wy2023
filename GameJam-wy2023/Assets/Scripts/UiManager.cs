using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    // Start is called before the first frame update
    public GameObject baseEffect;
    public Transform currentBase;

    private RaycastHit hifInfo;
    private static GameObject baseE;
    private static Tower currentTower;
    

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if(baseE !=null)
        {
            Destroy(baseE);
        }
        else
        {
            baseE = Instantiate(baseEffect);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            {
            RaySelect();
            //得到防御塔信息
        }
    }
    void RaySelect()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hifInfo,100,LayerMask.GetMask("Tower")))
        {
            //升级操作
        }else if (Physics.Raycast(ray, out hifInfo, 100, LayerMask.GetMask("TowerBase")))
        {
            baseE.transform.position = hifInfo.transform.position + new Vector3(-0.05f, 2.7f, 0);
            currentBase = hifInfo.transform;
        }
    }
}
