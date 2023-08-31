using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    //내가 UI를 만든거를 순서대로 출현시켜
    //9번을 누르면 
    //내가 게임을 끝났다는 거를 엔딩매니저에 알려주면 나온다

    //UI 스테이지 게임오브젝트
    public GameObject firstStage;
    public GameObject secondStage;
    public GameObject thirdStage;

    //UI 이동 버튼 
    public Button moveButton;

    //UI 체험 버튼
    public Button adventureButton;

    //UI 종료 버튼
    public Button exitButton;



    // Start is called before the first frame update
    void Start()
    {
        //시작할땐 UI가 꺼져있다.
        firstStage.SetActive(false);
        secondStage.SetActive(false);
        thirdStage.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        //9번 키를 누르면 UI가 켜진다.
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            print("1111");
            firstStage.SetActive(true);
        }
    }
    public void OnChangeButton()
    {
        firstStage.SetActive(false);
        secondStage.SetActive(true);
    }
    public void OnAdventureButton()
    {
        secondStage.SetActive(false);
        thirdStage.SetActive(true);
    }
    public void OnExitButton()
    {
        thirdStage.SetActive(false);
        //맵으로 가서 마을로 이동한다.
    }

}
