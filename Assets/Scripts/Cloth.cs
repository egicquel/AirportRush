using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth : Object
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
        Debug.Log(player);
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

    public int GetWeight()
    {
        return weight;
    }
}
