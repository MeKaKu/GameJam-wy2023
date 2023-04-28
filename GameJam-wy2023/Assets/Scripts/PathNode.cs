using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public PathNode nextNode;
    void Start()
    {
        GameObject[] gb = GameObject.FindGameObjectsWithTag("Path");
        int a = int.Parse((this.gameObject.name.Remove(0, 8)));
        if (a < gb.Length)
        {
            nextNode = gb[a].GetComponent<PathNode>();
        }
    }
}