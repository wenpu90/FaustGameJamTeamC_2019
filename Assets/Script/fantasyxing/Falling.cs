using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    
    public GameObject originalPoint;
    public GameObject fallingPoint;
    Transform originalPointPos;
    Transform fallingPointPos;

    float posX,posY,posZ;

    bool isPulling = false;
    void Start()
    {
        originalPointPos = originalPoint.GetComponent<Transform>();
        fallingPointPos = fallingPoint.GetComponent<Transform>();

        posX = this.gameObject.transform.position.x;
        posY = this.gameObject.transform.position.y;
        posZ = this.gameObject.transform.position.z;



    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)&&!isPulling)
        {
            Invoke("GearFalling", 2f);
            Invoke("GearRecover", 5f);
        }
    }

    private void GearFalling()
    {
        posY = fallingPointPos.position.y;
        StartCoroutine( DoTransform(2, transform.position, new Vector3(posX, posY, posZ)));
    }
    private void GearRecover()
    {
        posY = originalPointPos.position.y;
        StartCoroutine(DoTransform(5, transform.position, new Vector3(posX, posY, posZ)));
    }
    IEnumerator DoTransform(float duration, Vector3 posStart, Vector3 posEnd)
    {
        isPulling = true;
        float timeStart = Time.time;
        float timeEnd = timeStart + duration;

        while (Time.time < timeEnd)
        {
            float t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            float v = CubicEaseOut(t);
            Vector3 position = Vector3.LerpUnclamped(posStart, posEnd, v);
            this.transform.localPosition = position;
            yield return null;
        }
        this.transform.localPosition = posEnd;
        if (Time.time >= timeEnd)
        {
            isPulling = false;
            Debug.Log(isPulling);
        }
    }
    float CubicEaseOut(float t)
    {
        return ((t = t - 1) * t * t + 1);
    }
}
