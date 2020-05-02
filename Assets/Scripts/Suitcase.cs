using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suitcase : Object
{
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
                player.PickupSuitcase();
            }
        }
    }
}
