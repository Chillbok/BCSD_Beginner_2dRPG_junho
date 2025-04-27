using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI 프로그래밍 전에 반드시 넣어야 함

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager; //대화 매니저를 변수로 선언
    public GameObject talkPanel;
    public Image portraitImg; //Image UI 접근을 위한 변수
    public Text talkText;
    public GameObject scanObject;
    public bool isAction; // 확인하는 액션 했는가? true: 했음, false: 안했음
    public int talkIndex;

    void Start()
    {
        talkPanel.SetActive(false); //처음 시작할 때 대화창 안보이게 하기
    }
    public void Action(GameObject scanObj)
    {
        //Depacrated Contents:
        /*
        if(isAction) //Exit Action
        {
            isAction = false;
        }
        else //Enter Action
        {
            isAction = true;
            scanObject = scanObj;   //인식한 매개변수를 GameObject 변수인 scanObject에 삽입
            //대화창 출력 연습용
            //talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";    //텍스트에 scanObject의 이름을 소개하는 글 넣기
            ObjectData objectData = scanObject.GetComponent<ObjectData>();
            Talk(objectData.id, objectData.isNpc);
        }
        */
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);
        
        talkPanel.SetActive(isAction);  //대화창 나타내기 or 끄기, true면 켜고, false면 끔
    }

    void Talk(int id, bool isNpc)
    {

        string talkData = talkManager.GetTalk(id, talkIndex);

        if(talkData == null) //talkdata가 비어있으면 대화가 끝났구나
        {
            isAction = false; //대화 끝!
            talkIndex = 0; //문장이 처음부터 시작해야함. 초기화.
            return;
        }


        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0]; //구분자를 통해 배열로 나눠주는 문자열 함수.
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); //Parse(): 문자열을 해당 타입으로 변환해주는 함수.
            portraitImg.color = new Color(1,1,1,1); //NPC일 때에만 이미지가 보이도록 하기 위함. 맨 뒤 값은 1.(투명도 조절)
        }
        else
        {
            talkText.text = talkData;
            portraitImg.color = new Color(1,1,1,0);
        }

        isAction = true;
        talkIndex++; //다음 대사 출력용. Action()이 작동할 때마다 talkIndex가 1씩 늘어남.
    }
}