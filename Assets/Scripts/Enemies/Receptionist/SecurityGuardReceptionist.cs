using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityGuardReceptionist : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private float grumpyDuration = 1f;

    [SerializeField]
    private GameObject deny = default;

    private bool isGrumpy = false;
    private float grumpyTimer = 0f;

    private List<Vector3> movementPoints;
    private int movementPointIndex = 0;
    private bool isMoving = false;
    private bool allowPlayer = false;

    private new Renderer renderer;

    // Start is called before the first frame update
    void Start() {
        movementPoints = getMovementPoints();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        if (renderer.isVisible) {
            if (grumpyTimer > 0) {
                if (!isGrumpy) {
                    isGrumpy = true;
                }
                grumpyTimer -= Time.deltaTime;
            }
            else {
                if (isGrumpy) {
                    isGrumpy = false;
                }
                
            }
        }
        if (isMoving && movementPoints.Count > 1) {
            move();
        }
    }

    private void move() {
        var targetPosition = movementPoints[movementPointIndex];
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        //var rotationThisFrame = rotationSpeed * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, GetDirectionTowards(targetPosition), rotationThisFrame);
        transform.rotation = GetDirectionTowards(targetPosition);
        if (transform.position == targetPosition) {
            movementPointIndex += 1;
            if (movementPointIndex == movementPoints.Count) {
                isMoving = false;
            }
        }
    }

    private Quaternion GetDirectionTowards(Vector3 targetPosition) {
        Vector3 relativePos = targetPosition - transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private List<Vector3> getMovementPoints() {
        Transform[] childrenTransforms = GetComponentsInChildren<Transform>();
        List<Vector3> movementPoints = new List<Vector3>();
        movementPoints.Add(this.transform.position);
        foreach (Transform child in childrenTransforms) {
            if (child.gameObject.tag == "MovementPoint") {
                movementPoints.Add(child.position);
            }
        }
        return movementPoints;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!allowPlayer) {
            return;
        }
        PlayableCharacter character = collision.collider.gameObject.GetComponent<PlayableCharacter>();
        if (character != null) {
            grumpyTimer = grumpyDuration;
            transform.rotation = GetDirectionTowards(character.gameObject.transform.position);
            character.HitAdult();
        }
    }

    public void DoMove() {
        isMoving = true;
        allowPlayer = true;
    }

    public void Deny() {
        if (allowPlayer) {
            return;
        }
        deny.SetActive(true);
    }

    public void StopDeny() {
        deny.SetActive(false);
    }
}
