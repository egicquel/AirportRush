using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : HumanEnemy
{
    // Start is called before the first frame update
    new void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        PlayableCharacter character = collision.collider.gameObject.GetComponent<PlayableCharacter>();
        if (character != null) {
            character.HitKid();
        }
    }
}
