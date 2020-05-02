using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vomito : MonoBehaviour
{
    [SerializeField]
    private float unstableTime = 2f;
    
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
            character.BecomeUnstable(unstableTime, 0.2f);
        }
    }
}
