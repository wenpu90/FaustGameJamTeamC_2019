using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamStartAnim : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("PlayStartAnim");
        Debug.Log("AnimStart");
    }

    private void TurnOffAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

}
