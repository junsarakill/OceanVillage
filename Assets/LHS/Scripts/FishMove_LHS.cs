using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove_LHS : MonoBehaviour
{
    //객체 잡기 지점을 저장
    private Transform objectGrab;

    void Start()
    {
        
    }

    void Update()
    {
        //미끼에 물렸을 때 좌우로 이동
        if (GameManager_LHS.instance.isfishSave == true)
        {

        }

        //아예 물고기를 잡았을때 미끼 위치로 이동 . 정해진 위치로 이동 (Lerp ?)
        if (GameManager_LHS.instance.isfishLook == true)
        {
            Debug.Log("물고기 미끼위치로");

            transform.position = objectGrab.position;
        }
    }

    public void Grab(Transform objectGrab)
    {
        this.objectGrab = objectGrab;
    }
}
