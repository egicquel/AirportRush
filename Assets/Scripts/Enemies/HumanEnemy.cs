using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEnemy : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] enemySprites = default;
    
    // Start is called before the first frame update
    public void Start() {
        if (enemySprites.Length <= 0) {
            return;
        }
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) {
            return;
        }

        spriteRenderer.sprite = GetRandomSprite();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private Sprite GetRandomSprite() {
        int random = Random.Range(0, enemySprites.Length);
        return enemySprites[random];
    }
}
