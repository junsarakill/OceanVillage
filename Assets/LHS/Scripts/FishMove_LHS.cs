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
            //�� ��ġ�� �̳� ��ġ�� ���ƾ� �Ѵ�. Lerp�̵�

            //���� �������
            Debug.Log("����� �̳���ġ��");
        }
    }
}
