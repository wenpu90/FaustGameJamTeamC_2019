using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBeAttackCollider : MonoBehaviour
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
        //這段在DamageDealing做(Player控制手的開關 IsPlayerAttack=true)


        //if (collision.gameObject.CompareTag("Player"))
        //    ThisIsHand.Instance.IsPlayerAttack = true;


    }
}
