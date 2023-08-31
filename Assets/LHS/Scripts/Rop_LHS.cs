using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rop_LHS : MonoBehaviour
{
    //���η������� �׸���
    private LineRenderer lineRenderer;
    //����Ʈ ���� ���׸�Ʈ 
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    //����Ʈ���� �Ÿ�
    private float ropeSegLen = 0.25f;
    //���콺 ����Ʈ ���� ����Ʈ�� ��ȯ�ϱ�(���� ����?->�ƴ� �� ����)
    private int segmentLength = 35;
    //���� �������� �ʺ�
    private float lineWidth = 0.1f;

    void Start()
    {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        //���� ������ �Ÿ��� �����ϰ� ����
        //���콺 ����Ʈ�� �Ǿ��ִµ� �� ���� ��ġ�� ���� ���� ����
        Vector3 ropeStartPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //��ũ���� ��������Ʈ�� ȣ���ϴ� api ����Ͽ� ���� �׸�������
        //35���� ����Ʈ ���� -> ���׸�Ʈ ���� �ݺ��ϰ� ����ؼ� ����Ʈ�� ����
        for(int i = 0; i < segmentLength; i++)
        {
            //����Ʈ ������ŭ ����ش�.
            //���� ����Ʈ���� ropeSegLen �Ÿ� ��ŭ ���ָ鼭
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= ropeSegLen;
        }
    }

    //���� �׸���
    //���η������� �ʺ� �����ϰ�
    //���η������� �߰��� ��ġ�迭�� ���� ��
    private void DrawRope()
    {
        //ó���� �� �ʺ� ����
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        
        //����Ʈ ����� �ݺ��ϰ� �ٽ� �迭�� �߰�
    }

    void Update()
    {
        
    }

    //���� ��ġ�� ���� ��ġ�� ������� ���Ե� ����
    public struct RopeSegment
    {
        //Vector3�� ���� �ʾƵ� �ǳ�? ���̱� ������
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
