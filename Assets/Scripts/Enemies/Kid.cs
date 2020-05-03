using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : HumanEnemy
{
    private AudioSource audioSource;
    private bool hasAlreadyBeenHit = false;

    // Start is called before the first frame update
    new void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayableCharacter character = collision.collider.gameObject.GetComponent<PlayableCharacter>();
        if (character != null) {
            if (!hasAlreadyBeenHit) {
                audioSource.Play();
                hasAlreadyBeenHit = true;
            }
            character.HitKid();
        }
    }
}
