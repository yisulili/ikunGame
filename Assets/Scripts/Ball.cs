using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float reclaimTime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > reclaimTime) 
        {
            gameObject.SetActive(false);
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
