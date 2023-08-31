using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라의 x축 회전에 따라 미끼 x축 이동
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

    private void Awake()
    {
        
    }

    private void Start()
    {
        //로컬로 해야하나?
        savePos = transform.position;

        trCam = Camera.main.transform;
    }

    void Update()
    {
        if(isBaitMove)
        {
            transform.position = Vector3.Lerp(transform.position, endPos.position, speed * Time.deltaTime);
        }

        else
        {
            transform.position = Vector3.Lerp(transform.position, savePos, speed * Time.deltaTime);
        }

        //end 위치고 물고기도 닿았으면 
        if(isBaitMove && isFishPos)
        {
            transform.position = fishPos;
            Debug.Log("물고기 잡았다");



            //줄도 팽팽해지게
            /*Rop_LHS ropeObj = rope.GetComponent<Rop_LHS>();
            ropeObj.SetsegmentLength(5);*/
            rope.GetComponent<Rop_LHS>().SetsegmentLength(5);
            GameManager_LHS.instance.isfishSave = true;
        }

        //카메라 회전에 따라 미끼의 이동도 바꾸기
        //rotY = Mathf.Clamp(rotY, -rotClamp, rotClamp);
        //trCam.localEulerAngles = new Vector3(-rotY, 0, 0);
    }

    public void BaitAction(int a)
    {
        isBaitMove = !isBaitMove;
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
        }
    }
}
