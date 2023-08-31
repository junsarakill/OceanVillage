using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FishingManager : MonoBehaviour
{
    public static FishingManager instance;

    //타이머 UI
    [SerializeField] GameObject timerUI;
    //점수 UI
    [SerializeField] GameObject scoreUI;
    //점수 text
    TextMeshProUGUI scoreUIText;

    //낚시 모드 제한 시간
    [SerializeField]float fishingTime = 30;
    //물고기 스폰됨
    public bool isSpawned = false;
    //물고기 프리팹
    [SerializeField]GameObject fishF;
    //물고기 스폰 포인트
    [SerializeField]Transform fishSpawnPos;

    //점수
    public int score;
    public int SCORE
    {
        get{return score;}
        set
        {
            score = value;
            scoreUIText.text = $"{value}";
        }
    }
    
    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //낚시 모드 ui 초기 비활성화
        timerUI.SetActive(false);
        scoreUI.SetActive(false);
        
    }

    private void Start() {
        //점수 ui text 가져오기
        scoreUIText = scoreUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    //낚시 모드 시작
    public void StartFishing()
    {
        StartCoroutine(IEFishing());
    }

    //타이머
    public float leftTime;
    IEnumerator IEFishing()
    {
        print("낚시 모드 시작");
        //ui 활성화
        timerUI.SetActive(true);
        scoreUI.SetActive(true);
        //타이머 텍스트 가져오기
        TextMeshProUGUI timerText = timerUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //점수 초기화
        SCORE = 0;
        //처음 물고기 생성
        SpawnFish(3);
        //제한 시간 설정
        leftTime = fishingTime;
        //시간 흐르기
        while(leftTime > 0)
        {
            leftTime -= Time.deltaTime;
            timerText.text = $"0:{(int) leftTime}";
            yield return null;
            //@@10초일때 포인트
        }
        //시간 끝나면 종료
        PlayerMove.instance.EndFishing();
        //ui 비활성화
        timerUI.SetActive(false);
        scoreUI.SetActive(false);
        //@@ 점수 보내기
        print($"낚시 종료. 점수 : {SCORE}");
    }

    //딜레이 물고기 스폰 요청
    public void SpawnFish(float delayTime = 5)
    {
        StartCoroutine(IESpawnTimer(delayTime));
    }

    //물고기 스폰 타이머
    IEnumerator IESpawnTimer(float delayTime = 5)
    {
        //딜레이 만큼 대기
        float time = 0;
        while(time < delayTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        isSpawned = true;
        GameObject fish = Instantiate(fishF);
        fish.transform.SetLocalPositionAndRotation(fishSpawnPos.position, fishSpawnPos.rotation);
        print("물고기 스폰");
    }

    [SerializeField]GameObject tempIcon;

    //물고기 잡힘 코루틴
    //물고기가 잡히면 (들어 올려져서 버튼 연타로 게이지 value를 1로함)
    //물고기 생성 요청을 한다.
    //물고기 객체를 제거하고 팝업 이미지가 뜬다
    //상호작용 키로 팝업이미지를 끌 수 있다.
    //끄면 잡힌 물고기가 점수로 환원된다.

    //물고기 잡은 뒤 처리
    public IEnumerator IECatchFish()
    {
        isSpawned = false;
        Destroy(GameObject.FindWithTag("Fish"));
        //임시 팝업 이미지 활성화
        tempIcon.SetActive(true);
        //3초 : 팝업 효과 끝나면 자동 종료
        yield return new WaitForSeconds(3);
        //@@점수로 환원 되는 모션
        tempIcon.SetActive(false);
        FishingManager.instance.AddScore(250);
        yield return null;
        //물고기 생성 요청
        SpawnFish();
    }

    //점수 증가
    public void AddScore(int value)
    {
        SCORE += value;
        
    }
}
