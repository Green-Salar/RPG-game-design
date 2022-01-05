using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// off mesh link animations

public class offers : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        
        if (agent.isOnOffMeshLink)
        {
            anim.SetBool("fly", true); 
        }
        else
        {
            anim.SetBool("fly", false);
        }

    }
}
