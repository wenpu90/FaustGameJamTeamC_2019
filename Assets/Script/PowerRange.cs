using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerRange : MonoBehaviour
{
    public GameObject giantHand;
    public float powerRadius;   //可使用半徑
    static Collider[] perviousCols;

    Vector3 curPos, lastPos;
    void Start()
    {
        OnGetGear();
        lastPos = transform.position;
    }
    void Update()
    {
        curPos = transform.position;
        if (curPos != lastPos)
        {
            lastPos = transform.position;
            OnGetGear();
        }
    }

    public void OnGetGear()
    {
        //球形射線檢測,得到powerRadius範圍內所有的物件
        Collider[] cols = Physics.OverlapSphere(giantHand.transform.position, powerRadius);

        if (cols.Length > 0)
        {
            //前一次偵測的物件先回復
            if (perviousCols != null) 
            {
                for (int i = 0; i < perviousCols.Length; i++)
                {
                    if (perviousCols[i].tag.Equals("Ground")) //標籤為Gear的物件
                    {
                        perviousCols[i].GetComponent<gear>().canControl = false;
                        perviousCols[i].GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                }
            }
            for (int i = 0; i < cols.Length; i++)
                if (cols[i].tag.Equals("Ground")) //標籤為Gear的物件
                {
                    //使齒輪能被巨人(滑鼠)操作使用 +
                    //能使用的齒輪變成紅色
                    
                    //當下偵測的物件狀態變化
                    cols[i].GetComponent<gear>().canControl = true;
                    cols[i].GetComponent<MeshRenderer>().material.color = Color.red;
                    Debug.Log(cols[i].gameObject.name);
                    Debug.Log(cols[i].GetComponent<gear>().canControl);
                }
            perviousCols = cols;
        }
    }
}
