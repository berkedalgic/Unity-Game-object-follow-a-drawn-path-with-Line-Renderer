using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Shoting : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    float topX;

    DrawLine drawLine;
    BallMovement ball;

    bool lineStatus;

    private void Awake()
    {
        drawLine = GameObject.FindGameObjectWithTag("LineController").GetComponent<DrawLine>();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<BallMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Vector3.zero;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Ball"))
                {
                    lineStatus = true;
                    startPosition = hit.point;
                    drawLine.CreateLine(hit.point);
                    Debug.Log(startPosition);
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (lineStatus)
            {
                topX = 0;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Plane"))
                    {
                        if (Mathf.Abs(hit.point.x) > topX)
                        {
                            topX = hit.point.x;
                            if(Mathf.Round(drawLine.linePositions[drawLine.linePositions.Count-1].x) != Mathf.Round( hit.point.x) || 
                                Mathf.Round(drawLine.linePositions[drawLine.linePositions.Count - 1].z) != Mathf.Round(hit.point.z))
                            {
                                drawLine.UpdateLine(new Vector3(hit.point.x, 1, hit.point.z));
                            }
                                
                        }
                    }
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (lineStatus)
            {
                endPosition = Vector3.zero;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag("Plane"))
                    {
                        endPosition = hit.point;
                        drawLine.EndLine(new Vector3(hit.point.x, 1, hit.point.z));
                        Destroy(drawLine.currentLine);
                        StartCoroutine(ball.MovementBall());
                        drawLine.UpdatePosition();
                        Debug.Log(endPosition);
                        Debug.Log(topX);
                        lineStatus = false;
                    }
                }
            }
           
        }

    }
}
