using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : Object
{

    private int weight;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInRange())
        {
            if (Input.GetButtonDown("Interact"))
            {
                player.PickupClothes(weight);
            }
        }
    }

    public void SetWeight(int w)
    {
        weight = w;
    }

    public int GetWeight()
    {
        return weight;
    }
}
