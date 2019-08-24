using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    float originalPointY;
    float fallingPointY;

    float posX,posY,posZ;

    bool isPulling = false;

    void Init()
    {
        originalPointY=transform.position.y;
        fallingPointY = transform.position.y-10;
        posX = gameObject.transform.position.x;
        posZ = gameObject.transform.position.z;
    }

    public void GoFalling()
    {
        if (!isPulling)
        {
            Init();
            StartCoroutine(GearFalling());
        }
    }

    IEnumerator GearFalling()
    {
        isPulling = true;
        StartCoroutine( DoTransform(2, transform.position, new Vector3(posX, fallingPointY, posZ)));

        yield return new WaitForSeconds(2.5f);

        StartCoroutine(DoTransform(4, transform.position, new Vector3(posX, originalPointY, posZ)));
        yield return new WaitForSeconds(4.5f);
        transform.position = new Vector3(10000, 0, 0);
    }

    IEnumerator DoTransform(float duration, Vector3 posStart, Vector3 posEnd)
    {
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

        isPulling = false;
    }

    float CubicEaseOut(float t)
    {
        return ((t = t - 1) * t * t + 1);
    }
}
