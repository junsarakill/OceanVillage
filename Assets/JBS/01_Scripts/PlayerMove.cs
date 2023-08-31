using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 스탯
[System.Serializable]
public class PlayerStat
{
    //이동속도
    public float moveSpeed = 5;
    //회전 속력
    public float mSensivity = 200;
    //상호작용 거리
    public float interactRange = 3;

    public enum PlayerMode
    {
        MOVE, FISHING, RESULT
    }
    
    public PlayerMode playerMode;
}

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;

    //플레이어 스탯
    [SerializeField]PlayerStat pStat;
    //캐릭터 컨트롤러
    CharacterController cc;
    //카메라
    [SerializeField]Transform mCam;
    //회전 값
    float rx,ry;
    //이동 입력 값
    float h,v;
    //y축 속도
    float yVelocity;
    //중력
    float gravity = -9.81f;
    //의자
    Transform chair;
    //스폰 위치
    [SerializeField]Transform spawnPoint;
    //상호 작용 했던 위치,회전 값
    Vector3 originPos;
    Quaternion originRot;
    Quaternion mCamOriginRot;

    //낚시대 객체
    [SerializeField]GameObject fishingRod;
    
    //낚시 위치
    [SerializeField]Transform fishingPoint;

    [SerializeField] GameObject PlayerBody;

    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        pStat = new PlayerStat();
        cc = GetComponent<CharacterController>();
        chair = GameObject.FindWithTag("Chair").transform;
        ChangeState(PlayerStat.PlayerMode.MOVE);

        //초기 위치 스폰 포인트로 이동
        transform.SetLocalPositionAndRotation(spawnPoint.position, spawnPoint.rotation);

        //값 초기화
        originPos = new Vector3(0,0,0);
    }

    private void Update() {
        if(pStat.playerMode == PlayerStat.PlayerMode.MOVE)
        {
            //중력 작용
            CheckGround();
            //캐릭터 이동
            UpdateMove();
            
            //의자 상호작용 확인
            CheckInteract();
        }
        else
        {
            chair.GetComponent<Chair>().IS_INTERACT = false;
        }
        //캐릭터 회전
        UpdateRotate();
    }

    [SerializeField]float dis;
    
    //의자 상호작용 확인
    private void CheckInteract()
    {
        //의자와의 거리 확인
        dis = Vector3.Distance(transform.position, chair.position);
        //의자 스크립트
        Chair chairC = chair.GetComponent<Chair>();
        if(dis <= pStat.interactRange)
        {
            //상호작용 하이라이트
            chairC.IS_INTERACT = true;
            if(Input.GetButtonDown("Interact"))
            {
                print("상호작용");
                StartFishing();
            }
        }
        else
        {
            chairC.IS_INTERACT = false;
            if(Input.GetButtonDown("Interact"))
            {
                print("상호작용 할 물체 없음");
            }
        }
    }
    //낚시 모드
    public void StartFishing()
    {
        //모드 변경
        ChangeState(PlayerStat.PlayerMode.FISHING);
        //현재 위치,회전,카메라 회전 값 저장
        originPos = transform.position;
        originRot = transform.rotation;
        mCamOriginRot = mCam.rotation;
        //낚시 위치로 이동
        transform.SetLocalPositionAndRotation(fishingPoint.position, fishingPoint.rotation);
        mCam.localRotation = Quaternion.Euler(14.453f,0,0);

        FishingManager.instance.StartFishing();
    }

    [SerializeField] Transform endingPos;

    //낚시 모드 종료
    public void EndFishing()
    {
        ChangeState(PlayerStat.PlayerMode.MOVE);
        //모든 물고기 제거
        GameObject[] fishes = GameObject.FindGameObjectsWithTag("Fish");
        foreach(GameObject fish in fishes)
        {
            Destroy(fish);
        }
        //원래 위치로 이동
        transform.SetLocalPositionAndRotation(endingPos.position, endingPos.rotation);
        mCam.rotation = mCamOriginRot;
        
        EndingManager.instance.ResultUI();
    }

    //상태 변경
    public void ChangeState(PlayerStat.PlayerMode pMode)
    {
        //상태 변경
        pStat.playerMode = pMode;

        if(pStat.playerMode == PlayerStat.PlayerMode.MOVE)
        {
            PlayerBody.SetActive(false);
        }
        else if(pStat.playerMode == PlayerStat.PlayerMode.FISHING)
        {
            PlayerBody.SetActive(true);
        }
    }

    private void UpdateMove()
    {
        //상하좌우 입력 받음
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //값을 바로 1,-1로 받으려면 Input.GetAxisRaw("Horizontal"); 사용
        //입력 값으로 방향 구하기
        //입력된 방향 지정
        Vector3 moveDir = new Vector3(h,0,v);
        //현재 방향을 카메라의 앞으로 지정
        moveDir = mCam.TransformDirection(moveDir);
        moveDir.y = 0;
        moveDir.Normalize();
        Vector3 velocity = moveDir * pStat.moveSpeed;
        velocity.y = yVelocity;
        //이동
        //transform.position += moveDir * playerStat.moveSpeed * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }

    void UpdateRotate()
    {
        //마우스 움직임에 따라 좌우로 플레이어 회전
        //상하로 카메라 상하 회전
        rx += Input.GetAxis("Mouse X") * pStat.mSensivity * Time.deltaTime;
        ry += Input.GetAxis("Mouse Y") * pStat.mSensivity * Time.deltaTime;
        //print($"마우스 x : {rx}, y : {ry}");
        if(pStat.playerMode == PlayerStat.PlayerMode.MOVE)
        {
            //ry 각도 제한
            ry = Mathf.Clamp(ry,-75,75);
        }
        else if(pStat.playerMode == PlayerStat.PlayerMode.FISHING)
        {
            //@@ 낚시 위치에 따라 변경해야함
            //rx,ry 각도 제한
            rx = Mathf.Clamp(rx, -101, -4.5f);
            ry = Mathf.Clamp(ry,-53,5);
        }
        //카메라 회전
        mCam.eulerAngles = new Vector3(-ry,rx,0);
        //mCam.transform.eulerAngles = new Vector3(-ry,0,0);
        //플레이어 몸통 회전
        transform.eulerAngles = new Vector3(0,rx,0);
    }

    void CheckGround()
    {
        //지면에 충돌 시 점프 횟수 회복
        if(cc.isGrounded)
        {
            //leftJumpChance = jumpChance;
            yVelocity = 0;

            //if(isJump)
            //{
            //    isJump = false;
            //    photonView.RPC(nameof(SetTriggerRPC),RpcTarget.All,"Landing");
            //}

        }
        else
        {
            //y 속도에 중력 만큼 작용
            yVelocity += gravity * Time.deltaTime;
        }
    }

}
