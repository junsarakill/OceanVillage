using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rop_LHS : MonoBehaviour
{
    //라인렌더러로 그리기
    private LineRenderer lineRenderer;
    //포인트 로프 세그먼트 
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    //포인트들의 거리
    private float ropeSegLen = 0.25f;
    //마우스 포인트 월드 포인트로 변환하기(삭제 예정?->아닌 거 같음)
    private int segmentLength = 35;
    //라인 렌더러의 너비
    private float lineWidth = 0.1f;

    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        //두점 사이의 거리를 일정하게 유지
        //마우스 포인트로 되어있는데 ※ 시작 위치는 정해 놓을 예정
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //스크린을 월드포인트로 호출하는 api 사용하여 로프 그리기위해
        //35개의 포인트 생성 -> 세그먼트 수를 반복하고 계속해서 포인트를 연결
        for(int i = 0; i < segmentLength; i++)
        {
            //포인트 갯수만큼 담아준다.
            //시작 포인트에서 ropeSegLen 거리 만큼 빼주면서
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    //로프 그리기
    //라인렌더러의 너비를 설정하고
    //라인렌더러에 추가할 위치배열을 만들 것
    private void DrawRope()
    {
        //처음과 끝 너비 같게
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        
        //포인트 목록을 반복하고 다시 배열에 추가
    }

    void Update()
    {
        
    }

    //현재 위치와 이전 위치가 순서대로 포함될 예정
    public struct RopeSegment
    {
        //Vector3로 가지 않아도 되나? 점이기 때문에
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
