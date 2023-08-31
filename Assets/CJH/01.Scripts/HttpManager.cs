using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

//https://jsonplaceholder.typicode.com

[Serializable]
public class JsonList<T>
{
    public List<T> data;
}

[Serializable]
public struct SignUpInfo
{   
    public string nickname;
}

[Serializable]
public struct NetFishInfo
{
    public int status;
    public string message;
    public data data;
}

[Serializable]
public struct data
{
    public userDTO user;
    public fishDTO fish;
    public int price;
}

[Serializable]
public struct userDTO
{
    public string nickname;
    public int money;
}
[Serializable]
public struct fishDTO
{
    public string name;
    public int price;
}


[Serializable]
public struct NetActivityInfo
{
    public int id;
    public string image;
    public string village;
    public string villageEn;
    public string name;
    public string price;
    public string period;
    public string people;

}

[Serializable]
public struct NetVillageInfo
{
    public int id;
    public string name;
    public string image;
    public string address;
    public string fishNameEn;
}


[Serializable]
public struct UserInfo
{
    public int id;
    public string userId;
    public string password;
    public string nickname;
    public int money;
}

[Serializable]
public struct ReceiveUserInfo
{
    public int status;
    public string message;
    public UserInfo data;
}




public enum RequestType
{
    GET,
    POST,
    PUT,
    DELETE,
    TEXTURE
}

//웹 통신하기 위한 정보
public class HttpInfo
{
    public RequestType requestType;
    public string url = "";

    public string body= "{}";
    public Action<DownloadHandler> onReceive;
    public Action<DownloadHandler, int> onReceiveImage;


    public int imageId;

    public void Set(
        RequestType type, 
        string u, 
        Action<DownloadHandler> callback, 
        bool useDefaultUrl = true)
    {
        requestType = type;
        if (useDefaultUrl) url = "http://192.168.242.59:8888";
        url += u;
        onReceive = callback;
    }
}


public class HttpManager : MonoBehaviour
{
    static HttpManager instance;

    public static HttpManager Get()
    {
        if(instance == null)
        {
            //게임 오브젝트 만든다
            GameObject go = new GameObject("HttpStudy");
            //만들어진 게임 오브젝트에 HttpManager 컴포넌트 붙이자
            go.AddComponent<HttpManager>();
        }

        return instance;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //서버에게 REST API 요청 (GET, POST, PUT, DELETE)
    public void SendRequest(HttpInfo httpInfo)
    {
        StartCoroutine(CoSendRequest(httpInfo));
    }

    IEnumerator CoSendRequest(HttpInfo httpInfo)
    {
        UnityWebRequest req = null;

        //POST, GET, PUT, DELETE 분기
        switch (httpInfo.requestType)
        {
            case RequestType.GET:
                //Get방식으로 req 에 정보 셋팅
                req = UnityWebRequest.Get(httpInfo.url);
                break;
            case RequestType.POST:
                
                string str = JsonUtility.ToJson(httpInfo.body);
                req = UnityWebRequest.Post(httpInfo.url, str);
                
                byte[] byteBody = Encoding.UTF8.GetBytes(httpInfo.body);
                req.uploadHandler = new UploadHandlerRaw(byteBody);
                //헤더 추가
                req.SetRequestHeader("Content-Type", "application/json");
                
                break;
            case RequestType.PUT:
                req = UnityWebRequest.Put(httpInfo.url, httpInfo.body);
                break;
            case RequestType.DELETE:
                req = UnityWebRequest.Delete(httpInfo.url);
                break;
            case RequestType.TEXTURE:
                req = UnityWebRequestTexture.GetTexture(httpInfo.url);
                break;
        }        

        //서버에 요청을 보내고 응답이 올때까지 양보한다.
        yield return req.SendWebRequest();

        //만약에 응답이 성공했다면
        if(req.result == UnityWebRequest.Result.Success)
        {
            //print("네트워크 응답 : " + req.downloadHandler.text);


            if(httpInfo.requestType == RequestType.TEXTURE)
            {
                if(httpInfo.onReceiveImage != null)
                {
                    httpInfo.onReceiveImage(req.downloadHandler, httpInfo.imageId);
                }
            }
            else
            {
                if(httpInfo.onReceive != null)
                {
                    httpInfo.onReceive(req.downloadHandler);
                }
            }

        }
        //통신 실패
        else
        {
            print("네트워크 에러 : " + req.error);
        }

        req.Dispose();


        /*
         *  {"status":201,"message":"회원가입 완료","data":{"id":7,"userId":"메타","password":"{bcrypt}$2a$10$rUtMnwfzwq4LFCIYTG.sgOWUVAt7PfDDKZ7H68A1H1i4RjHoekkPe","nickname":"아카데미","money":0}}
         */
    }
}