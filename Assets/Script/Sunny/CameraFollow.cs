using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Quaternion iniRotation;
    private bool isInit;
    void Awake()
    {
        isInit = false;
        iniRotation = transform.rotation;
    }
    void Update()
    {
        if(!isInit)
        {
            iniRotation = transform.rotation;
            isInit = true;
            Debug.Log("CamInit");
        }
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z-3);
    }
}
