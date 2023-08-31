using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class INtro : MonoBehaviour
{
    public InputField inputId;

    public void NetSignUp()
    {
        HttpInfo info = new HttpInfo();

        info.Set(RequestType.POST, "/sign-up", (DownloadHandler downloadHandler) => {
            //Post ������ �������� �� �����κ��� ���� �ɴϴ�~
            print("NetSignUp : " + downloadHandler.text);
            ProjectManager.instance.myInfo = JsonUtility.FromJson<ReceiveUserInfo>(downloadHandler.text);

            SceneManager.LoadScene("GameScene0901");

        });

        SignUpInfo signUpInfo = new SignUpInfo();

        signUpInfo.nickname = inputId.text;

        info.body = JsonUtility.ToJson(signUpInfo);

        HttpManager.Get().SendRequest(info);
    }
}
