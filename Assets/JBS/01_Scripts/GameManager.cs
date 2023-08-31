using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameObject player;
    
    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //플레이어 가져오기
        player = GameObject.FindWithTag("Player");
    }

    private void Start() {
        //커서 비활성화
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update() {
        //esc로 커서 활성/비활성
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.visible)
            {
                //커서 비활성화
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                //커서 활성화
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }





        //@@=========테스트용=============
        //1번 누르면 낚시 모드 시작
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            player.GetComponent<PlayerMove>().StartFishing();
        }

        //2번 누르면 물고기 제거
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(FishingManager.instance.isSpawned)
            {
                StartCoroutine(FishingManager.instance.IECatchFish());
            }
            else
            {
                print("물고기 없다");
            }
        }
        //3번 누르면 시간 3초로 변경
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            FishingManager.instance.leftTime = 3;
        }
        //4번 누르면 점수 증가
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            FishingManager.instance.AddScore(50);
        }
    }
}
