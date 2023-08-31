using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishMode_LHS: MonoBehaviour
{
    /*//미끼
    [SerializeField] GameObject bait;
    //미끼시작위치
    [SerializeField] Transform startPos;
    //미끼종료위치
    [SerializeField] Transform endPos;*/

    void Start()
    {
        //@@ JBS 임시 주석 처리
        //UIManager_LHS.instance.GameStartBeforeUI();
    }

    void Update()
    {
        //스파이스바를 누르면 낚시대 애니메이션 작용
        if(Input.GetKeyDown(KeyCode.Space))
        {

            if(GameManager_LHS.instance.isfishSave == true)
            {
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = true;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = false;
                UIManager_LHS.instance.FishGrabUI();

                //다시 잡을 수 있는 상태로 만들기
                GameManager_LHS.instance.isfishSave = false;
                GameManager_LHS.instance.isfishLook = true;
            }

            else
            {
                //X축 회전 (자식 객체의 컴포넌트 가져오기) - Test용
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = true;

                //게임 UI끄기
                UIManager_LHS.instance.GameStartUI();
                //미끼가 특정 위치로 떨어짐

                //최초 위치에서 해당 위치로 이동 y축 이동

                //물고기를 잡은 상태라면 다시 올라올 수 있게 만들기
            }
        }

        //움직임을 멈추고
        //상하좌우 이동만 가능
    }
}
