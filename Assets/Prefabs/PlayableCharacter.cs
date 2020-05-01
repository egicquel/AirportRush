using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayableCharacter : MonoBehaviour
{
    public bool isCarryingSuitcase;
    public int  suitcaseWeight;
    public float  characterSpeed = 7f;
    public int  velocity;
    public int  impactLevelRisk;
    public int  losingClothesRisk;

    private Vector3 movement = Vector3.zero;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        isCarryingSuitcase = true;
        suitcaseWeight = 1000;
        this.UpdateVelocity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            rb.AddForce(transform.up * characterSpeed);
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            rb.AddForce(-transform.up * characterSpeed);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(transform.right * characterSpeed);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(-transform.right * characterSpeed);
        }
        // TODO A changer averc le check vélocity quand il sera pret
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
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

    // This will define the lose % rate of clothes, the impacts level and the charactere speed
    private void UpdateVelocity()
    {

    }
}
