using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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



    // Start is called before the first frame update
    void Start()
    {
        //�����Ҷ� UI�� �����ִ�.
        firstStage.SetActive(false);
        secondStage.SetActive(false);
        thirdStage.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        //9�� Ű�� ������ UI�� ������.
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            print("1111");
            firstStage.SetActive(true);
        }
    }
    public void OnChangeButton()
    {
        firstStage.SetActive(false);
        secondStage.SetActive(true);
    }
    public void OnAdventureButton()
    {
        secondStage.SetActive(false);
        thirdStage.SetActive(true);
    }
    public void OnExitButton()
    {
        thirdStage.SetActive(false);
        //������ ���� ������ �̵��Ѵ�.
    }

}
