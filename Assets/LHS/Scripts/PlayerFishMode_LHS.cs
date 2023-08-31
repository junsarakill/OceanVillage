using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



[System.Serializable]
public class FishGetInfoUser
{
    public string nickname;
    public int money;
}

[System.Serializable]
public class FishGetInfoFish
{
    public string name;
    public int price;
}

[System.Serializable]
public class FishGetInfo
{
    public FishGetInfoUser userDTO;
    public FishGetInfoFish fishDTO;
}

[System.Serializable]
public class NetFishGetInfo
{
    public FishGetInfo data;
}


public class PlayerFishMode_LHS: MonoBehaviour
{
    public GameObject bait;

    void Update()
    {
        //스파이스바를 누르면 낚시대 애니메이션 작용
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //올리기
            if(GameManager_LHS.instance.isfishSave == true)
            {
                Debug.Log("스페이스1번");
                //bait.GetComponent<BaitMove_LHS>().SetPos();

                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = true;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isFishPos = false;


                NetFish();

                
            }

            //내리기
            else
            {
                Debug.Log("스페이스2번");
              
                //X축 회전 (자식 객체의 컴포넌트 가져오기) - Test용
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = true;

                //게임 UI끄기
                UIManager_LHS.instance.GameStartUI();

                //GameManager_LHS.instance.isfishSave = false;
                GameManager_LHS.instance.isfishLook = false;

                //@@ JBS 추가 퐁당 사운드 재생
                AudioManager.instance.PlaySound(AudioManager.Sounds.FISHING_START);
            }
        }
    }

    public void NetFish()
    {
        HttpInfo info = new HttpInfo();
        info.Set(RequestType.POST, "/fish", (DownloadHandler downloadHandler) => {
            print("NetFish : " + downloadHandler.text);
            /*
             {
  "status": 201,
  "message": "생선 잡은 유저",
  "data": {
    "userDTO": {
      "nickname": "abc123",
      "money": 20000
    },
    "fishDTO": {
      "name": "갈치",
      "price": 20000
    }
  }
}
             */

            NetFishGetInfo info = JsonUtility.FromJson<NetFishGetInfo>(downloadHandler.text);

            UIManager_LHS.instance.FishGrabUI(info.data.fishDTO.name);

            //다시 잡을 수 있는 상태로 만들기
            GameManager_LHS.instance.isfishSave = false;
            if (!GameManager_LHS.instance.isfishLook)
            {
                //물고기 잡았을때
                GameManager_LHS.instance.isfishLook = true;
                StartCoroutine(FishingManager.instance.IECatchFish());
            }

        });

        SignUpInfo body = new SignUpInfo();
        body.nickname = ProjectManager.instance.myInfo.data.nickname;


        info.body = JsonUtility.ToJson(body);

        //info.Set(RequestType.GET, "/todos", OnReceiveGet);

        //info 의 정보로 요청을 보내자
        HttpManager.Get().SendRequest(info);
    }
}
