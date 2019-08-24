using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsHand : MonoBehaviour
{
    public float moveThreshold = 0.01f;
    public HandAnimEvent animEvent;
    public Animator anim;
    public int RandomRange;
    public KeyCode a,b,c,d,e;
    public Transform target;
    public bool IsTestAnim;
    public Vector3 HandMoveOffset;
    public HandState state;
    public bool IsAttack;
    public bool IsUp;
    public Vector3 originalPos;
    [SerializeField] Vector3 _targetPosition;
    [Range(0.5f, 10f)]public float CanBeAttackTime;
    public bool IsCanBeAttack;
    public bool IsPlayerAttack;
    public bool IsDebugMode;

    private static ThisIsHand _instance;
    public static ThisIsHand Instance
    {
        get
        {
            if (_instance == null) _instance = GameObject.FindObjectOfType<ThisIsHand>();
            return _instance;
        }
    }
    public enum HandState
    {
        Idle,
        moveUp,
        attack,
        moveDown
    }

    [Range(0.01f,1f)]public float HandMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (animEvent == null) animEvent = transform.GetChild(0).GetComponent<HandAnimEvent>();
        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTestAnim)
        {
            SlamAttack();
        }
        //if(Input.GetMouseButtonDown(0) && IsUp==false)
        if (gear.CanHandMove && IsUp == false)
        {
            gear.CanHandMove = false;
            StartCoroutine(MoveUp(() => SlamAttack()));
        }
        

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Default"))
        {
            if (IsUp && !animEvent.IsAttacking)
            {
                Debug.Log("DownAnim");

                StartCoroutine(MoveDown());
            }
        }
        if (IsDebugMode)
        {
            DebugFakeAttack();
            DebugFakeClick();
            DebugFakeMiddleAttack();
        }
        if (animEvent.IsCanBeAttack && IsPlayerAttack)
        {
            OnPlayerAttack();

        }

        IsPlayerAttack = false;


    }

    private void DebugFakeMiddleAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(MoveUp(() => MiddleFingerAttack()));
        }
    }

    private void OnPlayerAttack()
    {
        Debug.LogError("HandToDo:被玩家攻擊");
        anim.SetBool("OnAttack", true);

        animEvent.CreateGo(animEvent.particlePrefab[1], 3, this.transform.position);
        animEvent.CreateGo(animEvent.soundPrefab[1], 3, this.transform.position);
    }

    private void DebugFakeClick()
    {
        if(Input.GetMouseButtonDown(0))
        StartCoroutine(MoveUp(() => SlamAttack()));
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            IsPlayerAttack = true;
    }

    public void DebugFakeAttack()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            IsPlayerAttack = true;

            
        }
 
    }

    public void StartMoveDown()
    {
        StartCoroutine(MoveDown());
    }

    //public IEnumerator CountDown(float time)
    //{
    //IsCanBeAttack = true;
    //float targetTime = time;
    //while(targetTime >=0)
    //{
    //Debug.Log($"CountDown{targetTime}");
    //targetTime -= Time.deltaTime;
    //yield return null;
    //}

    //IsCanBeAttack = false;
    //}



    public IEnumerator MoveUp(Action attack)
    {
        Debug.Log("HandMoveUp");
        animEvent.JohnCenaSFX();
        originalPos = transform.position;
        _targetPosition = target.position;
        var pos = HandMoveOffset + target.position;
        var targetPos = new Vector3(transform.position.x, pos.y, transform.position.z);
        //Debug.LogError(target.position);
        while (transform.position.y <= targetPos.y-0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, HandMoveSpeed);
            Debug.Log("HandUPing");
            yield return null;
        }
        transform.position = targetPos;
        IsUp = true;
        attack.Invoke();
        animEvent.IsAttacking = true;


    }



    public IEnumerator MoveDown(System.Action next = null)
    {
        Debug.Log("HandDown");
        var pos = originalPos-HandMoveOffset;
        var targetPos = new Vector3(transform.position.x, pos.y, transform.position.z);
        while (transform.position.y > targetPos.y+0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, HandMoveSpeed);
            yield return null;
            Debug.Log("HandDowning");
        }
        transform.position = targetPos;
        IsUp = false;
    }

    //bool AnimatorIsPlaying()
    //{
    //    return anim.GetCurrentAnimatorStateInfo(0).length >
    //           anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    //}

    public void SlamAttack()
    {
        
        anim.SetTrigger("Slam");
        
    }

    public void MiddleFingerAttack()
    {
        anim.SetTrigger("MiddleFinger");
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
        int num = UnityEngine.Random.Range(0, RandomRange);

        anim.SetTrigger(num);
    }


}
