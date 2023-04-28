using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    public static Spwan instance;
    public PathNode startNode;
    public List<GameObject> monsters;
    private Transform spwanTrans;

    private List<SpwanData> spwanData = new List<SpwanData>();
    public TextAsset SData;

    public int wave = 1;
    private int index = 0;
    private float wait;
    private Vector3 pos;
    private int allWave;
    public int nowWave;

    private void Awake()
    {
        instance = this;
        pos = transform.position;
        SData = Resources.Load("TableData/SpawnData", typeof(TextAsset)) as TextAsset;
        Debug.Log(SData);
        LoadSpwanData();
        allWave = spwanData[spwanData.Count - 1].wave;
        SpwanData mData = spwanData[0];
        wave = mData.wave;
        wait = mData.wait;
    }
    private void Start()
    {
        spwanTrans = transform;
        StartCoroutine("SpwanMonster", wait);
    }
    void LoadSpwanData()
    {
        string[] lineArray = SData.text.Split('\r');
        for (int i = 2; i < lineArray.Length - 1; i++)
        {
            string[] rowArray = lineArray[i].Split(',');
            SpwanData spwan = new SpwanData();
            spwan.wave = int.Parse(rowArray[0]);
            spwan.monsterName = rowArray[1];
            spwan.monsterLevel = int.Parse(rowArray[2]);
            spwan.wait = int.Parse(rowArray[3]);
            spwanData.Add(spwan);
        }
    }
    void CreatMonster(SpwanData mData)
    {
        GameObject monster = Resources.Load("Prefabs/Monster/" + mData.monsterName) as GameObject;
        GameObject go = Instantiate(monster, pos, Quaternion.identity) as GameObject;
        go.GetComponent<Monster>().startNode = startNode;
    }
    IEnumerator SpwanMonster(float s)
    {
        yield return new WaitForSeconds(s);
        CreatMonster(spwanData[index]);
        nowWave = wave;
        index++;
        if(index<spwanData.Count)
        {
            wave = spwanData[index].wave;
            wait = spwanData[index].wait;
            StartCoroutine("SpwanMonster", wait);
        }
    }
}
