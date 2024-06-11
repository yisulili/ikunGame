using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrller : MonoBehaviour
{
    public int HP;
    public float speed;
    public float jumpSpeed;

    public int jumpCount;
    float moving;
    bool isGround;
    bool jumping;

    Rigidbody2D rigidbody2d;
    Transform groundCheck;
    public LayerMask Ground;
    Animator animator;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponent<Transform>();

      
        Debug.Log("transform.right" + transform.right);
        Debug.Log("transform.up" + transform.up);
        Debug.Log("transform.forward" + transform.forward);
        Debug.Log(" Vector3.left" + Vector3.left);
        Debug.Log("Vector3.right" + Vector3.right);
        Debug.Log("Vector3.up" + Vector3.up);
        Debug.Log("Vector3.down" + Vector3.down);
        Debug.Log("Vector3.forward" + Vector3.forward);
    }


    void Update()
    {
        moving = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && jumpCount > 0 )
        {
            jumping = true;
        }
        CheckGround();

    }

    private void FixedUpdate()
    {
        Walking();
        Jump(); 
        Flip();

    }
    void Flip()
    {
        if (moving > 0.01f)
           GetComponent<SpriteRenderer>().flipX = false;
        if (moving < -0.01f)
           GetComponent<SpriteRenderer>().flipX = true;
    }

    void Walking()
    {
        rigidbody2d.velocity = new Vector2(moving * speed * Time.deltaTime,rigidbody2d.velocity.y);
        animator.SetFloat("speed",Mathf.Abs(moving));


    }

    void CheckGround()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f,Ground);
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            animator.SetBool("isJump", false);
        }
        if (jumping)
        {
            rigidbody2d.velocity = Vector2.up * jumpSpeed;
            jumpCount--;
            animator.SetBool("isJump", true);
            jumping = false;
        }
        else
        {
            animator.SetBool("isJump", false);
        }
        if (rigidbody2d.velocity.y < 0)
        {
            rigidbody2d.gravityScale = 2.5f;
        }
        else
        {
            rigidbody2d.gravityScale = 1.0f;
        }
    }


    public void ModifyHP(int hp)
    {
        HP += hp;
        Debug.Log(hp);
        if (HP <= 0)
        {
            Debug.Log("ÓÎÏ·½áÊø£¡");
        }
        if(HP > 10)
        { HP = 10; }
    }
}
