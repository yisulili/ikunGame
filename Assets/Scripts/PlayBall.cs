using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public int speed;
    public int counter = 0;


    GameObject[] balls = new GameObject[10];
    
    Vector2 playingDir = Vector2.zero;
    Vector2 spawnPos; // 球的抛射点
    private void Start()
    {
        
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i] = Instantiate(ballPrefab,transform.position,Quaternion.identity);
        }
    }

    private void Update()
    {
        Vector2 mousPos = Input.mousePosition;
        playingDir = Camera.main.ScreenToWorldPoint(mousPos);
        Debug.Log(Vector2.Angle(transform.position, playingDir));
        if (GetComponent<SpriteRenderer>().flipX)
        {
            spawnPos = transform.position + new Vector3(-1.5f,1.5f,0);
        }
        else
        {
            spawnPos = transform.position + new Vector3(1.5f, 1.5f, 0);
        }
        counter = counter % 10;
        
        playingDir = (playingDir - spawnPos).normalized;

        //限制发射角度
        if(playingDir.x > 0)
        {
            if(playingDir.y / playingDir.x >= 1) { playingDir = new Vector2(1, 1).normalized; }
            else if(playingDir.y / playingDir.x <= -1) { playingDir = new Vector2(1,-1).normalized; }
        }
        else if(playingDir.x < 0)
        {
            if (playingDir.y / playingDir.x >= 1) { playingDir = new Vector2(-1, -1).normalized; }
            else if (playingDir.y / playingDir.x <= -1) { playingDir = new Vector2(-1, 1).normalized; }
        }



        //限制鼠标在主角前面时可发射篮球
        bool canPlay = false;
        if(GetComponent<SpriteRenderer>().flipX && playingDir.x < 0
            || !GetComponent<SpriteRenderer>().flipX && playingDir.x > 0) canPlay = true;
        else canPlay = false;
        

        if (Input.GetMouseButtonDown(0) && canPlay) 
        {
            Playing(balls[counter]);
            counter++;
        }

    }

    private void Playing(GameObject ball)
    {
        if(ball.activeSelf == false)
        {



            ball.SetActive(true);
            ball.transform.position = spawnPos;
            Rigidbody2D rbBall = ball.GetComponent<Rigidbody2D>();
            rbBall.AddForce(playingDir * speed, ForceMode2D.Impulse);
            Debug.Log("发射");
        }
        else
        {
            Debug.Log("手中没有篮球");
        }
    }

}
