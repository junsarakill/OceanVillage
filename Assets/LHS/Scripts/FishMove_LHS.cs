using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove_LHS : MonoBehaviour
{
    //��ü ��� ������ ����
    private Transform objectGrab;

    void Start()
    {
        
    }

    void Update()
    {
        //�̳��� ������ �� �¿�� �̵�
        if (GameManager_LHS.instance.isfishSave == true)
        {

        }

        //�ƿ� ����⸦ ������� �̳� ��ġ�� �̵� . ������ ��ġ�� �̵� (Lerp ?)
        if (GameManager_LHS.instance.isfishLook == true)
        {
            Debug.Log("����� �̳���ġ��");

            transform.position = objectGrab.position;
        }
    }

    public void Grab(Transform objectGrab)
    {
        this.objectGrab = objectGrab;
    }
}
