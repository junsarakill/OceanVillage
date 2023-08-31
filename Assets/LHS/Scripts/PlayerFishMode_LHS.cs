using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishMode_LHS: MonoBehaviour
{
    public GameObject bait;

    void Update()
    {
        //�����̽��ٸ� ������ ���ô� �ִϸ��̼� �ۿ�
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //�ø���
            if(GameManager_LHS.instance.isfishSave == true)
            {
                Debug.Log("�����̽�1��");
                //bait.GetComponent<BaitMove_LHS>().SetPos();

                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = true;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isFishPos = false;

                UIManager_LHS.instance.FishGrabUI();

                //�ٽ� ���� �� �ִ� ���·� �����
                GameManager_LHS.instance.isfishSave = false;
                if(!GameManager_LHS.instance.isfishLook)
                {
                    //����� �������
                    GameManager_LHS.instance.isfishLook = true;
                    StartCoroutine(FishingManager.instance.IECatchFish());
                }
            }

            //������
            else
            {
                Debug.Log("�����̽�2��");
              
                //X�� ȸ�� (�ڽ� ��ü�� ������Ʈ ��������) - Test��
                gameObject.GetComponentInChildren<FishRodRot_LHS>().isRodRot = false;
                gameObject.GetComponentInChildren<BaitMove_LHS>().isBaitMove = true;

                //���� UI����
                UIManager_LHS.instance.GameStartUI();

                //GameManager_LHS.instance.isfishSave = false;
                GameManager_LHS.instance.isfishLook = false;

                //@@ JBS �߰� ���� ���� ���
                AudioManager.instance.PlaySound(AudioManager.Sounds.FISHING_START);
            }
        }
    }
}
