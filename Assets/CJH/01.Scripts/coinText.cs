using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class coinText : MonoBehaviour
{
    int coin;


    // Start is called before the first frame update
    void Start()
    {
        coin = FishingManager.instance.SCORE;
        //text ������Ʈ �����ͼ�
         Text text = GetComponent<Text>();
        //�ؽ�Ʈ�� ����
        text.text = $"{coin}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
