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

    //게임 시작 전 
    public void GameStartBeforeUI()
    {
        gameStartUI.SetActive(true);
    }

    //게임 시작 시 
    public void GameStartUI()
    {
        gameStartUI.SetActive(false);
        //게이지바 나오게
        //gameSliderUI.SetActive(true);
    }

    //물고기 잡았을때 UI
    public void FishGrabUI(string fishName)
    {
        fishInfo.text = fishName + "를(을) 잡았다.";

        //애니메이션으로 나왔다 들어가기
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
