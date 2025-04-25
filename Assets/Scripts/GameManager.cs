using UnityEngine;
using UnityEngine.UI;   // UI 프로그래밍 전에 반드시 넣어야 함

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;

    void Start()
    {
        talkPanel.SetActive(false); //처음 시작할 때 대화창 안보이게 하기
    }
    public void Action(GameObject scanObj)
    {
        if(isAction) //Exit Action
        {
            isAction = false; //확인하는 액션을 했느냐 안했느냐
        }
        else //Enter Action
        {
            isAction = true;
            scanObject = scanObj;   //인식한 매개변수를 GameObject 변수인 scanObject에 삽입
            talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";    //텍스트에 scanObject의 이름을 소개하는 글 넣기
        }
        
        talkPanel.SetActive(isAction);  //대화창 나타내기 or 끄기, true면 켜고, false면 끔
    }
}