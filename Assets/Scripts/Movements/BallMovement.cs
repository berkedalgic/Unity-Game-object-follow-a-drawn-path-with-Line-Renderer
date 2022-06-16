using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    DrawLine drawLine;
    float speed;
    private void Awake()
    {
        drawLine = GameObject.FindGameObjectWithTag("LineController").GetComponent<DrawLine>();
    }
    private void Start()
    {
        speed = .5f;
    }
    public IEnumerator MovementBall()
    {
        for (int i = 1; i < drawLine.linePositions.Count; i++)
        {
            yield return new WaitForSeconds(.01f);
            transform.position = Vector3.MoveTowards(drawLine.linePositions[i-1], drawLine.linePositions[i - 1], Time.deltaTime* speed);
        }

    }
}
