using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임진행 여기서 해야함!
public class GameManager_LHS : MonoBehaviour
{
    public static GameManager_LHS instance;

    //물고기 잡았다는 거 알려주기
    public bool isfishSave =false;

    public bool isfishLook;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
