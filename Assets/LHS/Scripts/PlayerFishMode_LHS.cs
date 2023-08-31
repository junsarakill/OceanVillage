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
        //�����̽��ٸ� ������ ���ô� �ִϸ��̼� �ۿ�
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //�ø���
            if(GameManager_LHS.instance.isfishSave == true)
            {
                Debug.Log("�����̽�1��");
                //bait.GetComponent<BaitMove_LHS>().SetPos();

                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = true;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isFishPos = false;


                NetFish();

                
            }

            //������
            else
            {
                Debug.Log("�����̽�2��");
              
                //X�� ȸ�� (�ڽ� ��ü�� ������Ʈ ��������) - Test��
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = true;

                //���� UI����
                UIManager_LHS.instance.GameStartUI();

                //GameManager_LHS.instance.isfishSave = false;
                GameManager_LHS.instance.isfishLook = false;

                //@@ JBS �߰� ���� ���� ���
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
  "message": "���� ���� ����",
  "data": {
    "userDTO": {
      "nickname": "abc123",
      "money": 20000
    },
    "fishDTO": {
      "name": "��ġ",
      "price": 20000
    }
  }
}
             */

            NetFishGetInfo info = JsonUtility.FromJson<NetFishGetInfo>(downloadHandler.text);

            UIManager_LHS.instance.FishGrabUI(info.data.fishDTO.name);

            //�ٽ� ���� �� �ִ� ���·� �����
            GameManager_LHS.instance.isfishSave = false;
            if (!GameManager_LHS.instance.isfishLook)
            {
                //����� �������
                GameManager_LHS.instance.isfishLook = true;
                StartCoroutine(FishingManager.instance.IECatchFish());
            }

        });

        SignUpInfo body = new SignUpInfo();
        body.nickname = ProjectManager.instance.myInfo.data.nickname;


        info.body = JsonUtility.ToJson(body);

        //info.Set(RequestType.GET, "/todos", OnReceiveGet);

        //info �� ������ ��û�� ������
        HttpManager.Get().SendRequest(info);
    }
}
