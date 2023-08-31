using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot_LHS : MonoBehaviour
{
    //누적된 회전 값
    float rotX;
    float rotY;

    public float rotSpeed = 200;
    //제한값
    public float rotClamp = 80;

    Transform trCam;

    float mx;
    float my;

    void Start()
    {
        trCam = Camera.main.transform;
    }

    void Update()
    {
        mx = Input.GetAxis("Mouse X");
        my = Input.GetAxis("Mouse Y");

        rotX += mx * rotSpeed * Time.deltaTime;
        rotY += my * rotSpeed * Time.deltaTime;

        transform.localEulerAngles = new Vector3(0, rotX, 0);

        rotY = Mathf.Clamp(rotY, -rotClamp, rotClamp);

        trCam.localEulerAngles = new Vector3(-rotY, 0, 0);
    }
}
