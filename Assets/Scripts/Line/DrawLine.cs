using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    LineRenderer line;
    public GameObject currentLine;
    public GameObject linePrefab;

    public List<Vector3> linePositions = new List<Vector3>();

    GameObject ball;
    private void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }
    private void Start()
    {
        UpdatePosition();
    }
    public void CreateLine(Vector3 startPos)
    {
        linePositions.Clear();

        currentLine = Instantiate(linePrefab, ball.transform.position, Quaternion.identity);
        
        line = currentLine.GetComponent<LineRenderer>();
        line.SetPosition(0, startPos);
        line.SetPosition(1, startPos);
        linePositions.Add(startPos);

    }

    public void UpdateLine(Vector3 newPos)
    {
        line.positionCount++;
        line.SetPosition(line.positionCount-1, newPos);
        if (line.positionCount % 3 == 0)
        {
            linePositions.Add(newPos);
        }
        
    }

    public void EndLine(Vector3 endPos)
    {
        line.SetPosition(line.positionCount-1, endPos);
        linePositions.Add(endPos);
    }

    public void UpdatePosition()
    {
        transform.position = ball.transform.position;
    }
}
