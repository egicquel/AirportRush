using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : HumanEnemy {

    [SerializeField]
    private float grumpyDuration = 1f;

    private bool isGrumpy = false;
    private float grumpyTimer = 0f;

    private Quaternion originalRotation;
    
    // Start is called before the first frame update
    new void Start() {
        base.Start();
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update() {
        if (grumpyTimer > 0) {
            if (!isGrumpy) {
                isGrumpy = true;
            }
            
            grumpyTimer -= Time.deltaTime;
        }
        else if (isGrumpy) {
            isGrumpy = false;
            transform.rotation = originalRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "Player") {
            grumpyTimer = grumpyDuration;
            transform.rotation = GetDirectionTowards(collision.collider.gameObject.transform.position);
        }
    }

    private Quaternion GetDirectionTowards(Vector3 targetPosition) {
        Vector3 relativePos = targetPosition - transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90f;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
