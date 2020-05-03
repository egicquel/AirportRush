using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : Object
{
    private int weight;
    private Vector3 destination;
    private float travelSpeed = 4f;
    private Vector3 smoothedPosition;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination)
        {
            smoothedPosition = Vector3.Lerp(this.transform.position, destination, travelSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

        }
        if (IsInRange())
        {
            if (Input.GetButtonDown("Interact"))
            {
                player.PickupClothes(this);
            }
        }
    }

    public void SetWeight(int w)
    {
        weight = w;
    }

    public void SetNewPosition(Vector3 pos)
    {
        destination = pos;
    }
    public int GetWeight()
    {
        return weight;
    }
}
