using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAction : MonoBehaviour
{
    public float Speed = 6;
    public GameManager manager; //플레이어에서 매니저 함수를 호출할 수 있게 변수 생성
    Rigidbody2D rigid;
    Animator anim;
    float h;    //좌우 방향값
    float v;    //상하 방향값
    bool isHorizonMove;
    Vector3 dirVec;
    GameObject scanObject;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Move Value
        // manager.isAction이 true면 0, false면 위아래 입력값
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal"); 
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        //Check Button Down & Up
        //manager.isAction이 true면 false 대입, false면 해당값 대입
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical");

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
        if(vDown && v == 1)         //상
        {
            dirVec = Vector3.up;
        }
        else if(vDown && v == -1)   //하
        {
            dirVec = Vector3.down;
        }
        else if(hDown && h == -1)   //좌
        {
            dirVec = Vector3.left;
        }
        else if(hDown && h == 1)    //우
        {
            dirVec = Vector3.right;
        }

        //Scan Object & Action
        if(Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
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
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
