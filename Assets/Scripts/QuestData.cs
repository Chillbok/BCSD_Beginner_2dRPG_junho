using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    //오브젝트에 넣어서 쓸 것이 아니고, 코드에서 불러와서 쓸 스크립트임.
    //따라서 Monobehaviour 지움.

    public string questName; //퀘스트 이름
    public int[] npcId; //퀘스트와 연관되어 있는 npc의 아이디를 저장하는 배열

    //구조체 생성을 위한 매개변수 생성자 작성하기
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}
