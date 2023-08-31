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
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            //NetSignUp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            NetFish();
        }

        if(Input.GetKeyDown(KeyCode.Alpha7))
        {
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            //NetActivity();
        }
    }

    public void NetActivity()
    {
        HttpInfo info = new HttpInfo();
        info.Set(RequestType.GET, "/activity/name", (DownloadHandler downloadHandler) => {
            print("NetActivity : " + downloadHandler.text);

            JsonList<NetActivityInfo> jsonList = JsonUtility.FromJson<JsonList<NetActivityInfo>>(downloadHandler.text);

            //jsonList.data[0].price


        });


        //info 의 정보로 요청을 보내자
        HttpManager.Get().SendRequest(info);
    }

    ///village/flounder
    

    public void NetFish()
    {
        HttpInfo info = new HttpInfo();
        info.Set(RequestType.POST, "/fish", (DownloadHandler downloadHandler) => {
            print("NetFish : " + downloadHandler.text);
        });

        SignUpInfo body = new SignUpInfo();
        body.nickname = "현숙이";


        info.body = JsonUtility.ToJson(body);

        //info.Set(RequestType.GET, "/todos", OnReceiveGet);

        //info 의 정보로 요청을 보내자
        HttpManager.Get().SendRequest(info);
    }

    void OnReceiveGet(DownloadHandler downloadHandler)
    {
        print("OnReceiveGet : " + downloadHandler.text);
    }

    
    // userId: \"Clementine Bauch\", password = \"Samantha\", nickname: \"Nathan@yesenia.net\" 
    public void NetSignUp()
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/sign-up", (DownloadHandler downloadHandler) => {
            //Post 데이터 전송했을 때 서버로부터 응답 옵니다~
            print("NetSignUp : " +downloadHandler.text);
            userInfo = JsonUtility.FromJson<ReceiveUserInfo>(downloadHandler.text);
        });

        SignUpInfo signUpInfo = new SignUpInfo();
        
        signUpInfo.nickname = "현숙이";

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
        //다운로드된 Image 데이터를 Sprite 로 만든다.
        // Texture2D --> Sprite
        Texture2D texture = ((DownloadHandlerTexture)downloadHandler).texture;
        downloadImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}