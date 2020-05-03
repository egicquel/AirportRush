using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndReceptionistStuff : MonoBehaviour
{
    [SerializeField]
    private EndReceptionist receptionist1 = default;
    [SerializeField]
    private EndReceptionist receptionist2 = default;
    [SerializeField]
    private EndReceptionist receptionist3 = default;
    [SerializeField]
    private EndReceptionist receptionist4 = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGoodReceptionist(int idReceptionist) {
        switch (idReceptionist) {
            case 0:
                receptionist1.SetEnableIsGood();
                break;
            case 1:
                receptionist2.SetEnableIsGood();
                break;
            case 2:
                receptionist3.SetEnableIsGood();
                break;
            case 3:
                receptionist4.SetEnableIsGood();
                break;
        }
    }
}
