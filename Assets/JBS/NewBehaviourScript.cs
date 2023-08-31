using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //이동은 cc 쓰는 기본 이동c
    //회전은 metaphoton 참고c
    //시간제한은 코루틴 time-=time.del;
    //시작 인트로는 시네머신 사용
    //거리가 2 이내면 낚시 미니게임을 실행할 수 있는 의자 오브젝트와 상호작용 가능
    //하이라이트는 오버쿡드 참고
    //물고기가 isSpawn이 아닐때 시간을 셈
    //5초마다 스폰
    //시간 10초 남을때 딸랑 사운드 (오버쿡드 참고)
    //0초 되면 결산 ui 활성화
    //점수 확인하고 랭킹권이면 서버에서 랭킹 정보를 가져와서 점수 비교후 정렬
    //5글자 알파벳 입력하면(도트풍) 해당 이름,점수를 서버에 전송
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
