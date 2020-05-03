using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour
{
    [SerializeField]public Cloth[] clothTab;
    private int[] clothWeightTab = {50,100,150,200};

    public bool   isCarryingSuitcase;
    public bool   isRunning;
    public int    suitcaseWeight;
    public float  characterSpeed = 8f;
    public int    speedModificator = 1;
    [SerializeField]
    public float runningMultiplicator = 1.6f;
    [SerializeField]
    public float unstableTime = 1.5f;
    [SerializeField]
    public float fallenTime = 0.8f;
    [SerializeField]
    private GameObject suitcase;

    private bool isOnTheGround = false;
    private bool isUnstable = false;
    private bool cannotFall = false;
    private float steadiness;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        steadiness = 0.1f;
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
        /*if (Input.GetButtonDown("DropSuitcase") && isCarryingSuitcase)
        {
            this.DropSuitcase();
        }
        if (Input.GetButtonDown("Interact") && !isCarryingSuitcase)
        {
            this.PickupSuitcase();
        }*/

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Add force if is carrying the suitcase
        /*
        if (isCarryingSuitcase)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                rb.AddForce(-transform.up * speedModificator);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                rb.AddForce(transform.up * speedModificator);
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                rb.AddForce(-transform.right * speedModificator);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                rb.AddForce(transform.right * speedModificator);
            }
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
            wantedVelocity.x = (wantedVelocity.x != 0 ? wantedVelocity.x : Random.Range(-25f, 25f)) * Random.Range(0.5f, 1.5f);
            wantedVelocity.y = (wantedVelocity.y != 0 ? wantedVelocity.y : Random.Range(-25f, 25f)) * Random.Range(0.5f, 1.5f);
        }

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
        if (isUnstable && !cannotFall) {
            Fall();
        }
        else if (isRunning) {
            BecomeUnstable(unstableTime, 0.5f);
        }
    }

    public void HitKid() {
        if (isRunning) {
            LoseClothes();
            BecomeUnstable(0.25f, 0.25f);
        }
    }

    public void HitBall() {
        Fall();
    }

    public void BecomeUnstable(float tUnstable, float tInvulnerability) {
        isUnstable = true;
        cannotFall = true;
        animator.SetBool("isUnstable", isUnstable);
        Invoke("BecomeStable", tUnstable);
        Invoke("EndInvulnerability", tInvulnerability);
    }

    private void BecomeStable() {
        isUnstable = false;
        animator.SetBool("isUnstable", isUnstable);
    }

    private void EndInvulnerability() {
        cannotFall = false;
    }

    private void Fall() {
        isOnTheGround = true;
        isUnstable = false;
        animator.SetBool("isUnstable", isUnstable);
        animator.SetBool("isFallen", isOnTheGround);
        LooseSuitcase();
        LoseClothes();
        Invoke("GetUp", fallenTime);
    }

    private void GetUp() {
        isOnTheGround = false;
        suitcase.SetActive(false);
        animator.SetBool("isFallen", isOnTheGround);
    }

    private void LooseSuitcase() {
        suitcase.SetActive(true);
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

    // Charactere pick up his SuitCase and refresh velocity
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
    public void LoseClothes()
    {
        Cloth lostCloth = this.GenerateRandomCloth();
        suitcaseWeight -= lostCloth.GetWeight();
        this.UpdateVelocity();
    }

    // Charatere pick up a close, add his weight to the suitcase and update his velocity
    public void PickupClothes(Cloth cloth)
    {
        cloth.gameObject.SetActive(false);
        suitcaseWeight += cloth.GetWeight();
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
    }

    //Generate a cloth and his throw destination and return it
    private Cloth GenerateRandomCloth()
    {
        int clothType = Random.Range(0, clothTab.Length);
        Vector3 clothThrow = new Vector3(transform.position.x + Random.Range(2,4), transform.position.y + Random.Range(2,4), 0);
        Cloth generatedCloth = Instantiate(clothTab[clothType], transform.position, Quaternion.identity).GetComponent<Cloth>();
        generatedCloth.SetWeight(this.clothWeightTab[clothType]);
        generatedCloth.SetNewPosition(clothThrow);
        return generatedCloth;
    }
}
