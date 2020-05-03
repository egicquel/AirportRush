using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndReceptionist : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float validationTime = 1f;

    [Header("Setup stuff")]
    [SerializeField]
    private GameObject exclamation = default;
    [SerializeField]
    private GameObject talking = default;
    [SerializeField]
    private GameObject done = default;
    [SerializeField]
    private GameObject wrong = default;
    [SerializeField]
    private GameObject invisibleWall = default;
    [SerializeField]
    private GameObject arrow = default;
    [SerializeField]
    private Sprite[] talkingSprites = default;

    private float validationTimer;
    private bool playerInsideValidationZone = false;
    private bool isDone = false;
    private float divisionForTalkingSprite = 1f;
    private SpriteRenderer talkingSpriteRenderer;
    private bool isGood = false;

    // Start is called before the first frame update
    void Start()
    {
        validationTimer = validationTime;
        exclamation.SetActive(true);
        talking.SetActive(true);
        done.SetActive(false);
        arrow.SetActive(false);
        talkingSpriteRenderer = talking.GetComponent<SpriteRenderer>();
        talking.SetActive(false);
        divisionForTalkingSprite = (validationTime * 1f) / talkingSprites.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDone) {
            return;
        }
        
        if (playerInsideValidationZone) {
            validationTimer -= Time.deltaTime;
            int indexSprite = (int)((validationTime - validationTimer) / divisionForTalkingSprite);
            if (indexSprite < talkingSprites.Length) {
                talkingSpriteRenderer.sprite = talkingSprites[indexSprite];
            }
            if (validationTimer <= 0) {
                Done();
            }
        }
    }

    public void SetEnableIsGood() {
        isGood = true;
    }

    public void EnterValidationZone() {
        if (isDone) {
            return;
        }
        playerInsideValidationZone = true;
        exclamation.SetActive(false);
        talking.SetActive(true);
    }

    public void ExitValidationZone() {
        if (isDone) {
            return;
        }
        playerInsideValidationZone = false;
        exclamation.SetActive(true);
        talking.SetActive(false);
    }

    private void Done() {
        isDone = true;
        talking.SetActive(false);
        if (isGood) {
            done.SetActive(true);
            invisibleWall.SetActive(false);
            arrow.SetActive(true);
        }
        else {
            wrong.SetActive(true);
        }
    }
}
