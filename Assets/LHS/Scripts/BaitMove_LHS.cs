using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�޶��� x�� ȸ���� ���� �̳� x�� �̵�
public class BaitMove_LHS : MonoBehaviour
{
    public bool isBaitMove;

    [SerializeField] Transform endPos;

    [SerializeField] float speed = 1;

    //���� ��ġ
    private Vector3 savePos;

    Transform trCam;

    private void Awake()
    {
        
    }

    private void Start()
    {
        //���÷� �ؾ��ϳ�?
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

        //ī�޶� ȸ���� ���� �̳��� �̵��� �ٲٱ�
        //rotY = Mathf.Clamp(rotY, -rotClamp, rotClamp);
        //trCam.localEulerAngles = new Vector3(-rotY, 0, 0);
    }

    public void BaitAction(int a)
    {
        isBaitMove = !isBaitMove;
    }
}
