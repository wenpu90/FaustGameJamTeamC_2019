using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollow : MonoBehaviour
{
    public Transform target;
    public bool IsLerp;
    public float LerpAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        if(!IsLerp)
        {
            this.transform.position = target.position;
        }
        else
        {
            transform.position = Vector3.Lerp(this.transform.position, target.position, LerpAmount * Time.deltaTime);
        }
    }
}
