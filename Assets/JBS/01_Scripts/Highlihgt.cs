using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlihgt : MonoBehaviour
{
    //메시 렌더러
    public Renderer objRenderer;
    //렌더러 초기 머티리얼 값
    Material[] objMatList;
    //아웃라인 머티리얼
    Material outline;
    //머티리얼 리스트
    [SerializeField]List<Material> materialList = new List<Material>();
    public bool isHighlight = false;

    private void Awake() {
        outline = new Material(Shader.Find("Mingyu/Outline"));
        objMatList = objRenderer.materials;
    }

    private void LateUpdate() {
        CheckHighlight();    
    }
    public void CheckHighlight()
    {
        if(objMatList != null)
        {
            if(isHighlight)
            {
                materialList.Clear();
                materialList.AddRange(objMatList);
                materialList.Add(outline);

                objRenderer.materials = materialList.ToArray();
            }
            else
            {
                materialList.Clear();
                materialList.AddRange(objMatList);
                materialList.Remove(outline);

                objRenderer.materials = materialList.ToArray();
            }
        }
    }

}
