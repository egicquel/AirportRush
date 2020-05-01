using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableCharacter : MonoBehaviour
{
    public bool   isCarryingSuitcase;
    public bool   isRunning;
    public int    suitcaseWeight;
    public float  characterSpeed = 3f;
    public int    speedModificator = 1;
    public int    impactLevelRisk;

    private Vector2 movement;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        isRunning = false;
        isCarryingSuitcase = true;
        suitcaseWeight = 1000;
        this.UpdateVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Sprint")) {
            this.StartRunning();
        }
        if (Input.GetButtonUp("Sprint")) {
            this.StopRunning();
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0) {
            rb.AddForce(transform.up * speedModificator);
        } else if (Input.GetAxis("Vertical") < 0) {
            rb.AddForce(-transform.up * speedModificator);
        } else if (Input.GetAxis("Horizontal") > 0) {
            rb.AddForce(transform.right * speedModificator);
        } else if (Input.GetAxis("Horizontal") < 0) {
            rb.AddForce(-transform.right * speedModificator);
        }
        if ( isRunning )
        {
            rb.MovePosition(rb.position + movement * characterSpeed * Time.fixedDeltaTime * 1.75f);
        } else {
            rb.MovePosition(rb.position + movement * characterSpeed * Time.fixedDeltaTime);
        }
    }

    // Charactere lost his Suitcase and refresh velocity
    public void DropSuitcase()
    {
        isCarryingSuitcase = false;
        this.UpdateVelocity();
    }

    // Chartactere pick up his SuitCase and refresh velocity
    public void PickupSuitcase()
    {
        isCarryingSuitcase = true;
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
        /*if (isCarryingSuitcase)
        {
            speedModificator *= (suitcaseWeight / 1000);
        } else {
           speedModificator /= (suitcaseWeight / 1000);
        }*/
        impactLevelRisk = 1 / (suitcaseWeight / 10);
    }
}
