//XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXxx

using UnityEngine;
using System.Collections;

public class GearFlyOut : MonoBehaviour
{
    public static GearFlyOut instance;
    public float time = 0.1f;//代表从A点出发到B经过的时长
    public Vector3 pointA;//点A
    public Vector3 pointB;//点B
    float g = -10f;//重力加速度
    float h = 15;    //高度

    private Vector3 speed;//初速度向量
    private Vector3 Gravity;//重力向量
    void Start()
    {
        if (instance == null)
            instance = this;
        transform.position = new Vector3(10000, 0, 0);
    }
    private float dTime = 0;

    void FixedUpdate()
    {
        if (!gear.isflyout) return;
        //Debug.Log(dTime += Time.fixedDeltaTime);
        Gravity.y = g * (dTime += Time.fixedDeltaTime)*2;
        //模拟位移
        transform.Translate((speed+ Gravity) * Time.fixedDeltaTime);
    }

    public void InitMove(Vector3 point_a, Vector3 point_b)
    {
        pointA = point_a;
        pointB = point_b;
        transform.position = pointA;//将物体置于A点
        //通过一个式子计算初速度
        speed = new Vector3((pointB.x - pointA.x) / time,
            (h - 0.5f * g * time ) / time,
            (pointB.z - pointA.z) / time);
        Gravity = Vector3.zero;//重力初始速度为0
    }
}