using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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

    //패널
    public GameObject panel;



    // Start is called before the first frame update
    void Start()
    {
        //시작할땐 UI가 꺼져있다.
        firstStage.SetActive(false);
        secondStage.SetActive(false);
        thirdStage.SetActive(false);
        panel.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        //9번 키를 누르면 UI가 켜진다.
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            print("1111");
            firstStage.SetActive(true);
            //패널도 켜진다.
            panel.SetActive(true);
        }
    }
    //public void DownloadFishImage(string imageUrl)
    //{
    //    HttpInfo info = new HttpInfo();
    //    info.Set(
    //        RequestType.TEXTURE,
    //        imageUrl,
    //        null,
    //        false);
    //    //info.imageId = imageId;
    //    info.onReceiveImage = (DownloadHandler downloadHandler, int idx) => {

    //        Texture2D texture = ((DownloadHandlerTexture)downloadHandler).texture;
    //        villageImages[idx].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

    //    };

    //    HttpManager.Get().SendRequest(info);
    //}

    public void OnEnterButton()
    {
        HttpInfo info = new HttpInfo();
        info.Set(RequestType.POST, "/ sign - up'", (DownloadHandler downloadHandler) =>
        {
            print("NetSignUp : " + downloadHandler.text);

            JsonList<NetVillageInfo> jsonList = JsonUtility.FromJson<JsonList<NetVillageInfo>>(downloadHandler.text);

            //이미지 다운로드
            for (int i = 0; i < jsonList.data.Count; i++)
            {
                DownloadVillageImage(jsonList.data[i].image, i);
            }

            firstStage.SetActive(false);
            secondStage.SetActive(true);
        });

    }



    public void OnChangeButton()
    {

        HttpInfo info = new HttpInfo();
        info.Set(RequestType.GET, "/village/flounder", (DownloadHandler downloadHandler) =>
        {
            print("NetVillage : " + downloadHandler.text);

            JsonList<NetVillageInfo> jsonList = JsonUtility.FromJson<JsonList<NetVillageInfo>>(downloadHandler.text);

            //이미지 다운로드
            for (int i = 0; i < jsonList.data.Count; i++)
            {
                DownloadVillageImage(jsonList.data[i].image, i);
            }

            firstStage.SetActive(false);
            secondStage.SetActive(true);
        });


        //info 의 정보로 요청을 보내자
        HttpManager.Get().SendRequest(info);
    }

    public Image[] villageImages;
    public void DownloadVillageImage(string imageUrl, int imageId)
    {
        HttpInfo info = new HttpInfo();
        info.Set(
            RequestType.TEXTURE,
            imageUrl,
            null,
            false);
        info.imageId = imageId;
        info.onReceiveImage = (DownloadHandler downloadHandler, int idx) =>
        {

            Texture2D texture = ((DownloadHandlerTexture)downloadHandler).texture;
            villageImages[idx].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        };

        HttpManager.Get().SendRequest(info);
    }

    public void OnAdventureButton()
    {
        HttpInfo info = new HttpInfo();
        info.Set(RequestType.GET, "/activity/name", (DownloadHandler downloadHandler) =>
        {
            print("NetActivity : " + downloadHandler.text);

            JsonList<NetActivityInfo> jsonList = JsonUtility.FromJson<JsonList<NetActivityInfo>>(downloadHandler.text);

            //이미지 다운로드
            for (int i = 0; i < 3; i++)
            {
                DownloadActivityImage(jsonList.data[i].image, i);
                //print(i + " : " + jsonList.data[i].image);
            }
            secondStage.SetActive(false);
            thirdStage.SetActive(true);
        });
        //info 의 정보로 요청을 보내자
        HttpManager.Get().SendRequest(info);
    }
    public Image[] ActivityImages;
    public void DownloadActivityImage(string imageUrl, int imageId)
    {
        HttpInfo info = new HttpInfo();
        info.Set(
            RequestType.TEXTURE,
            imageUrl,
            null,
            false);
        info.imageId = imageId;
        info.onReceiveImage = (DownloadHandler downloadHandler, int idx) =>
        {

            Texture2D texture = ((DownloadHandlerTexture)downloadHandler).texture;
            ActivityImages[idx].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        };

        HttpManager.Get().SendRequest(info);
    }

    public void OnExitButton()
    {
        thirdStage.SetActive(false);
        //패널이 꺼진다.
        panel.SetActive(false);
        //맵으로 가서 마을로 이동한다.
    }

}
