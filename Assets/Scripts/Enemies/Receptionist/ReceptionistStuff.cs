using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionistStuff : MonoBehaviour
{
    private Receptionist receptionist;
    private SecurityGuardReceptionist[] guards;
    
    // Start is called before the first frame update
    void Start()
    {
        receptionist = gameObject.GetComponentInChildren<Receptionist>();
        guards = gameObject.GetComponentsInChildren<SecurityGuardReceptionist>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerReceptionistCall() {
        receptionist.CallPlayer();
    }

    public void MoveGuards() {
        foreach (SecurityGuardReceptionist guard in guards) {
            guard.DoMove();
        }
    }

    public void EnterGuardZone() {
        foreach (SecurityGuardReceptionist guard in guards) {
            guard.Deny();
        }
    }

    public void LeaveGuardZone() {
        foreach (SecurityGuardReceptionist guard in guards) {
            guard.StopDeny();
        }
    }
}
