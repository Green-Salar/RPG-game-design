using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemHHBehaiver : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private NavMeshAgent agent;
    private Transform enemyTransform;
    private GameObject enemyObject;
    private bool attacking;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyObject = GameObject.Find("Player");
    }

    void Update()
    {
        if (enemyObject != null)
        {
            enemyTransform = enemyObject.transform;
             agent.transform.LookAt(enemyTransform.position);
            anim.SetBool("run", (((agent.velocity.magnitude / agent.speed) > 0.5) && agent.remainingDistance > 0.5));
            anim.SetBool("walk", (((agent.velocity.magnitude / agent.speed) < 0.5) && ((agent.velocity.magnitude / agent.speed) > 0.01) ));
            if (enemyObject != null && attacking == true && (enemyTransform.position - agent.transform.position).magnitude < 4)
        {
            anim.SetBool("attacking", true);
            agent.transform.LookAt(enemyTransform.position);
        }
        else
        {
            anim.SetBool("attacking", false);
        }

            Vector3 between = agent.transform.position - enemyTransform.position;
            attacking = true;
            if ((between.magnitude) > 2)
            {

                agent.destination = between.normalized * 2f + enemyTransform.position;

            }
        }
        else
        {
            anim.SetBool("attacking", false);
            agent.destination = agent.transform.position;
        }


    }
    
}
