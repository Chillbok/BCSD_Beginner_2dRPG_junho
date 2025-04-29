using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   // UI 프로그래밍 전에 반드시 넣어야 함

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager; //대화 매니저를 변수로 선언
    public QuestManager questManager; //퀘스트 매니저를 변수로 생성
    public Animator talkPanel; //대화창 생성/삭제 애니메이션을 위한 변수
    public Animator portraitAnim; //초상화 애니메이션 담기 위한 변수
    public Image portraitImg; //Image UI 접근을 위한 변수
    public Sprite prevPortrait; //과거 스프라이트를 저장해두어 비교하기 위한 변수
    public TypeEffect talk;
    public GameObject scanObject;
    public bool isAction; // 확인하는 액션 했는가? true: 했음, false: 안했음
    public int talkIndex;

    void Start()
    {
        Debug.Log(questManager.CheckQuest());
    }
    public void Action(GameObject scanObj)
    {
        //Get Current Object
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc);
        
        //Visible Talk for Action
        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        //Set Talk Data
        int questTalkIndex = 0;
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(
                /*퀘스트 번호 + NPC id = 퀘스트 대화 데이터 id*/id+ questTalkIndex,
                talkIndex
                );
        }

        //End Talk
        if(talkData == null) //talkdata가 비어있으면 대화가 끝났구나
        {
            isAction = false; //대화 끝!
            talkIndex = 0; //문장이 처음부터 시작해야함. 초기화.
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        //Continue Talk
        if(isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]); //구분자를 통해 배열로 나눠주는 문자열 함수.

            //Show Portrait
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1])); //Parse(): 문자열을 해당 타입으로 변환해주는 함수.
            portraitImg.color = new Color(1,1,1,1); //NPC일 때에만 이미지가 보이도록 하기 위함. 맨 뒤 값은 1.(투명도 조절)
            //Animation Portrait
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            //Hide Portrait
            portraitImg.color = new Color(1,1,1,0); //Portrait 가리기
        }

        //Next Talk
        isAction = true;
        talkIndex++; //다음 대사 출력용. Action()이 작동할 때마다 talkIndex가 1씩 늘어남.
    }
}