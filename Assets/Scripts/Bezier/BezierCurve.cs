using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform controlPoint;
    [SerializeField] private GameObject arrImg;

    public float lockControl;

    private int numPoints = 50;

    private void OnEnable() {
        Debug.Log("fsldhafksjdf");
    }

    private void Start()
    {
        lineRenderer.positionCount = numPoints;
    }

    private void Update()
    {
        DrawCurve();
    }

    private void DrawCurve()
    {
        SetControl((endPoint.position.x+startPoint.position.x)*0.5f ,1);
        arrImg.transform.position = endPoint.position;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            Vector3 point = CalculateBezierPoint(t, startPoint.position, controlPoint.position, endPoint.position);
            lineRenderer.SetPosition(i, point);
        }
    }

    public void SetStart(Vector3 pos,float z=0){
        startPoint.transform.position = pos + Vector3.forward*z;
    }
    void SetControl(float x,float z=0){
        controlPoint.transform.position = Vector3.right*x + Vector3.up*lockControl + Vector3.forward*z;
    }
    public void SetEnd(Vector3 pos,float z=0){
        endPoint.transform.position = pos + Vector3.forward*z;
    }

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}
