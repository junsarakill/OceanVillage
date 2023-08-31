using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;


[System.Serializable] // 직렬화
public struct FishInfo
{
    //고유 아이디(아이디 안에 이름과 지역이 정해져있음)
    public int id;
    //이름
    public string fishName;
    //지역
    public string city;
    //가격
    public string value;

}

[System.Serializable]
public struct FishMenuList
{
    public List<FishInfo> data;
}

public class Json : MonoBehaviour
{
    //물고기의 정보
    FishInfo fishInfo;

    //물고기 정보 여러개 들고 있는 변수
    public List<FishInfo> fishBowlList = new List<FishInfo>();

    //fishbowlList의 key값을 만들어주기 위한 구조체
    //FishMenuList info = new FishMenuList();


    // Start is called before the first frame update
    void Start()
    {

        //고등어 갈치 가자미 세개를 랜덤으로 
        //지역은 다 정해져있음
        //가격 또한 정해져있음
        
        

        fishInfo = new FishInfo();

        fishInfo.id = 0;
        fishInfo.fishName = "생선 : 고등어";
        fishInfo.city = "전라북도 군산의 00마을";
        fishInfo.value = 10000 + "원";
        fishBowlList.Add(fishInfo);


        fishInfo.id = 1;
        fishInfo.fishName = "생선 : 갈치";
        fishInfo.city = "경상북도 군위의 00마을";
        fishInfo.value = 20000 + "원";
        fishBowlList.Add(fishInfo);

        fishInfo.id = 2;
        fishInfo.fishName = "생선 : 가자미";
        fishInfo.city = "전라남도 여수의 00마을";
        fishInfo.value = 25000 + "원";
        fishBowlList.Add(fishInfo);

        FishMenuList info = new FishMenuList();
        info.data = fishBowlList;

        string s= JsonUtility.ToJson(info,true);
        print(s);

        //jsonDataTest test = new jsonDataTest();
        //test.jsonData = fishBowlList;
        //{
        //    "FishName" : "고등어",
        //    "city" : "군산",
        //    "value" : "10000",
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //1번 키 누르면 json 형태로 만들자
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
             int rand = Random.Range(0, fishBowlList.Count);

            //포스트 요청하는 url링크
            print(fishBowlList[rand].fishName);
            return;

            //fishInfo를 json형태로 만들자
            string jsonData = JsonUtility.ToJson(fishInfo, true);
            print(jsonData);

            //jsonData를 파일로 저장
            FileStream file = new FileStream(Application.dataPath + " /fishInfo.txt",FileMode.Create);

            //json String 데이터를 byte배열로 만든다
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);

            //byteData를 file에 쓰자
            file.Write(byteData,0,byteData.Length);

            file.Close();
        }

        //2번 키 누르면 txt읽어오자
        if(Input.GetKey(KeyCode.Alpha2))
        {
            FileStream file = new FileStream(Application.dataPath + "/fishInfo.txt", FileMode.Open);
            //file 크기만큼 byte 배열 할당
            byte[] byteData = new byte[file.Length];
            //byteData에 file의 내용을 읽어온다
            file.Read(byteData,0,byteData.Length);

            //파일 닫기
            file.Close();
            //byteData를 문자열로 바꾸자
            string jsonData = Encoding.UTF8.GetString(byteData);

            //문자열로 되어있는 jsonData를 fishInfo에 parsing한다
            fishInfo = JsonUtility.FromJson<FishInfo>(jsonData);
        }
    }

    //잡았을 때 서버에 요청 

    //생선의 정보를 불러일으킨다

    //fishinfo.data 추가

    //물고기가 잡혔을 때 특정 url -> get or post

    //내가 잡은 물고기 리스트를 보여준다(인벤토리,)

    //

    //유저가 잡은 물고기 따로 저장

    //얼마 벌었는지






}
