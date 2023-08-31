using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라의 x축 회전에 따라 미끼 x축 이동
public class BaitMove_LHS : MonoBehaviour
{
    public bool isBaitMove;

    [SerializeField] Transform endPos;

    [SerializeField] float speed = 1;

    //저장 위치
    private Vector3 savePos;

    Transform trCam;

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

        //카메라 회전에 따라 미끼의 이동도 바꾸기
        //rotY = Mathf.Clamp(rotY, -rotClamp, rotClamp);
        //trCam.localEulerAngles = new Vector3(-rotY, 0, 0);
    }

    public void BaitAction(int a)
    {
        isBaitMove = !isBaitMove;
    }
}
