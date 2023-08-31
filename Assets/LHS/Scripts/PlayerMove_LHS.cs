using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_LHS : MonoBehaviour
{
    public float speed = 10;

    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        transform.position += dir * speed * Time.deltaTime;
    }
}
