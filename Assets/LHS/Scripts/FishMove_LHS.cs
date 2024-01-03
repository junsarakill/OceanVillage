using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//물고기 스크립트
public class FishMove_LHS : MonoBehaviour
{
    //객체 잡기 지점을 저장
    private Transform objectGrab;

    void Update()
    {
        //아예 물고기를 잡았을때 미끼 위치로 이동 . 정해진 위치로 이동 (Lerp ?)
        //잡고 나서
        if (GameManager_LHS.instance.isfishLook == true && objectGrab != null)
        {
            //Debug.Log("물고기 미끼위치로" + objectGrab.position);
            transform.position = objectGrab.position;
        }
    }

    IEnumerator DestroyFish()
    {
        yield return new WaitForSeconds(3);

        Destroy(gameObject);
    }

    public void Grab(Transform objectGrab)
    {
        this.objectGrab = objectGrab;
    }
}
