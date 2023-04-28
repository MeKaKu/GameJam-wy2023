using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatButton : MonoBehaviour
{
    public GameObject tower;

    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(CreateTower);
    }

    void CreateTower()
    {
        Vector3 pos = UiManager.instance.currentBase.position+Vector3.up*2.8f;
        GameObject temp = Instantiate(tower, pos, Quaternion.identity);
        temp.transform.SetParent(UiManager.instance.currentBase);

        //取消底座选择功能
        UiManager.instance.currentBase.GetComponent<Collider>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
