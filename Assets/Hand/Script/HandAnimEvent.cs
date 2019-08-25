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
    public List<GameObject> BloodParticle;

    public GameObject AttackPlayerCollider, BeAttackByPlayerCollider, MiddleAttackCollider;



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
        soundPrefab.Clear();
        if (particlePrefab == null) particlePrefab = new List<GameObject>();
        particlePrefab.Clear();
        soundPrefab.Add( (GameObject)Resources.Load("Hand/DangerSFX"));
        soundPrefab.Add( (GameObject)Resources.Load("Hand/OnAttackSFX"));
        soundPrefab.Add((GameObject)Resources.Load("Hand/apear"));
        particlePrefab.Add((GameObject)Resources.Load("Hand/危!"));
        particlePrefab.Add((GameObject)Resources.Load("Hand/再來啊!"));
        //SetAttackColliderFalse();
        //SetBeAttackColliderFalse();
        SetAllColliderFalse();
        _SetBloodParticleFalse();
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

    private void _SetBloodParticleFalse()
    {
        foreach(var p in BloodParticle)
        {
            p.SetActive(false);
        }
    }

    public void SetAllColliderFalse()
    {
        BeAttackByPlayerCollider.SetActive(false);
        AttackPlayerCollider.SetActive(false);
        MiddleAttackCollider.SetActive(false);
    }

    public void SetAttackColliderTrue()
    {
        BeAttackByPlayerCollider.SetActive(false);
        AttackPlayerCollider.SetActive(true);
        MiddleAttackCollider.SetActive(false);
    }
    public void SetAttackColliderFalse()
    {
        BeAttackByPlayerCollider.SetActive(true);
        AttackPlayerCollider.SetActive(false);
        MiddleAttackCollider.SetActive(false);
    }

    public void SetBeAttackColliderTrue()
    {
        BeAttackByPlayerCollider.SetActive(true);
    }
    public void SetBeAttackColliderFalse()
    {
        BeAttackByPlayerCollider.SetActive(false);
    }

    public void SetMiddleAttackColliderTrue()
    {
        Debug.Log("SetMiddleAttackColliderTrue");
        BeAttackByPlayerCollider.SetActive(false);
        AttackPlayerCollider.SetActive(false);
        MiddleAttackCollider.SetActive(true);
    }
    public void SetMiddleAttackColliderFalse()
    {
        Debug.Log("SetMiddleAttackColliderFalse");
        BeAttackByPlayerCollider.SetActive(true);
        AttackPlayerCollider.SetActive(false);
        MiddleAttackCollider.SetActive(false);
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

    private void MiddleFingerEvent()
    {
        Debug.Log("MiddleFingerEvent");
        StartCoroutine(CameraShake(5));

    }

    public void HandMoveDown()
    {
        Debug.Log("手往下");
        ThisIsHand.Instance.StartMoveDown();
    }
    private IEnumerator CameraShake(float middleFingerDuration=0)
    {
        var cam = Camera.main;
        if(cam != null)
        {
            var time = middleFingerDuration == 0 ? shakeDuration : middleFingerDuration;
            var oPos = cam.transform.localPosition;
            while ((time -= Time.deltaTime )>0)
            {

                time -= Time.deltaTime;
                
                cam.transform.localPosition = cam.transform.localPosition + Random.insideUnitSphere * shakeAmount;
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
        if (GameObject.FindGameObjectsWithTag("DDDDanger").Length > 1) return;
        CreateGo(soundPrefab[0], 3,Vector3.zero);
        CreateGo(particlePrefab[0], 3, target.transform.position + dangerEffOffset);
    }

    public void CreateGo(GameObject go, float destroyTime, Vector3 spawnPos)
    {
        var so = Instantiate(go);
        so.transform.position = spawnPos;
        Destroy(so, destroyTime);
    }

    public void JohnCenaSFX()
    {
        if (GameObject.FindGameObjectsWithTag("AnnoyJohnCena").Length > 1) return;
        CreateGo(soundPrefab[2], 10, Vector3.zero);
    }


}
