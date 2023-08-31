using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ī�޶��� x�� ȸ���� ���� �̳� x�� �̵�
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

        //end ��ġ�� ����⵵ ������� 
        if(isBaitMove && isFishPos)
        {
            transform.position = fishPos;
            Debug.Log("����� ��Ҵ�");



            //�ٵ� ����������
            /*Rop_LHS ropeObj = rope.GetComponent<Rop_LHS>();
            ropeObj.SetsegmentLength(5);*/
            rope.GetComponent<Rop_LHS>().SetsegmentLength(5);
            GameManager_LHS.instance.isfishSave = true;
        }

        //ī�޶� ȸ���� ���� �̳��� �̵��� �ٲٱ�
        //rotY = Mathf.Clamp(rotY, -rotClamp, rotClamp);
        //trCam.localEulerAngles = new Vector3(-rotY, 0, 0);
    }

    public void BaitAction(int a)
    {
        isBaitMove = !isBaitMove;
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
        }
    }
}
