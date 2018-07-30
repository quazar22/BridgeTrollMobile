using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;
    private Joystick joystick;
    private Button JumpButton;
    private float speed = 20f;
    private bool jumping = false;
	
	void Start () {
        rb = GetComponent<Rigidbody>();
        joystick = GameObject.Find("BackgroundImage").GetComponentInChildren<Joystick>();
        JumpButton = GameObject.Find("Jump").GetComponent<Button>();
        anim = GetComponent<Animator>();
        JumpButton.onClick.AddListener(Jump);
	}
	
    private void Jump()
    {
        if(!jumping)
        {
            jumping = true;
            rb.AddForce(new Vector3(rb.velocity.x, speed * -Physics.gravity.y * 4, rb.velocity.z));
            anim.SetInteger("State", 2);
        }
    }

    private void JumpUpdate(float distance)
    {
        if (!jumping)
        {
            anim.SetInteger("State", 1);
            anim.speed = distance * 4;
        }
        else
        {
            anim.SetInteger("State", 2);
            anim.speed = 1;
        }
    }

	// Update is called once per frame
	void Update () {
        

    }

    private void FixedUpdate()
    {
        ////////////////////////// TEST CODE //////////////////////////
        ////////////////////////// TEST CODE //////////////////////////
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        ////////////////////////// TEST CODE //////////////////////////
        ////////////////////////// TEST CODE //////////////////////////
        float x = joystick.Horizontal();
        float y = joystick.Vertical();
        float distance = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        JumpUpdate(distance);
        Vector3 movement;
        if (x != 0f && y != 0f)
        {
            Vector3 newvec = new Vector3(transform.eulerAngles.x,
                                                Mathf.Atan2(x, y) * Mathf.Rad2Deg,
                                                transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newvec), 0.05f);
        }
        else
        {
            anim.speed = 1;
            if (!jumping)
                anim.SetInteger("State", 0);
        }
        movement = transform.forward * distance * speed;
        movement.y = rb.velocity.y;
        rb.velocity = movement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GROUND")
        {
            anim.SetInteger("State", 1);
            jumping = false;
        }
    }
}
