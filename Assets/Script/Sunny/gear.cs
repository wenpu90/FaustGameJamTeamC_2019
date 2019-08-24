using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class gear : MonoBehaviour, IPointerClickHandler
{
    public bool canControl=false;
    public static bool CanHandMove = false;
    public float start_speed;
    bool is_out = false;
    public static bool isflyout = false;

    float radian = 0; // 弧度
    Vector3 oldPos; // 开始时候的坐标
    float perRadian; // 每次变化的弧度(變化速度)，最好小於0.07
    float radius; //高度變化，最好小於0.3
    int plus_minus = 1;

    void Start()
    {
        start_speed = Random.Range(0f, 2f);
        radius = Random.Range(0.05f, 0.3f);
        perRadian= Random.Range(1f, 3f);
        oldPos = transform.position; // 保存最初的位置
        if (Random.Range(-5, 5) % 2 == 1)
            plus_minus = -1;
    }

    private float dTime = 0;
    public float mouseWeelSpeed = 1.0f;
    void Update()
    {
        if (!is_out)
        {
            //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, Random.Range(0f, 4f)), transform.position.z);

            //////////////////////////////////////////////////////////////////////////////////////////
            
            radian += perRadian * Time.deltaTime; // 弧度每次加0.03
            float dy = Mathf.Cos(radian) * radius; // dy定义的是针对y轴的变量，也可以使用sin，找到一个适合的值就可以
            transform.position = oldPos + new Vector3(0, dy, 0);
            
            //////////////////////////////////////////////////////////////////////////////////////////

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                mouseWeelSpeed+= Input.GetAxis("Mouse ScrollWheel")*5;
                start_speed = (start_speed* Time.deltaTime) + mouseWeelSpeed;

                if (mouseWeelSpeed < -15)
                    mouseWeelSpeed = -15;
                else if (mouseWeelSpeed > 15)
                    mouseWeelSpeed = 15;
            }
            this.transform.Rotate(0, start_speed* plus_minus , 0);

        }
        
    }

    public GameObject fallingObj; 
    public GameObject handObj; 
    public void OnPointerClick(PointerEventData eventData)
    {
        /*if (eventData.clickCount == 2 && !is_out){ Debug.Log("eventData.clickCount == 2"); }*/
        if (eventData.button == PointerEventData.InputButton.Left && !is_out)
        {
            is_out = isflyout = true;
            //Debug.Log("Left");
            fallingObj.transform.position = transform.position;
            transform.position = new Vector3(10000, 0, 0);
            fallingObj.GetComponent<Falling>().GoFalling();

            Invoke("PutBackObject_L", 6.8f);
        }
        else if (eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Middle");

        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //Debug.Log("Right");
            is_out = isflyout = CanHandMove = true;
            handObj.transform.position = new Vector3(transform.position.x, transform.position.y-3, transform.position.z);
            transform.position = new Vector3(10000, 0, 0);
            gearFlyOut();

            Invoke("PutBackObject_2", 10);
        }
    }


    public void PutBackObject_2()
    {
        transform.position = oldPos;
        is_out = isflyout = false;
        gear_boom.GetComponent<Rigidbody>().useGravity = false;
        gear_boom.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gear_boom.transform.position = new Vector3(10000, 0, 0);
    }
    public void PutBackObject_L()
    {
        transform.position = oldPos;
        is_out = isflyout = false;
    }

    public GameObject gear_boom;
    public void gearFlyOut()
    {
        //GearFlyOut.instance.InitMove(this.transform.position, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2));
        
        gear_boom.transform.position = new Vector3(oldPos.x, oldPos.y+0.5f, oldPos.z);
        gear_boom.GetComponent<Rigidbody>().useGravity = true;

        Debug.Log(gear_boom.transform.up * 2);
        gear_boom.GetComponent<Rigidbody>().AddForce(new Vector3(1,2,1));
        
    }

    

}
