using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove_LHS : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager_LHS.instance.isfishLook == true)
        {
            //내 위치는 미끼 위치로 놓아야 한다. Lerp이동

            //완전 잡았을때
            Debug.Log("물고기 미끼위치로");
        }
    }
}
