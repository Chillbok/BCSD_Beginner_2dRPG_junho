using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData; //<id, 말할 내용>: 대화 하나에는 여러 문장이 들어있으므로 string[] 사용함
    Dictionary<int, Sprite> portraitData; //초상화 데이터.

    public Sprite[] portraitArr; //초상화 스프라이트 저장할 배열

    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        //대화 데이터
        talkData.Add(1000/*Luna*/, new string[] {
            "안녕?:0",
            "이 곳에 처음 왔구나?:1"
        });
        talkData.Add(2000/*Ludo*/, new string[] {
            "안녕? 내 이름은 Ludo라고 해.:1",
            "여긴 정말 아름답지?:0",
            "나도 그렇게 생각해.:1"
            });

        //물건들
        talkData.Add(3000/*Box*/, new string[] {"평범한 나무상자다."});
        talkData.Add(4000/*Desk*/, new string[] {"누군가 사용했던 흔적이 있는 책상이다."});

        //퀘스트 대화
        //퀘스트 번호 + NPC id에 해당하는 대화 데이터 작성

        //Luna
        talkData.Add(10 + 1000, new string[] {
            "어서 와.:0",
            "이 마을에 놀라운 전설이 있다는데,:1",
            "오른쪽 호수 쪽에 루도가 알려줄거야.:2"
        });
        talkData.Add(11 + 1000, new string[] {
            "아직 못 만났어?:0",
            "루도는 오른쪽 호수에 있어.:0"
        });
        talkData.Add(20 + 1000, new string[] {
            "루도의 동전?:1",
            "돈을 흘리고 다니면 못 쓰지!:3",
            "나중에 루도에게 한마디 해야겠어.:3"
        });

        //Ludo
        talkData.Add(11 + 2000, new string[] {
            "안녕.:1",
            "이 호수의 전설을 들으러 온 거야?:0",
            "그럼 일 좀 하나 해줬으면 해.:1",
            "내 집 근처에 떨어진 동전 좀 주워줘.:2"
        });
        talkData.Add(20+2000, new string[] {
            "찾으면 꼭 좀 가져다 줘.:1"
        });
        talkData.Add(21+2000, new string[] {
            "엇, 찾아줘서 고마워.:2"
        });

        //Coin
        talkData.Add(20+5000, new string[] {"근처에서 동전을 찾았다."});

        //스프라이트 데이터
        //0:Normal, 1:Speak, 2:Happy, 3:Angry
        //Luna
        portraitData.Add(1000 + 0,portraitArr[0]);
        portraitData.Add(1000 + 1,portraitArr[1]);
        portraitData.Add(1000 + 2,portraitArr[2]);
        portraitData.Add(1000 + 3,portraitArr[3]);
        //Ludo
        portraitData.Add(2000 + 0,portraitArr[4]);
        portraitData.Add(2000 + 1,portraitArr[5]);
        portraitData.Add(2000 + 2,portraitArr[6]);
        portraitData.Add(2000 + 3,portraitArr[7]);

    }

    public string GetTalk(int id, int talkIndex)
    { 
        if (!talkData.ContainsKey(id)) //ContainsKey(): Dictionary에 Key가 존재하는지 검사해주는 함수
        {
            if(!talkData.ContainsKey(id - id%10))
                return GetTalk(id - id%100, talkIndex); //Get First Talk
            else
                return GetTalk(id - id%10, talkIndex); //Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
