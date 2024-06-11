using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slim2 : MonoBehaviour
{
    public float jumpForce;
    public float dieTime;
    public int damage;

    Vector2 moveDir = new Vector2(1, 1);
    float time = 0;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (spriteRenderer.flipX)
        {
            moveDir.x = -1;
        }
        else
        {
            moveDir.x = 1;
        }

    }

    void Update()
    {

        time += Time.deltaTime;
        if(time > 5)
        {
            animator.SetBool("isMove",false);
        }
        if(time > 7)
        {
            TurnAround();
            animator.SetBool("isMove", true);
            time = 0;
        }
    }
    public void Move()
    {
        rb.AddForce(moveDir * jumpForce, ForceMode2D.Impulse);

    }
    public void TurnAround()
    {
        if (spriteRenderer.flipX)
        {
            moveDir.x = 1;
            spriteRenderer.flipX = false;
        }
        else
        {
            moveDir.x = -1;
            spriteRenderer.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("ikun ‹…À");
            collision.gameObject.GetComponent<PlayerContrller>().ModifyHP(damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Basketball"))
        {
            animator.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Renderer>().sortingOrder = 1;
            rb.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
            die();
        }
    }

    void die()
    {
        Destroy(gameObject, dieTime);
    }
}
