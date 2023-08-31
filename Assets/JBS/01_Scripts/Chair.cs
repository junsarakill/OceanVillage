using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    //의자 하이라이트 및 빌보드 ui
    
    //플레이어 상호작용 여부
    public bool isInteract = false;
    public bool IS_INTERACT
    {
        get{return isInteract;}
        set
        {
            isInteract = value;
            chairCanvas.SetActive(value);
            hl.isHighlight = value;
        }
    }

    //대화상자 ui
    [SerializeField]GameObject chairCanvas;
    //하이라이트
    Highlihgt hl;

    private void Awake() {
        hl = GetComponent<Highlihgt>();
    }

    private void Update() {
        //낚시 ui 빌보드
        chairCanvas.transform.forward = Camera.main.transform.forward;
    }
}
