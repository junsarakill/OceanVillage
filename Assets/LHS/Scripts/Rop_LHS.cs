using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ���׸�Ʈ�� ���� �� ���� ��ġ�� �����ϴ� ����ü
//���� ��ġ�� ���� ��ġ�� ������� ���Ե� ����
public struct RopeSegment
{
    //Vector3�� ���� �ʾƵ� �ǳ�? ���̱� ������
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
    //������ �׸��� ���� LineRenderer
    private LineRenderer lineRenderer;
    //���� ���׸�Ʈ�� �����ϴ� ����Ʈ (����Ʈ�� �Ǵ� ������)
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    //���� ����Ʈ ���� �Ÿ�
    private float ropeSegLen = 0.25f;
    //���� ���׸�Ʈ �� (���η������� ���̰� �� ����)
    public int segmentLength = 20;
    //LineRenderer�� �ʺ�
    private float lineWidth = 0.01f;

    //���� ���� ��ġ
    [SerializeField] private Transform startPos;
    //���� �� ��ġ (����� ��� ��ġ)
    [SerializeField] private Transform endPos;

    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();

        //���� ������ �Ÿ��� �����ϰ� (���� ��ġ�� ���� ���� �� - ���˴�)
        Vector3 ropeStartPoint = startPos.position;

        //�ʱ� ���� ���׸�Ʈ ����
        //��ũ���� ��������Ʈ�� ȣ���ϴ� api ����Ͽ� ���� �׸�������
        //35���� ����Ʈ ���� -> ���׸�Ʈ ���� �ݺ��ϰ� ����ؼ� ����Ʈ�� ����
        for (int i = 0; i < segmentLength; i++)
        {
            //����Ʈ ������ŭ ����ش�.
            //���� ����Ʈ���� ropeSegLen �Ÿ� ��ŭ ���ָ鼭
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    void Update()
    {
        //���� �׸���
        this.DrawRope();
    }

    private void FixedUpdate()
    {
        //���� ���� �ùķ��̼�
        this.Simulate();
    }

    // ������� ���� ���������� ȿ��
    public void SetsegmentLength(int num)
    {
        //���׸�Ʈ �� �ܺο��� ���� ����
        segmentLength = num;
    }

    //1.ó�� ��ġ�� �׻� ���콺 ����Ʈ����
    //2.�� ������ ������ �Ÿ� ����
    private void Simulate()
    {
        //������ ���� �ùķ��̼�, �߷� �� ���� ���� ����
        //SIMULATION
        //�߷�
        Vector3 forceGravity = new Vector3(0f, -1.5f);

        //1 ���� Ʋ��
        for (int i = 1; i < this.segmentLength; i++)
        {
            //�س��� ���ϴ� ������ ����� �ȴ�.
            RopeSegment firstSegment = this.ropeSegments[i];
            //������ ���� ��ġ���� ������ ���� ��ġ�� ����
            Vector3 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            //���ο� �ӵ��� ���� ��
            firstSegment.posNow += velocity;
            //���� ������ �߷��� ����
            firstSegment.posNow += forceGravity * Time.deltaTime;
            //�� ��ġ�� ���
            this.ropeSegments[i] = firstSegment;
        }

        //������ ��� ������ ���� ���� ���� ����
        //CONSTRAINIS
        //�ùķ����͸� ��ģ �� ���� ������ ���� ������ �����ؾ���
        //���������� ������ �����ؾ� ��.(50��� ����)
        //������ �� ���� ���� ������ ������ ���� ����� �� ��Ȯ
        for (int i = 0; i < 50; i++)
        {
            this.ApplyConstraint();
        }
    }

    //������ ���¸� �����ϱ� ���� ���� ���� ����
    private void ApplyConstraint()
    {
        //1. ������ ù ���� ��ġ�� �׻� ���콺 ��ġ�� �����ϴ�.
        RopeSegment firstSegment = ropeSegments[0];
        firstSegment.posNow = this.startPos.position;
        this.ropeSegments[0] = firstSegment;

        //+ 3. ������ �� ���׸�Ʈ ���� ��´�.
        RopeSegment endSegment = this.ropeSegments[segmentLength - 1];
        endSegment.posNow = this.endPos.position;
        this.ropeSegments[segmentLength - 1] = endSegment;

        //2. ������ �� ������ �׻� ������ �Ÿ��� ���� (ropeSegLen)
        //�׷��� ������ �ʹ� ��� �ٿ��� �ǰ� �ʹ� ª���� �÷� �� ���׸�Ʈ ������ �Ÿ��� ���ƾ� �ߴ�.
        for (int i = 0; i < this.segmentLength - 1; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            //������ ����ϱ� ���� ����
            //���� ������ ũ�⸦ ��� (D)
            //�ʿ��� �Ÿ����� B�� ���� D�� ������ ������ �Ѱ谡 10 �� ���

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            //������ ���� ��ȯ (-100) -> 100
            float error = Mathf.Abs(dist - this.ropeSegLen);
            //��
            Vector3 changeDir = Vector3.zero;

            if (dist > ropeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < ropeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            //��
            Vector3 changeAmount = changeDir * error;
            if (i != 0)
            {
                //0.5Ʋ��
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

    //LineRenderer�� ����Ͽ� ���� �׸���
    //���η������� �ʺ� �����ϰ�
    //���η������� �߰��� ��ġ�迭�� ���� ��
    private void DrawRope()
    {
        //ó���� �� �ʺ� ���� -> ������Ʈ���� ����?
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        //����Ʈ ����� �ݺ��ϰ� �ٽ� �迭�� �߰�
        Vector3[] ropePositions = new Vector3[this.segmentLength];
        for (int i = 0; i < this.segmentLength; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }
}
