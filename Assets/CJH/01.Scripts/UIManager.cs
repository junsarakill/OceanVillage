using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image downloadImage;

    public ReceiveUserInfo userInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickGet()
    {
        //todos
        HttpInfo info = new HttpInfo();
        info.Set(RequestType.GET, "/todos", (DownloadHandler downloadHandler) => {
            print("OnReceiveGet : " + downloadHandler.text);
        });

        //info.Set(RequestType.GET, "/todos", OnReceiveGet);

        //info �� ������ ��û�� ������
        HttpManager.Get().SendRequest(info);
    }

    void OnReceiveGet(DownloadHandler downloadHandler)
    {
        print("OnReceiveGet : " + downloadHandler.text);
    }

    
    // userId: \"Clementine Bauch\", password = \"Samantha\", nickname: \"Nathan@yesenia.net\" 
    public void PostTest()
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/sign-up", (DownloadHandler downloadHandler) => {
            //Post ������ �������� �� �����κ��� ���� �ɴϴ�~
            print(downloadHandler.text);
            userInfo = JsonUtility.FromJson<ReceiveUserInfo>(downloadHandler.text);
        });

        SignUpInfo signUpInfo = new SignUpInfo();
        signUpInfo.userId = "��Ÿ";
        signUpInfo.password = 12345.ToString();
        signUpInfo.nickname = "��ī����";

        info.body = JsonUtility.ToJson(signUpInfo);

        HttpManager.Get().SendRequest(info);
    }


    public void OnClickDownloadImage()
    {
        HttpInfo info = new HttpInfo();
        info.Set(
            RequestType.TEXTURE,
            "https://via.placeholder.com/150/92c952",
            OnCompleteDownloadTexture,
            false);

        HttpManager.Get().SendRequest(info);
    }

    void OnCompleteDownloadTexture(DownloadHandler downloadHandler)
    {
        //�ٿ�ε�� Image �����͸� Sprite �� �����.
        // Texture2D --> Sprite
        Texture2D texture = ((DownloadHandlerTexture)downloadHandler).texture;
        downloadImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}