using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewController : MonoBehaviour {

	public float velocity = 5;
	public float turnSpeed = 10;
    public Rigidbody rb;
    public float dashForce;
    public float jumpForce;
	Vector2 input;
	float angle;

    public bool isGrouned = false;
	Quaternion targetRotation;
	Transform cam;
//	public Animator anim;
	void Start () {
		cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        RigidBodyInitialize();

    }
	

	void Update () {
		GetInput ();
        Dash();
        Jump();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) {
			return;
		}
		CalculateDirection();
		Rotate();
		Move();
	}
	void GetInput(){
		input.x = Input.GetAxisRaw ("Horizontal");
		input.y = Input.GetAxisRaw ("Vertical");
//		anim.SetFloat ("Horizontal", input.x);
//		anim.SetFloat ("Vertical", input.y);
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
        if (Input.GetKeyDown(KeyCode.U))
        {
            transform.DOMove(transform.forward * dashForce, 0.3f);
            //rb.AddForce(transform.forward * dashForce, ForceMode.);
            //rb.velocity = transform.forward * dashForce;
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            rb.AddForce(transform.up * jumpForce);
        }
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
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrouned = false;
        }
    }

}
