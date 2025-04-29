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

    //모바일 버튼 입력 받을 변수 12개 생성
    int up_Value;
    int down_Vaule;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Move Value
        //PC + Mobile
        // manager.isAction이 true면 0, false면 위아래 입력값
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value; 
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Vaule;

        //Check Button Down & Up
        //manager.isAction이 true면 false 대입, false면 해당값 대입
        //PC + Mobile
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;

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

        //Mobile Var Init
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;
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

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D":
                down_Vaule = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
            case "A":
                if (scanObject != null)
                    manager.Action(scanObject);
                break;
            case "C":
                manager.SubMenuActive();
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_Up = true;
                break;
            case "D":
                down_Vaule = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }
}
