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
    //점수 증가 text
    TextMeshProUGUI incScoreUIText;
    //점수 증가 text ui 위치
    Vector3 incScoreUIPos;

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

    //획득 파티클 프리팹
    [SerializeField] GameObject fishGetPSF;
    
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
        incScoreUIText = scoreUI.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        //증가 점수 초기 위치 설정
        RectTransform incRt = (RectTransform)incScoreUIText.transform;
        //처음 위치 저장
        incScoreUIPos = incRt.position;
        print($"incScoreUIPos = {incScoreUIPos}");
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
        SpawnFish(1);
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
    public void SpawnFish(float delayTime = 3)
    {
        StartCoroutine(IESpawnTimer(delayTime));
    }

    //물고기 스폰 타이머
    IEnumerator IESpawnTimer(float delayTime = 3)
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
        //물고기 객체
        GameObject fish = GameObject.FindWithTag("Fish");
        //획득 파티클 효과 재생
        GameObject fishGetPS = Instantiate(fishGetPSF, fish.transform);
        //사운드 재생
        AudioManager.instance.PlaySound(AudioManager.Sounds.FISH_CATCH);
        isSpawned = false;
        //3초 : 팝업 효과 끝나면 자동 종료
        yield return new WaitForSeconds(.5f);
        //물고기 제거
        Destroy(fish);
        print("이미=======================지 생성");
        //점수로 환원 되는 모션
        StartCoroutine(IEScoreMotion());
        //물고기 생성 요청
        SpawnFish();
    }

    //물고기 이미지 프리팹
    [SerializeField]GameObject fishImageF;
    //캔바스
    [SerializeField]GameObject JBSCanvas;

    //점수 환원 모션
    IEnumerator IEScoreMotion()
    {
        //화면 정중앙에 물고기 이미지 생성
        GameObject fishImage = Instantiate(fishImageF, JBSCanvas.transform);
        //계속 회전 시키기
        Coroutine rotCor = StartCoroutine(IEFishImageRotate(fishImage.transform));
        //점수 위치로 이동
        iTween.MoveTo(fishImage, iTween.Hash(
            "position", scoreUI.transform.position
            ,"easetype", iTween.EaseType.easeOutBack
            ,"time", 2
        ));
        iTween.ScaleTo(fishImage, iTween.Hash(
            "delay", .1f
            ,"scale", Vector3.zero
            ,"easetype", iTween.EaseType.easeInCubic
            ,"time", 1.9f
        ));
        while(fishImage.transform.localScale != Vector3.zero)
        {
            yield return null;
        }
        //print("소멸 시작");
        //이동하면 제거되고 점수 상승 효과
        //코루틴 종료
        StopCoroutine(rotCor);
        Destroy(fishImage);
        print("물고기 모션 종료");
        //점수 추가
        AddScore(fishScore);
        yield return null;

    }

    //물고기의 점수
    public int fishScore = 250;

    //생선 이미지 회전 시키기
    IEnumerator IEFishImageRotate(Transform fishTr)
    {
        while(fishTr.gameObject != null)
        {
            fishTr.Rotate(0,0,-2*8);
            yield return null;
        }
    }

    //점수 증가
    public void AddScore(int value)
    {
        StartCoroutine(IEIncScoreMotion(value));
        StartCoroutine(IEAddScore(value));
    }

    IEnumerator IEIncScoreMotion(int value)
    {
        //value를 초록색 글씨로 위로 조금 떠오르는 텍스트 itween
        RectTransform incRt = (RectTransform)incScoreUIText.transform;
        incScoreUIText.text = $"+{value}";
        //활성화
        incRt.gameObject.SetActive(true);
        //이동 시간
        float moveTime = 2;
        //위치로 이동
        iTween.MoveTo(incRt.gameObject, iTween.Hash(
            "y", (incScoreUIPos.y + 100)
            ,"easetype", iTween.EaseType.easeOutQuad
            ,"time", moveTime
        ));
        //띠링 점수 오르는 사운드 재생
        //위치 이동 대기
        for(float time = 0; time < moveTime; time += Time.deltaTime)
        {
            yield return null;
        }
        //증가 텍스트 비활성화 및 위치 초기화
        print("asd"+incRt.position);
        incRt.position = incScoreUIPos;
        print("asd"+incRt.position);
        yield return null;
        incRt.gameObject.SetActive(false);
    }

    IEnumerator IEAddScore(int value)
    {
        SCORE += value;
        //점수 증가 사운드 재생
        AudioManager.instance.PlaySound(AudioManager.Sounds.GET_SCORE);
        //.5 초 동안 텍스트 색상 노란색->다시 하얀색
        scoreUIText.color = Color.yellow;
        for(float time = 0; time < .5f; time += Time.deltaTime)
        {
            yield return null;
        }
        scoreUIText.color = Color.white;
        yield return null;
    }
}
