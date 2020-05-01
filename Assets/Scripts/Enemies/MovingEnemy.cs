using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingEnemy : HumanEnemy
{
    [SerializeField]
    private float moveSpeed = 1f;
    
    private List<Vector3> movementPoints;
    private int movementPointIndex = 0;
    private bool isForward = true;

    private new Renderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        movementPoints = getMovementPoints();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible) {
            if (movementPoints.Count > 1)
            {
                move();
            }
        }
    }

    private void move()
    {
        var targetPosition = movementPoints[movementPointIndex];
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        if (transform.position == targetPosition)
        {
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

    private List<Vector3> getMovementPoints()
    {
        Transform[] childrenTransforms = GetComponentsInChildren<Transform>();
        List<Vector3> movementPoints = new List<Vector3>();
        movementPoints.Add(this.transform.position);
        foreach (Transform child in childrenTransforms)
        {
            if (child.gameObject.tag == "MovementPoint")
            {
                movementPoints.Add(child.position);
            }
        }
        return movementPoints;
    }
}
