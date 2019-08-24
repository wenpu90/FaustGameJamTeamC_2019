using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewController : MonoBehaviour {

	public float velocity = 7;
	public float turnSpeed = 10;
    public Rigidbody rb;
    public float dashForce = 15;
    public float jumpForce = 5;
	Vector2 input;
	float angle;

    public float dashTimer = 0;
    public bool isGrouned = false;
	Quaternion targetRotation;
	Transform cam;
	public Animator anim;
    public GameObject damageDealer;

    public bool canMove = true;
	void Start () {
		cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        RigidBodyInitialize();

    }
	

	void Update ()
    {
        if (!canMove) return;
		GetInput ();

        Jump();
        Attack();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1 || !canMove) {
            return;
		}

        CalculateDirection();
		Rotate();
		Move();
        Dash();
    }
	void GetInput(){
		input.x = Input.GetAxisRaw ("Horizontal");
		input.y = Input.GetAxisRaw ("Vertical");
        anim.SetFloat("Horizontal", input.x);
        anim.SetFloat("Vertical", input.y);
    }
	void CalculateDirection(){
		angle = Mathf.Atan2 (input.x, input.y);
		angle = Mathf.Rad2Deg * angle;
		angle += cam.eulerAngles.y;
	}

	void Rotate(){
		targetRotation = Quaternion.Euler (0, angle, 0);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
	}
	void Move(){
		transform.position += transform.forward * velocity * Time.deltaTime;
	}
    void Dash()
    {
        if(dashTimer >= 0)
        {
            dashTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.U) )
        {
            dashTimer = 1f;
            Invoke("VectorZero", 0.3f);
           //transform.DOMove(new Vector3(0,0,transform.position.z * dashForce), 0.3f);
            //rb.AddForce(transform.forward * dashForce, ForceMode.);
            rb.velocity = transform.forward * dashForce;
        }
    }
    void VectorZero()
    {
        rb.velocity = Vector3.zero;
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            rb.velocity = transform.up * jumpForce;
            anim.SetTrigger("jump");
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.O) )
        {
            anim.SetTrigger("attack");
            canMove = false;
            damageDealer.SetActive(true);
            Invoke("UnLockMovement", 0.7f);

        }
    }
    void UnLockMovement()
    {
        canMove = true;
        CancelInvoke("UnLockMovement");
        damageDealer.SetActive(false);
    }
    void RigidBodyInitialize()
    {
        rb.mass = 100;
        rb.drag = 1;
        rb.freezeRotation = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrouned = true;
            transform.SetParent(collision.transform);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrouned = false;
            transform.SetParent(null);
        }
    }

}
