using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    public float Speed = 4;
    Rigidbody2D rigid;
    Animator anim;
    float h;    //좌우 방향값
    float v;    //상하 방향값
    bool isHorizonMove;
    Vector3 dirVec;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //Check Horizontal Move
        if(hDown)
        {
            isHorizonMove = true;
        }
        else if(vDown)
        {
            isHorizonMove = false;
        }
        else if(hUp || vUp)
        {
            if(h != 0)
            {
                isHorizonMove = true;
            }
            else
            {
                isHorizonMove = false;
            }
        }

        //Animation
        if(anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);    //서로 타입이 다르면 명시적 형변환으로 처리
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange",true);
            anim.SetInteger("vAxisRaw", (int)v);    //서로 타입이 다르면 명시적 형변환으로 처리
        }
        else
        {
            anim.SetBool("isChange",false);
        }

        //Direction
        if(vDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if(vDown && v == -1)
        {
            dirVec = Vector3.down;
        }
        else if(hDown && h == -1)
        {
            dirVec = Vector3.left;
        }
        else if(hDown && h == 1)
        {
            dirVec = Vector3.right;
        }
    }

    void FixedUpdate()
    {
        //Move
        Vector2 moveVec;
        if (isHorizonMove == true)
        {
            moveVec = new Vector2(h,0);
        }
        else
        {
            moveVec = new Vector2(0,v);
        }
        rigid.linearVelocity = moveVec * Speed;

        //Ray
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0,1,0));
    }
}
