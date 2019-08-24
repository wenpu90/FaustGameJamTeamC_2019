﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    float originalPointY;
    float fallingPointY;

    float posX,posY,posZ;

    bool isPulling = false;

    void Start()
    {
        originalPointY=transform.position.y;
        fallingPointY = transform.position.y-10;
        posX = this.gameObject.transform.position.x;
        posY = this.gameObject.transform.position.y;
        posZ = this.gameObject.transform.position.z;
    }

    public void GoFalling()
    {
        if (!isPulling)
        {
            StartCoroutine(GearFalling());
        }
    }

    IEnumerator GearFalling()
    {
        isPulling = true;
        posY = fallingPointY;
        StartCoroutine( DoTransform(2, transform.position, new Vector3(posX, posY, posZ)));

        yield return new WaitForSeconds(5);

        posY = originalPointY;
        StartCoroutine(DoTransform(5, transform.position, new Vector3(posX, posY, posZ)));

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