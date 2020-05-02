using System.Collections.Generic;
using UnityEngine;

public class ThrowingEnemies : MonoBehaviour {
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
    private Vector3 loosePosition;

    private new Renderer renderer;

    // Start is called before the first frame update
    void Start() {
        movementPoints = new List<Vector3>();
        ball.transform.position = leftKid.transform.position;
        movementPoints.Add(leftKid.transform.position);
        movementPoints.Add(rightKid.transform.position);
        renderer = ball.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        if (renderer.isVisible) {
            MoveBall();
        } 
    }

    private void MoveBall() {
        var movementThisFrame = ballSpeed * Time.deltaTime;
        if (loosePosition != Vector3.zero) {
            ball.transform.position = Vector2.MoveTowards(ball.transform.position, loosePosition, movementThisFrame);
            leftKid.transform.rotation = GetDirectionFromTowards(leftKid.transform.position, ball.transform.position);
            rightKid.transform.rotation = GetDirectionFromTowards(rightKid.transform.position, ball.transform.position);
            return;
        }

        if (timerThrow > 0) {
            timerThrow -= Time.deltaTime;
            return;
        }
        
        var targetPosition = movementPoints[movementPointIndex];
        ball.transform.position = Vector2.MoveTowards(ball.transform.position, targetPosition, movementThisFrame);
        if (ball.transform.position == targetPosition) {
            timerThrow = pauseBetweenThrow;
            movementPointIndex = movementPointIndex + (isForward ? 1 : -1);
            var isLoose = false;
            if (movementPointIndex == (movementPoints.Count - 1)) {
                isForward = false;
                if (targetPosition != leftKid.transform.position) {
                    isLoose = true;
                }
            }
            else if (movementPointIndex == 0) {
                isForward = true;
                if (targetPosition != rightKid.transform.position) {
                    isLoose = true;
                }
            }

            if (isLoose) {
                Vector3 vector = movementPoints[0] - movementPoints[1];
                loosePosition = targetPosition + (isForward ? -vector : vector) * 10;
            }
        }
    }

    private Quaternion GetDirectionFromTowards(Vector3 fromPosition, Vector3 targetPosition) {
        Vector3 relativePos = targetPosition - fromPosition;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
