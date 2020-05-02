using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] protected PlayableCharacter player;
    [SerializeField] protected float maxDistance;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayableCharacter>();
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    protected bool IsInRange()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= maxDistance)
        {

            if (!animator.GetBool("shining"))
            animator.SetBool("shining", true);
            return true;
        }
        else
        {
            if(animator.GetBool("shining"))
            {
                animator.SetBool("shining", false);
            }
            return false;
        }
    }
}
