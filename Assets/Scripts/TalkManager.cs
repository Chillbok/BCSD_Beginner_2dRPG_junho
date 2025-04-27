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
        talkData.Add(100/*Box*/, new string[] {"평범한 나무상자다."});
        talkData.Add(200/*Desk*/, new string[] {"누군가 사용했던 흔적이 있는 책상이다."});

        //스프라이트 데이터
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
        if(talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
