using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private KeyCode initKey = KeyCode.Escape;
    public GameObject player;
    private Quaternion iniRotation;
    private bool isInit;
    void Awake()
    {

    }

    [Range(0.1f, 10f)] public float LerpAmount;
    [Range(1f, 10f)] public float upDis;
    [Range(1f, 10f)] public float backDis;
    void Update()
    {


        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + upDis, player.transform.position.z - backDis), LerpAmount * Time.deltaTime);

        if (Input.GetKeyDown(initKey))
        {
            TurnOffAnimator();
            Debug.Log("CamInit");
        }
    }

    public void TurnOffAnimator()
    {


        GetComponent<Animator>().enabled = false;
    }
}
