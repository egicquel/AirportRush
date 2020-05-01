using System.Collections.Generic;
using UnityEngine;

public class ThrowingEnemies : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float ballSpeed = 1f;
    [SerializeField]
    private float pauseBetweenThrow = 0f;

    [Header("Setup stuff")]
    [SerializeField]
    private GameObject leftKid = default;
    [SerializeField]
    private GameObject rightKid = default;
    [SerializeField]
    private GameObject ball = default;

    private List<Vector3> movementPoints;
    private int movementPointIndex = 0;
    private bool isForward = true;
    private float timerThrow = 0f;

    private new Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        movementPoints = new List<Vector3>();
        ball.transform.position = leftKid.transform.position;
        movementPoints.Add(leftKid.transform.position);
        movementPoints.Add(rightKid.transform.position);
        renderer = ball.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible)
        {
            MoveBall();
        } 
    }

    private void MoveBall()
    {
        if (timerThrow > 0)
        {
            timerThrow -= Time.deltaTime;
            return;
        }
        
        var targetPosition = movementPoints[movementPointIndex];
        var movementThisFrame = ballSpeed * Time.deltaTime;
        ball.transform.position = Vector2.MoveTowards(ball.transform.position, targetPosition, movementThisFrame);
        if (ball.transform.position == targetPosition)
        {
            timerThrow = pauseBetweenThrow;
            movementPointIndex = movementPointIndex + (isForward ? 1 : -1);
            if (movementPointIndex == (movementPoints.Count - 1))
            {
                isForward = false;
            }
            else if (movementPointIndex == 0)
            {
                isForward = true;
            }
        }
    }
}
