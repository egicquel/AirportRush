using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Receptionist : MonoBehaviour
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
    private Sprite[] talkingSprites = default;
    [SerializeField]
    private UnityEvent eventOnDone = default;

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
        exclamation.SetActive(false);
        talking.SetActive(false);
        done.SetActive(false);
        talkingSpriteRenderer = talking.GetComponent<SpriteRenderer>();
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

    public void CallPlayer() {
        if (isDone) {
            return;
        }
        exclamation.SetActive(true);
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
        done.SetActive(true);
        eventOnDone.Invoke();
    }
}
