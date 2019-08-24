using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gear : MonoBehaviour, IPointerClickHandler
{
    public float start_speed=1.0f;
    bool is_out = false;
    public static bool isflyout = false;

    float radian = 0; // 弧度
    Vector3 oldPos; // 开始时候的坐标
    float perRadian = 0.03f; // 每次变化的弧度(變化速度)，最好小於0.07
    float radius = 0.2f; //高度變化，最好小於0.3
    int plus_minus = 1;

    void Start()
    {
        radius = Random.Range(0.05f, 0.3f);
        perRadian= Random.Range(0.03f, 0.07f);
        oldPos = transform.position; // 保存最初的位置
        if (Random.Range(-5, 5) % 2 == 1)
            plus_minus = -1;
    }

    private float dTime = 0;
    void Update()
    {
        if (!is_out)
        {
            //////////////////////////////////////////////////////////////////////////////////////////

            radian += perRadian; // 弧度每次加0.03
            float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
            transform.position = oldPos + new Vector3(0, dy, 0);

            //////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                start_speed = start_speed + Input.GetAxis("Mouse ScrollWheel");
                if (start_speed < -3)
                    start_speed = -3;
                else if (start_speed > 3)
                    start_speed = 3;
            }
            this.transform.Rotate(0, start_speed* plus_minus, 0);

        }
        
    }

    public GameObject fallingObj; 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2 && !is_out)
        {
            //Debug.Log("eventData.clickCount == 2");
            transform.position = new Vector3(10000, 0, 0);
            gearFlyOut();

            Invoke("PutBackObject", 10);
        }
        if (eventData.button == PointerEventData.InputButton.Left && !is_out)
        {
            //Debug.Log("Left");
            fallingObj.transform.position = transform.position;
            transform.position = new Vector3(10000, 0, 0);
            fallingObj.GetComponent<Falling>().GoFalling();

            Invoke("PutBackObject", 6);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Middle");

        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right");

        }
    }


    public void PutBackObject()
    {
        transform.position = oldPos;
        is_out = isflyout = false;
        gear_boom.GetComponent<Rigidbody>().useGravity = false;
        gear_boom.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public GameObject gear_boom;
    public void gearFlyOut()
    {
        //GearFlyOut.instance.InitMove(this.transform.position, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2));
        is_out = isflyout = true;
        
        gear_boom.transform.position = new Vector3(oldPos.x, oldPos.y+0.5f, oldPos.z);
        gear_boom.GetComponent<Rigidbody>().useGravity = true;

        Debug.Log(gear_boom.transform.up * 2);
        gear_boom.GetComponent<Rigidbody>().AddForce(new Vector3(1,2,1));
        
    }

    

}
