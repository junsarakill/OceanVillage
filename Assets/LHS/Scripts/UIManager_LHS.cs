using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager_LHS : MonoBehaviour
{
    public static UIManager_LHS instance;
    
    [SerializeField] GameObject gameStartUI;
    [SerializeField] GameObject gameSliderUI;
    [SerializeField] GameObject FishInfoUI;
    [SerializeField] Text fishInfo;

    Animator anim;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //���� ���� �� 
    public void GameStartBeforeUI()
    {
        gameStartUI.SetActive(true);
    }

    //���� ���� �� 
    public void GameStartUI()
    {
        gameStartUI.SetActive(false);
        //�������� ������
        //gameSliderUI.SetActive(true);
    }

    //����� ������� UI
    public void FishGrabUI(string fishName)
    {
        fishInfo.text = fishName + "��(��) ��Ҵ�.";

        //�ִϸ��̼����� ���Դ� ����
        //FishInfoUI.SetActivea(true);
       // FishInfoUI.SetActive(true);
        anim = FishInfoUI.GetComponent<Animator>();
        anim.SetTrigger("Fish");// ("FishMove", true);
    }

    public void FishEndUI()
    {
        FishInfoUI.SetActive(false);
    }
}
