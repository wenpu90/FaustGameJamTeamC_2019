using UnityEngine;
using System.Collections;

public class shaked : MonoBehaviour
{

    public float shake = 0.2f;

    bool shakeSwitch=true;


    private void Update()
    {
        if (shakeSwitch == true)
        {
            gameObject.transform.position = new Vector3(Random.Range(0f, shake * 2f) - shake, transform.position.y, transform.position.z);
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(0f, shake * 1f) - shake);
            shake = shake / 1.05f;
            if (shake < 0.05)
            {
                shake = 0;
                shakeSwitch = false;
            }
        }
    }
}