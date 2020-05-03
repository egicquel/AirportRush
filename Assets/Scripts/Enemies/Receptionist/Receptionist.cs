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
    private GameObject doorIndication = default;
    [SerializeField]
    private Sprite[] talkingSprites = default;
    [SerializeField]
    private AudioClip[] talkingSounds = default;
    [SerializeField]
    private AudioClip DoneSound = default;
    [SerializeField]
    private UnityEvent eventOnDone = default;
    [SerializeField]
    private Sprite[] doorSprites = default;

    private float validationTimer;
    private bool playerInsideValidationZone = false;
    private bool isDone = false;
    private float divisionForTalkingSprite = 1f;
    private SpriteRenderer talkingSpriteRenderer;
    private SpriteRenderer doorIndicationSpriteRenderer = default;
    private AudioSource audioSource;
    private int indexCompletion = 0;
    private int goodDoor;

    // Start is called before the first frame update
    void Start()
    {
        validationTimer = validationTime;
        exclamation.SetActive(false);
        done.SetActive(false);
        talking.SetActive(true);
        talkingSpriteRenderer = talking.GetComponent<SpriteRenderer>();
        talking.SetActive(false);
        doorIndication.SetActive(true);
        doorIndicationSpriteRenderer = doorIndication.GetComponent<SpriteRenderer>();
        doorIndication.SetActive(false);
        divisionForTalkingSprite = (validationTime * 1f) / talkingSprites.Length;
        audioSource = GetComponent<AudioSource>();
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
            if (indexCompletion != indexSprite && indexSprite < talkingSprites.Length) {
                indexCompletion = indexSprite;
                audioSource.clip = talkingSounds[indexSprite - 1];
                audioSource.Play();
                talkingSpriteRenderer.sprite = talkingSprites[indexSprite];
            }
            if (validationTimer <= 0) {
                doorIndicationSpriteRenderer.sprite = doorSprites[goodDoor];
                Done();
            }
        }
    }

    public void SetGoodDoor(int idGoodDoor) {
        goodDoor = idGoodDoor;
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
        doorIndication.SetActive(true);
        audioSource.clip = DoneSound;
        audioSource.Play();
        eventOnDone.Invoke();
    }
}
