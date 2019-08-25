using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiantHPUI : MonoBehaviour
{
    private  float startDelay = 1f;
    private float intervalTime = 0.2f;
    public List<Image> HPIcons;
    private int HpIndex;

    private void Awake()
    {
        HpIndex = 0;
        foreach (var hp in HPIcons)
        {
            hp.enabled = false;
            HpIndex++;

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowAnim());
    }
    
    private IEnumerator ShowAnim()
    {
        yield return new WaitForSeconds(startDelay);

        foreach(var hp in HPIcons)
        {
            yield return new WaitForSeconds(intervalTime);
            hp.enabled = true;
        }
    }

    public void PlayOnAttackAnim()
    {
        HPIcons[HpIndex - 1].transform.gameObject.GetComponent<Animator>().SetTrigger("OnDamage");
        HpIndex--;
    }
}
