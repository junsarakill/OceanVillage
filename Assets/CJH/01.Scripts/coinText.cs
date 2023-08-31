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
        //text 컴포넌트 가져와서
         Text text = GetComponent<Text>();
        //텍스트는 코인
        text.text = $"{coin}";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
