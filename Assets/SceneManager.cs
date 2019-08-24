using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public int SceneIndex;

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
    }
}
