using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    //public 변수들
    public GameObject EndCursor;
    public int CharPerSeconds; //글자 재생 속도를 위한 변수
    public bool isAnim; //애니메이션 실행 판단을 위한 플래그 변수

    //non-public 변수들
    string targetMsg; //메시지 저장을 위한 변수
    Text msgText; //Text 변수 생성
    AudioSource audioSource; //초기화 후 재생 함수에서 Play()
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>(); 
    }

    public void SetMsg(string msg)
    {
        //플래그 변수를 이용해 분기점 로직 작성
        if (isAnim)
        {
            msgText.text = targetMsg; //글자 채워주고
            CancelInvoke(); //현재 진행중인 Invoke() 꺼짐
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    //애니메이션 재생 시작 함수
    void EffectStart()
    {
        msgText.text = ""; //공백 처리
        index = 0; //초기화
        EndCursor.SetActive(false); //커서 위아래로 움직이는 애니메이션 시작

        //Start Animation
        interval = 1.0f / CharPerSeconds;
        Debug.Log(interval);

        isAnim = true; //초기화

        Invoke("Effecting", interval); //시간차 반복 호출을 위한 Invoke() 함수 사용
    }

    //애니메이션 재생 과정 함수
    void Effecting()
    {
        if(msgText.text == targetMsg) //텍스트 모두 출력하면 종료
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        //Sound
        if(targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;

        //Recursive
        Invoke("Effecting", interval);
    }

    //애니메이션 재생 종료 함수
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true); //커서 위아래로 움직이는 애니메이션 삭제
    }
}
