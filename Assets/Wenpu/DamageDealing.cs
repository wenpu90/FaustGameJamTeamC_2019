using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealing : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //getDamage
        }
    }
}
