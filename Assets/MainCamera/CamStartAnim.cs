using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamStartAnim : MonoBehaviour
{
    public KeyCode skipStartAnim;

    private void OnEnable()
    {
        GetComponent<Animator>().SetTrigger("PlayStartAnim");
        Debug.Log("AnimStart");
    }

    private void Update()
    {
        //if(Input.GetKeyDown(skipStartAnim)) GetComponent<Animator>().enabled = false;
    }

    public void TurnOffAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

}
