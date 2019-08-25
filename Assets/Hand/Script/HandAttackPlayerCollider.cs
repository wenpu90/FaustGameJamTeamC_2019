using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttackPlayerCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogError("手攻擊到玩家了");
            collision.rigidbody.AddForce(new Vector3(1, 2, 1) * 3000);
        }
            //ThisIsHand.Instance.IsPlayerAttack = true;
    }
}
