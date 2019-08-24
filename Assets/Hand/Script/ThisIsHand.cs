using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsHand : MonoBehaviour
{

    public Animator anim;
    public int RandomRange;
    public KeyCode a,b,c,d,e;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PlayAnimRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FaceTo();
            Slam();
        }
    }

    public void Slam()
    {
        anim.SetTrigger("Slam");
    }

    public void FaceTo()
    {
        if (target == null) return;

        var pos = target.position;
        pos = new Vector3(pos.x, this.transform.position.y, pos.z);
        var dir = pos - this.transform.position;

        this.transform.LookAt(dir);
    }

    public void PlayAnimRandom()
    {
        int num = Random.Range(0, RandomRange);

        anim.SetTrigger(num);
    }


}
