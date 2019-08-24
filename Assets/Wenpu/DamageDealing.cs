using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealing : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PlayDamageCollision");
        if (collision.transform.CompareTag("Enemy"))
        {
            ThisIsHand.Instance.IsPlayerAttack = true;
        }
    }
}
