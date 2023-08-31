using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishMode_LHS: MonoBehaviour
{
    public GameObject bait;

    void Update()
    {
        //스파이스바를 누르면 낚시대 애니메이션 작용
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //올리기
            if(GameManager_LHS.instance.isfishSave == true)
            {
                Debug.Log("스페이스1번");
                bait.GetComponent<BaitMove_LHS>().SetPos();

                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = true;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isFishPos = false;

                UIManager_LHS.instance.FishGrabUI();

                //다시 잡을 수 있는 상태로 만들기
                GameManager_LHS.instance.isfishSave = false;
                //물고기 잡았을때
                GameManager_LHS.instance.isfishLook = true;
            }

            //내리기
            else
            {
                Debug.Log("스페이스2번");
              
                //X축 회전 (자식 객체의 컴포넌트 가져오기) - Test용
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = true;

                //게임 UI끄기
                UIManager_LHS.instance.GameStartUI();

                //GameManager_LHS.instance.isfishSave = false;
                GameManager_LHS.instance.isfishLook = false;
            }
        }
    }
}
