using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�޶��� x�� ȸ���� ���� �̳� x�� �̵� (���� ����)
//�̳� ��ũ��Ʈ
public class BaitMove_LHS : MonoBehaviour
{
    public bool isBaitMove;
    public bool isFishPos;

    [SerializeField] Transform endPos;

    [SerializeField] float speed = 1;
    [SerializeField] GameObject rope;

    //���� ��ġ
    private Vector3 savePos;

    Transform trCam;

    Vector3 fishPos;

    Vector3 startPos;

    private void Start()
    {
        //���÷� �ؾ��ϳ�?
        savePos = transform.position;
        startPos = endPos.position;

        trCam = Camera.main.transform;
    }

    void Update()
    {
         //end ��ġ�� ����⵵ ������� 
        if (isBaitMove && isFishPos)
        {
            transform.position = fishPos;
           // Debug.Log("����� ��Ҵ�");

            //�ٵ� ����������
            rope.GetComponent<Rop_LHS>().SetsegmentLength(5);



            GameManager_LHS.instance.isfishSave = true;
        }

        else if (isBaitMove)
        {
           // Debug.Log("�̳� ����");
            rope.GetComponent<Rop_LHS>().SetsegmentLength(20);
            transform.position = Vector3.Lerp(transform.position, endPos.position, speed * Time.deltaTime);
        }

        else
        {
            //Debug.Log("�̳� �ٽ� ������");
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
        Debug.Log("���������ٽü���" + endPos.position);
    }

    //����⿡ ������ �� ��ġ�� �����س��� ������� ��ġ��
    //�ٵ� ����� ������ ����Ⱑ �� ��ġ�� �� �� �ְ� �ϱ� ^^
    private void OnTriggerEnter(Collider other)
    {
        //�������
        if(other.CompareTag("Fish"))
        {
            //�� ��ġ�� ����� ��ġ�� ���´�.
            isFishPos = true;
            fishPos = other.transform.position;

            other.GetComponent<FishMove_LHS>().Grab(this.transform);
        }
    }

}
