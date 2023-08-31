using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����� ��ũ��Ʈ
public class FishMove_LHS : MonoBehaviour
{
    //��ü ��� ������ ����
    private Transform objectGrab;

    void Update()
    {
        //�̳��� ������ �� �¿�� �̵�
        if (GameManager_LHS.instance.isfishSave == true)
        {
            //���� ����
        }

        //�ƿ� ����⸦ ������� �̳� ��ġ�� �̵� . ������ ��ġ�� �̵� (Lerp ?)
        //��� ����
        if (GameManager_LHS.instance.isfishLook == true && objectGrab != null)
        {
            //Debug.Log("����� �̳���ġ��" + objectGrab.position);
            transform.position = objectGrab.position;
            //StartCoroutine(FishingManager.instance.IECatchFish());
            //StartCoroutine(DestroyFish());
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
