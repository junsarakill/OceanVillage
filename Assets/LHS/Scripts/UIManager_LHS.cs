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

    //게임 시작 전
    public void GameStartBeforeUI()
    {
        gameStartUI.SetActive(true);
    }

    //게임 시작 시 
    public void GameStartUI()
    {
        gameStartUI.SetActive(false);
        gameSliderUI.SetActive(true);
        //게이지바 나오게
    }

    //물고기 잡았을때 UI
    public void FishGrabUI()
    {
        FishInfoUI.SetActive(true);
    }

    public void FishEndUI()
    {
        FishInfoUI.SetActive(false);
    }
}
