using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//각 세그먼트의 현재 및 이전 위치를 저장하는 구조체
//현재 위치와 이전 위치가 순서대로 포함될 예정
public struct RopeSegment
{
    //Vector3로 가지 않아도 되나? 점이기 때문에
    public Vector3 posNow;
    public Vector3 posOld;

    public RopeSegment(Vector3 pos)
    {
        this.posNow = pos;
        this.posOld = pos;
    }
}

public class Rop_LHS : MonoBehaviour
{
    //로프를 그리기 위한 LineRenderer
    private LineRenderer lineRenderer;
    //로프 세그먼트를 저장하는 리스트 (포인트가 되는 지점들)
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    //로프 포인트 간의 거리
    private float ropeSegLen = 0.25f;
    //로프 세그먼트 수 (라인렌더러의 길이가 될 것임)
    public int segmentLength = 20;
    //LineRenderer의 너비
    private float lineWidth = 0.01f;

    //로프 시작 위치
    [SerializeField] private Transform startPos;
    //로프 끝 위치 (물고기 잡는 위치)
    [SerializeField] private Transform endPos;

    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();

        //두점 사이의 거리를 일정하게 (시작 위치는 정해 놓을 것 - 낚싯대)
        Vector3 ropeStartPoint = startPos.position;

        //초기 로프 세그먼트 생성
        //스크린을 월드포인트로 호출하는 api 사용하여 로프 그리기위해
        //35개의 포인트 생성 -> 세그먼트 수를 반복하고 계속해서 포인트를 연결
        for (int i = 0; i < segmentLength; i++)
        {
            //포인트 갯수만큼 담아준다.
            //시작 포인트에서 ropeSegLen 거리 만큼 빼주면서
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    void Update()
    {
        //로프 그리기
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        //로프 물리 시뮬레이션
        this.Simulate();
    }

    // 잡았을때 줄이 팽팽해지는 효과
    public void SetsegmentLength(int num)
    {
        //세그먼트 수 외부에서 설정 가능
        segmentLength = num;
    }

    //1.처음 위치는 항상 마우스 포인트에서
    //2.각 점마다 일정한 거리 유지
    private void Simulate()
    {
        //▶로프 물리 시뮬레이션, 중력 및 제약 조건 포함
        //SIMULATION
        //중력
        Vector3 forceGravity = new Vector3(0f, -1.5f);

        //1 부터 틀림
        for (int i = 1; i < this.segmentLength; i++)
        {
            //※내가 원하는 값으로 만들면 된다.
            RopeSegment firstSegment = this.ropeSegments[i];
            //로프의 현재 위치에서 로프의 이전 위치를 빼서
            Vector3 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            //새로운 속도를 얻은 후
            firstSegment.posNow += velocity;
            //개별 지점에 중력을 적용
            firstSegment.posNow += forceGravity * Time.deltaTime;
            //새 위치를 계산
            this.ropeSegments[i] = firstSegment;
        }

        //▶로프 모양 유지를 위한 제약 조건 적용
        //CONSTRAINIS
        //시뮬레이터를 맞친 후 개별 지점에 제약 조건을 적용해야함
        //제약조건을 여러번 적용해야 함.(50배로 적용)
        //로프에 더 많은 제약 조건을 적용할 수록 결과가 더 정확
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    //▶로프 형태를 유지하기 위한 제약 조건 적용
    private void ApplyConstraint()
    {
        //1. 로프의 첫 번빼 위치는 항상 마우스 위치를 따릅니다.
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = this.startPos.position;
        this.ropeSegments[0] = firstSegment;

        //+ 3. 로프의 끝 세그먼트 점을 얻는다.
        RopeSegment endSegment = this.ropeSegments[segmentLength - 1];
        endSegment.posNow = this.endPos.position;
        this.ropeSegments[segmentLength - 1] = endSegment;

        //2. 로프의 두 지점이 항상 일정한 거리를 유지 (ropeSegLen)
        //그래서 로프가 너무 길면 줄여야 되고 너무 짧으면 늘려 두 세그먼트 사이의 거리가 같아야 했다.
        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            //오류를 계산하기 위해 먼저
            //현재 두점의 크기를 계산 (D)
            //필요한 거리에서 B를 빼고 D로 나누면 두점의 한계가 10 인 경우

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            //데이터 절댓값 반환 (-100) -> 100
            float error = Mathf.Abs(dist - this.ropeSegLen);
            //◀
            Vector3 changeDir = Vector3.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            //◀
            Vector3 changeAmount = changeDir * error;
            if (i != 0)
            {
                //0.5틀림
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            }
            else
            {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    //LineRenderer를 사용하여 로프 그리기
    //라인렌더러의 너비를 설정하고
    //라인렌더러에 추가할 위치배열을 만들 것
    private void DrawRope()
    {
        //처음과 끝 너비 같게 -> 컴포넌트에서 조절?
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        //포인트 목록을 반복하고 다시 배열에 추가
        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
}
