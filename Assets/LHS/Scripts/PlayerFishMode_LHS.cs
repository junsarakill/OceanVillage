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
        //@@ JBS �ӽ� �ּ� ó��
        //UIManager_LHS.instance.GameStartBeforeUI();
    }

    void Update()
    {
        //�����̽��ٸ� ������ ���ô� �ִϸ��̼� �ۿ�
        if(Input.GetKeyDown(KeyCode.Space))
        {

            if(GameManager_LHS.instance.isfishSave == true)
            {
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = true;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = false;
                UIManager_LHS.instance.FishGrabUI();

                //�ٽ� ���� �� �ִ� ���·� �����
                GameManager_LHS.instance.isfishSave = false;
                GameManager_LHS.instance.isfishLook = true;
            }

            else
            {
                //X�� ȸ�� (�ڽ� ��ü�� ������Ʈ ��������) - Test��
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = true;

                //���� UI����
                UIManager_LHS.instance.GameStartUI();
                //�̳��� Ư�� ��ġ�� ������

                //���� ��ġ���� �ش� ��ġ�� �̵� y�� �̵�

                //����⸦ ���� ���¶�� �ٽ� �ö�� �� �ְ� �����
            }
        }

        //�������� ���߰�
        //�����¿� �̵��� ����
    }
}
