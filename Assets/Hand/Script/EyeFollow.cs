using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public Transform target;
    public bool IsLerp;
    [Range(0.1f,10f)]public float LerpAmount = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        var movePos = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        if (!IsLerp)
        {
            this.transform.position = target.position;
        }
        else
        {
            transform.position = Vector3.Lerp(this.transform.position, movePos, LerpAmount * Time.deltaTime);
        }
    }
}
