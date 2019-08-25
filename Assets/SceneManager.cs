using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public int SceneIndex;

    private KeyCode Scene1 = KeyCode.F1, scene2= KeyCode.F2, scene3= KeyCode.F3;

    private float timer;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = 0;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if( SceneIndex ==0 && ThisIsHand.Instance.BeAttackCount > 20)
        {
            SceneIndex++;

            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            
        }

        if(SceneIndex == 0 && Input.GetKeyDown(KeyCode.F11))
        {
            SceneIndex++;

            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        if (SceneIndex == 1)
        {
            timer += Time.deltaTime;

            if(timer > 20)
            {
                SceneIndex++;
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }


        if (Input.GetKeyDown(Scene1))
        {
            SceneIndex = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(scene2))
        {
            SceneIndex = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(scene3))
        {
            SceneIndex = 2;
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }
}
