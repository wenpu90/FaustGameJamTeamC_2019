using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimEvent : MonoBehaviour
{
    public bool IsAttacking;
    public ParticleSystem[] particles;
    [Range(0.001f, 0.1f)]public float shakeAmount;
    [Range(0.1f, 5f)] public float shakeDuration;
    public Transform target;
    public Transform parent;
    private bool IsFaceTO;
    public Vector3 HandMoveOffset;
    [Range(0.01f, 1f)] public float HandMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
        if (parent == null) parent = this.transform.parent;
    }

    public void SetAttackingTrue()
    {
        IsAttacking = true;
    }
    public void SetAttackingFalse()
    {
        IsAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Anim 事件開關
        if(IsFaceTO)
        {
            FaceTo();
        }

    }

    private void HandSlamEvent()
    {
        particles[0].Play(true);
        StartCoroutine(CameraShake());
        
    }

    private IEnumerator CameraShake()
    {
        var cam = Camera.main;
        if(cam != null)
        {
            var time = shakeDuration;
            var oPos = cam.transform.localPosition;
            while ((time -= Time.deltaTime )>0)
            {

                time -= Time.deltaTime;
                
                cam.transform.localPosition = oPos + Random.insideUnitSphere * shakeAmount;
                yield return null;
            }
            cam.transform.localPosition = oPos;
        }
    }

    private void FaceTo()
    {
        if (target == null) return;

        var pos = target.position;
        pos = new Vector3(pos.x, this.transform.position.y, pos.z);
        var dir = pos - this.transform.position;

        this.transform.LookAt(pos);
    }

    public void SetFaceToTrue()
    {
        IsFaceTO = true;
    }
    public void SetFaceToFalse()
    {
        IsFaceTO = false;
    }

    public void DangerEff()
    {

    }


}
