using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;


[System.Serializable] // ����ȭ
public struct FishInfo
{
    //���� ���̵�(���̵� �ȿ� �̸��� ������ ����������)
    public int id;
    //�̸�
    public string fishName;
    //����
    public string city;
    //����
    public string value;

}

[System.Serializable]
public struct FishMenuList
{
    public List<FishInfo> data;
}

public class Json : MonoBehaviour
{
    //������� ����
    FishInfo fishInfo;

    //����� ���� ������ ��� �ִ� ����
    public List<FishInfo> fishBowlList = new List<FishInfo>();

    //fishbowlList�� key���� ������ֱ� ���� ����ü
    //FishMenuList info = new FishMenuList();


    // Start is called before the first frame update
    void Start()
    {

        //���� ��ġ ���ڹ� ������ �������� 
        //������ �� ����������
        //���� ���� ����������
        
        

        fishInfo = new FishInfo();

        fishInfo.id = 0;
        fishInfo.fishName = "���� : ����";
        fishInfo.city = "����ϵ� ������ 00����";
        fishInfo.value = 10000 + "��";
        fishBowlList.Add(fishInfo);


        fishInfo.id = 1;
        fishInfo.fishName = "���� : ��ġ";
        fishInfo.city = "���ϵ� ������ 00����";
        fishInfo.value = 20000 + "��";
        fishBowlList.Add(fishInfo);

        fishInfo.id = 2;
        fishInfo.fishName = "���� : ���ڹ�";
        fishInfo.city = "���󳲵� ������ 00����";
        fishInfo.value = 25000 + "��";
        fishBowlList.Add(fishInfo);

        FishMenuList info = new FishMenuList();
        info.data = fishBowlList;

        string s= JsonUtility.ToJson(info,true);
        print(s);

        //jsonDataTest test = new jsonDataTest();
        //test.jsonData = fishBowlList;
        //{
        //    "FishName" : "����",
        //    "city" : "����",
        //    "value" : "10000",
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //1�� Ű ������ json ���·� ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
             int rand = Random.Range(0, fishBowlList.Count);

            //����Ʈ ��û�ϴ� url��ũ
            print(fishBowlList[rand].fishName);
            return;

            //fishInfo�� json���·� ������
            string jsonData = JsonUtility.ToJson(fishInfo, true);
            print(jsonData);

            //jsonData�� ���Ϸ� ����
            FileStream file = new FileStream(Application.dataPath + " /fishInfo.txt",FileMode.Create);

            //json String �����͸� byte�迭�� �����
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);

            //byteData�� file�� ����
            file.Write(byteData,0,byteData.Length);

            file.Close();
        }

        //2�� Ű ������ txt�о����
        if(Input.GetKey(KeyCode.Alpha2))
        {
            FileStream file = new FileStream(Application.dataPath + "/fishInfo.txt", FileMode.Open);
            //file ũ�⸸ŭ byte �迭 �Ҵ�
            byte[] byteData = new byte[file.Length];
            //byteData�� file�� ������ �о�´�
            file.Read(byteData,0,byteData.Length);

            //���� �ݱ�
            file.Close();
            //byteData�� ���ڿ��� �ٲ���
            string jsonData = Encoding.UTF8.GetString(byteData);

            //���ڿ��� �Ǿ��ִ� jsonData�� fishInfo�� parsing�Ѵ�
            fishInfo = JsonUtility.FromJson<FishInfo>(jsonData);
        }
    }

    //����� �� ������ ��û 

    //������ ������ �ҷ�����Ų��

    //fishinfo.data �߰�

    //����Ⱑ ������ �� Ư�� url -> get or post

    //���� ���� ����� ����Ʈ�� �����ش�(�κ��丮,)

    //

    //������ ���� ����� ���� ����

    //�� ��������






}
