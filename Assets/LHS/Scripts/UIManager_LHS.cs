using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_LHS : MonoBehaviour
{
    public static UIManager_LHS instance;
    
    [SerializeField] GameObject gameStartUI;
    [SerializeField] GameObject gameSliderUI;
    [SerializeField] GameObject FishInfoUI;

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
        gameSliderUI.SetActive(true);
        //�������� ������
    }

    //����� ������� UI
    public void FishGrabUI()
    {
        FishInfoUI.SetActive(true);
    }

    public void FishEndUI()
    {
        FishInfoUI.SetActive(false);
    }
}
