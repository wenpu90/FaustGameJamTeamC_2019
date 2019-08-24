using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimEvent : MonoBehaviour
{
    public ParticleSystem[] particles;
    [Range(0.001f, 0.1f)]public float shakeAmount;
    [Range(0.1f, 5f)] public float shakeDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandSlamEvent()
    {
        particles[0].Play(true);
        StartCoroutine(CameraShake());
        
    }

    public IEnumerator CameraShake()
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
}
