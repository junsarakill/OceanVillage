using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������� ���⼭ �ؾ���!
public class GameManager_LHS : MonoBehaviour
{
    public static GameManager_LHS instance;

    //����� ��Ҵٴ� �� �˷��ֱ�
    public bool isfishSave =false;

    public bool isfishLook;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
