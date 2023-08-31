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
        
    }

    void Update()
    {
        //스파이스바를 누르면 낚시대 애니메이션 작용
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //X축 회전 (자식 객체의 컴포넌트 가져오기) - Test용
            gameObject.GetComponentInChildren<FishRodRot_LHS>().RodAction(1);
            gameObject.GetComponentInChildren<BaitMove_LHS>().BaitAction(1);

            //미끼가 특정 위치로 떨어짐

            //최초 위치에서 해당 위치로 이동 y축 이동
        }

        //움직임을 멈추고
        //상하좌우 이동만 가능
    }
}
