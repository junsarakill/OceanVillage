using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishRodRot_LHS : MonoBehaviour
{
    public bool isRodRot;

    [SerializeField] float rodStartAngle = 3;
    [SerializeField] float rodGameAngle = 60;

    [SerializeField] float speed = 1;

    //저장 위치
    Quaternion targetRotation;

    void Update()
    {
        if(isRodRot)
        {
            //X축 회전
            Quaternion targetRotation = Quaternion.Euler(rodStartAngle, 0, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, speed * Time.deltaTime);
        }

        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(rodGameAngle, 0, 0);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation2, speed * Time.deltaTime);
        }
    }

    public void RodAction(int a)
    {
        isRodRot = !isRodRot;
    }
}
