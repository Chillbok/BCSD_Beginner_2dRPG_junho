using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex; //퀘스트 대화순서 변수
    public GameObject[] questObject; //퀘스트 오브젝트를 저장할 변수
    Dictionary<int, QuestData> questList; //퀘스트 데이터를 저장할 Dictionary 변수

    void Awake()
    {
        //초기화
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        //Add 함수로 <QuestId, QuestData> 데이터를 저장
        questList.Add(/*퀘스트 아이디*/10, new QuestData(
            /*questName:*/"마을 사람들과 대화하기",
            /*npcId:*/new int[] {1000, 2000}
        ));
        
        questList.Add(/*퀘스트 아이디*/20, new QuestData(
            /*questName:*/"루도의 동전 찾아주기",
            /*npcId:*/new int[] {5000, 2000}
        ));
        questList.Add(30, new QuestData("퀘스트 올 클리어!",
            new int[] {0}
        ));
    }

    public int GetQuestTalkIndex(int id) //NPC ID를 받고 퀘스트 번호를 반환하는 함수
    {
        return questId + questActionIndex; //퀘스트 번호 + 퀘스트 대화 순서 = 퀘스트 대화 ID
    }

    public string CheckQuest(int id)//대화 진행을 위해 퀘스트 대화순서를 올리는 함수
    {
        //Next Talk Target
        //순서에 맞게 대화했을 때에만 퀘스트 대화 순서를 올리도록 작성
        //id가 퀘스트리스트 id와 일치하면 실행
        if(id == questList[questId].npcId[questActionIndex])
            questActionIndex++;
        
        //Controll Quest Object
        ControllObject(); //퀘스트 오브젝트 있는지 확인하고 있으면 활성화

        //Talk Complete & Next Quest
        if(questActionIndex == questList[questId].npcId.Length) //퀘스트 대화순서가 끝에 도달했을 때 퀘스트 번호 증가
            NextQuest();
        
        //Quest Name
        return questList[questId].questName; //퀘스트 이름을 반환하도록 함수 개조.
    }

    //다음 퀘스트를 위한 함수 생성
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0; //새로운 퀘스트가 시작되었으므로, 0으로 초기화
    }

    void ControllObject()
    {
        switch(questId)
        {
            //퀘스트 번호, 퀘스트 대화순서에 따라 오브젝트 조절
            case 10:
                //Ludo와 대화 끝나면 동전 보이게 하기
                if(questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            //동전 먹으면 동전 끄기
            case 20:
                if(questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
