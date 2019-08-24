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
    public Vector3 dangerEffOffset;
    public List<GameObject> soundPrefab;
    public List<GameObject> particlePrefab;
    public bool IsCanBeAttack;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
        if (parent == null) parent = this.transform.parent;
    }
    private void OnEnable()
    {
        if (anim == null) anim = GetComponent<Animator>();

        if (soundPrefab == null) soundPrefab = new List<GameObject>();
        if (particlePrefab == null) particlePrefab = new List<GameObject>();
        soundPrefab.Add( (GameObject)Resources.Load("Hand/DangerSFX"));
        soundPrefab.Add( (GameObject)Resources.Load("Hand/DangerSFX"));
        particlePrefab.Add((GameObject)Resources.Load("Hand/危!"));
        particlePrefab.Add((GameObject)Resources.Load("Hand/再來啊!"));
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

    public void IsCanBeAttackTrue()
    {
        IsCanBeAttack = true;
        Debug.Log("手可以被攻擊");

    }
    public void IsCanBeAttackFalse()
    {
        IsCanBeAttack = false;
        anim.SetBool("OnAttack",false);
        Debug.Log("手不能被攻擊");

    }

    private void HandSlamEvent()
    {
        particles[0].Play(true);
        StartCoroutine(CameraShake());
        
    }

    public void HandMoveDown()
    {
        Debug.Log("手往下");
        ThisIsHand.Instance.StartMoveDown();
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

    public void DangerParticle()
    {

        CreateGo(soundPrefab[0], 3,Vector3.zero);
        CreateGo(particlePrefab[0], 3, target.transform.position + dangerEffOffset);
    }

    public void CreateGo(GameObject go, float destroyTime, Vector3 spawnPos)
    {
        var so = Instantiate(go);
        so.transform.position = spawnPos;
        Destroy(so, destroyTime);
    }


}
