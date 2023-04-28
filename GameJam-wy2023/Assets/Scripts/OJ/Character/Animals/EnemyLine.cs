using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLine : MonoBehaviour
{
    public LineRenderer lineRenderer; 
    // Line Renderer组件
    public float laserWidth = 0.1f;
    // 激光的宽度
    private Vector3 targetPoint; // 激光的结束点
    void Update() { 
        // 获取鼠标的位置
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10; // 调整Z轴，确保激光在摄像机前面 // 将屏幕坐标转换成世界坐标，得到激光的结束点
        targetPoint = Camera.main.ScreenToWorldPoint(mousePos); 
        // 设置Line Renderer的起始点和结束点
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, targetPoint); 
        // 设置Line Renderer的宽度
        lineRenderer.startWidth = laserWidth; 
        lineRenderer.endWidth = laserWidth; 
    }
}
