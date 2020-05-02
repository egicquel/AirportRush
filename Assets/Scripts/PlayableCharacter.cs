﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    public bool   isCarryingSuitcase;
    public bool   isRunning;
    public int    suitcaseWeight;
    public float  characterSpeed = 8f;
    public int    speedModificator = 1;
    public int    impactLevelRisk;
    [SerializeField]
    public float runningMultiplicator = 1.6f;
    [SerializeField]
    public float unstableTime = 1.5f;
    [SerializeField]
    public float fallenTime = 0.8f;
    [SerializeField]
    [Range(0f, 1f)]
    public float steadinessMax = 1f;
    [SerializeField]
    [Range(0f, 1f)]
    public float steadinessMin = 0.1f;

    private bool isOnTheGround = false;
    private bool isUnstable = false;
    private float steadiness;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] public GameObject suitcase;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        steadiness = steadinessMin;
        isRunning = false;
        isCarryingSuitcase = true;
        animator.SetBool("hasSuitcase", isCarryingSuitcase);
        suitcaseWeight = 1000;
        this.UpdateVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        //Event Listener Sprint
        if (Input.GetButtonDown("Sprint")) {
            this.StartRunning();
        }
        if (Input.GetButtonUp("Sprint")) {
            this.StopRunning();
        }

        //Event Listener Suitcase
        if (Input.GetButtonDown("DropSuitcase") && isCarryingSuitcase)
        {
            this.DropSuitcase();
        }
        if (Input.GetButtonDown("Interact") && !isCarryingSuitcase)
        {
            this.PickupSuitcase();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        /*if (Input.GetAxis("Vertical") > 0) {
            rb.AddForce(-transform.up * speedModificator);
        } else if (Input.GetAxis("Vertical") < 0) {
            rb.AddForce(transform.up * speedModificator);
        } else if (Input.GetAxis("Horizontal") > 0) {
            rb.AddForce(-transform.right * speedModificator);
        } else if (Input.GetAxis("Horizontal") < 0) {
            rb.AddForce(transform.right * speedModificator);
        }

        rb.position = (rb.position + movement * characterSpeed * Time.fixedDeltaTime * (isRunning ? 1.60f : 1));*/

        if (isOnTheGround) {
            rb.velocity = Vector2.zero;
            return;
        }

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        if (IsMoveButtonsPressed(horizontalAxis, verticalAxis)) {
            float characterAngle = Mathf.Atan2(-horizontalAxis, verticalAxis) * Mathf.Rad2Deg;
            rb.rotation = characterAngle;
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }
        Vector2 wantedVelocity = new Vector2(horizontalAxis, verticalAxis) * characterSpeed * Time.fixedDeltaTime * (isRunning ? runningMultiplicator : 1f);
        if (isUnstable) {
            wantedVelocity.x = (wantedVelocity.x != 0 ? wantedVelocity.x : Random.Range(-1f, 1f)) * Random.Range(0.1f, 1.5f);
            wantedVelocity.y = (wantedVelocity.y != 0 ? wantedVelocity.y : Random.Range(-1f, 1f)) * Random.Range(0.1f, 1.5f);
        }
        Debug.Log(IsVelocityUnderThreshold(wantedVelocity));
        if (IsVelocityUnderThreshold(wantedVelocity)) {
            rb.velocity = wantedVelocity;
        }
        else {
            rb.velocity = Vector2.Lerp(rb.velocity, wantedVelocity, steadiness);
        }
    }

    private bool IsVelocityUnderThreshold(Vector2 velocity) {
        return velocity.x < 0.5f && velocity.x > -0.5f && velocity.y < 0.5f && velocity.y > -0.5f;
    }

    private bool IsMoveButtonsPressed(float horizontalAxis, float verticalAxis) {
        return horizontalAxis < -0.2 || horizontalAxis > 0.2 || verticalAxis < -0.2 || verticalAxis > 0.2;
    }

    public void HitAdult() {
        if (isUnstable) {
            Fall();
        }
        else if (isRunning && isCarryingSuitcase) {
            BecomeUnstable();
        }
    }

    public void HitKid() {
        if (isRunning) {
            LoseClothes();
            rb.velocity = Vector2.zero;
        }
    }

    private void BecomeUnstable() {
        isUnstable = true;
        animator.SetBool("isUnstable", isUnstable);
        Invoke("BecomeStable", unstableTime);
    }

    private void BecomeStable() {
        isUnstable = false;
        animator.SetBool("isUnstable", isUnstable);
    }

    private void Fall() {
        isOnTheGround = true;
        isUnstable = false;
        animator.SetBool("isUnstable", isUnstable);
        animator.SetBool("isFallen", isOnTheGround);
        DropSuitcase();
        LoseClothes();
        Invoke("GetUp", fallenTime);
    }

    private void GetUp() {
        isOnTheGround = false;
        animator.SetBool("isFallen", isOnTheGround);
    }

    // Charactere lost his Suitcase and refresh velocity
    public void DropSuitcase()
    {
        suitcase.transform.position = transform.position;
        suitcase.SetActive(true);
        isCarryingSuitcase = false;
        animator.SetBool("hasSuitcase", isCarryingSuitcase);
        this.UpdateVelocity();
    }

    // Chartactere pick up his SuitCase and refresh velocity
    public void PickupSuitcase()
    {
        suitcase.SetActive(false);
        isCarryingSuitcase = true;
        animator.SetBool("hasSuitcase", isCarryingSuitcase);
        this.UpdateVelocity();
    }

    // Charatere start Runing
    public void StartRunning()
    {
        isRunning = true;
        this.UpdateVelocity();
    }

    // Charatere stop Runing
    public void StopRunning()
    {
        isRunning = false;
        this.UpdateVelocity();
    }

    // Charatere lose a random clothes from his suitcase and refresh his velocity
    public int LoseClothes()
    {
        int clothesWeight = Random.Range(50,200);
        suitcaseWeight -= clothesWeight;
        this.UpdateVelocity();
        return clothesWeight;
    }

    // Charatere pick up a close, add his weight to the suitcase and update his velocity
    public void PickupClothes(int clothesWeight)
    {
        suitcaseWeight += clothesWeight;
        this.UpdateVelocity();
    }

    // This will define the impacts level and the charactere speed
    private void UpdateVelocity()
    {
        if (isCarryingSuitcase)
        {
            speedModificator -= (suitcaseWeight / 1000);
        } else {
            speedModificator += (suitcaseWeight / 1000);
        }
        impactLevelRisk = 1 / (suitcaseWeight / 10);
    }
}
