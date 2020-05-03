using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent eventOnEnter = default;
    [SerializeField]
    private UnityEvent eventOnLeave = default;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayableCharacter character = collision.gameObject.GetComponent<PlayableCharacter>();
        if (character != null) {
            eventOnEnter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        PlayableCharacter character = collision.gameObject.GetComponent<PlayableCharacter>();
        if (character != null) {
            eventOnLeave.Invoke();
        }
    }
}
