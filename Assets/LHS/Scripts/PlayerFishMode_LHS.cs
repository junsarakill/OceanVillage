using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishMode_LHS: MonoBehaviour
{
    /*//�̳�
    [SerializeField] GameObject bait;
    //�̳�������ġ
    [SerializeField] Transform startPos;
    //�̳�������ġ
    [SerializeField] Transform endPos;*/

    void Start()
    {
        
    }

    void Update()
    {
        //�����̽��ٸ� ������ ���ô� �ִϸ��̼� �ۿ�
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //X�� ȸ�� (�ڽ� ��ü�� ������Ʈ ��������) - Test��
            gameObject.GetComponentInChildren<FishRodRot_LHS>().RodAction(1);
            gameObject.GetComponentInChildren<BaitMove_LHS>().BaitAction(1);

            //�̳��� Ư�� ��ġ�� ������

            //���� ��ġ���� �ش� ��ġ�� �̵� y�� �̵�
        }

        //�������� ���߰�
        //�����¿� �̵��� ����
    }
}
