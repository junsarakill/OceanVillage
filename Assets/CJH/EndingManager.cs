using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    //���� UI�� ����Ÿ� ������� ��������
    //9���� ������ 
    //���� ������ �����ٴ� �Ÿ� �����Ŵ����� �˷��ָ� ���´�

    //UI �������� ���ӿ�����Ʈ
    public GameObject firstStage;
    public GameObject secondStage;
    public GameObject thirdStage;

    //UI �̵� ��ư 
    public Button moveButton;

    //UI ü�� ��ư
    public Button adventureButton;

    //UI ���� ��ư
    public Button exitButton;

    //�г�
    public GameObject panel;



    // Start is called before the first frame update
    void Start()
    {
        //�����Ҷ� UI�� �����ִ�.
        firstStage.SetActive(false);
        secondStage.SetActive(false);
        thirdStage.SetActive(false);
        panel.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        //9�� Ű�� ������ UI�� ������.
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            print("1111");
            firstStage.SetActive(true);
            //�гε� ������.
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

            //�̹��� �ٿ�ε�
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

            //�̹��� �ٿ�ε�
            for (int i = 0; i < jsonList.data.Count; i++)
            {
                DownloadVillageImage(jsonList.data[i].image, i);
            }

            firstStage.SetActive(false);
            secondStage.SetActive(true);
        });


        //info �� ������ ��û�� ������
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

            //�̹��� �ٿ�ε�
            for (int i = 0; i < 3; i++)
            {
                DownloadActivityImage(jsonList.data[i].image, i);
                //print(i + " : " + jsonList.data[i].image);
            }
            secondStage.SetActive(false);
            thirdStage.SetActive(true);
        });
        //info �� ������ ��û�� ������
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
        //�г��� ������.
        panel.SetActive(false);
        //������ ���� ������ �̵��Ѵ�.
    }

}
