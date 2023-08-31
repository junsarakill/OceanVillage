using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라의 x축 회전에 따라 미끼 x축 이동 (나중 구현)
//미끼 스크립트
public class BaitMove_LHS : MonoBehaviour
{
    public bool isBaitMove;
    public bool isFishPos;

    [SerializeField] Transform endPos;

    [SerializeField] float speed = 1;
    [SerializeField] GameObject rope;

    //저장 위치
    private Vector3 savePos;

    Transform trCam;

    Vector3 fishPos;

    Vector3 startPos;

    private void Start()
    {
        //로컬로 해야하나?
        savePos = transform.position;
        startPos = endPos.position;

        trCam = Camera.main.transform;
    }

    void Update()
    {
         //end 위치고 물고기도 닿았으면 
        if (isBaitMove && isFishPos)
        {
            transform.position = fishPos;
           // Debug.Log("물고기 잡았다");

            //줄도 팽팽해지게
            rope.GetComponent<Rop_LHS>().SetsegmentLength(5);



            GameManager_LHS.instance.isfishSave = true;
        }

        else if (isBaitMove)
        {
           // Debug.Log("미끼 놓기");
            rope.GetComponent<Rop_LHS>().SetsegmentLength(20);
            transform.position = Vector3.Lerp(transform.position, endPos.position, speed * Time.deltaTime);
        }

        else
        {
            //Debug.Log("미끼 다시 원상태");
            transform.position = Vector3.Lerp(transform.position, savePos, speed * Time.deltaTime);
        }
    }

    /* public void BaitAction(int a)
     {
         isBaitMove = !isBaitMove;
     }*/
    public void SetPos()
    {
        endPos.position = startPos;
        Debug.Log("종료지점다시셋팅" + endPos.position);
    }

    //물고기에 닿으면 내 위치는 저장해놓고 물고기의 위치로
    //근데 제대로 누르면 물고기가 내 위치로 올 수 있게 하기 ^^
    private void OnTriggerEnter(Collider other)
    {
        //물고기라면
        if(other.CompareTag("Fish"))
        {
            //내 위치를 물고기 위치로 놓는다.
            isFishPos = true;
            fishPos = other.transform.position;

            other.GetComponent<FishMove_LHS>().Grab(this.transform);
        }
    }

}
