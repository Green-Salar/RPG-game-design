using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerScript : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform enemyTransform;
    private GameObject enemyObject;
    private bool attacking;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        enemyObject = GameObject.Find("Player");
    }

    void Update()
    {
        if (enemyObject != null)
        {
            enemyTransform = enemyObject.transform;
            agent.transform.LookAt(enemyTransform.position);
            anim.SetBool("run", (((agent.velocity.magnitude / agent.speed) > 0.001) && agent.remainingDistance > 0.01));

            if (enemyObject != null && attacking == true && (enemyTransform.position - agent.transform.position).magnitude < 6)
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
            if ((between.magnitude) > 3)
            {

                agent.destination = between.normalized * 3f + enemyTransform.position;

            }
        }
        else
        {
            anim.SetBool("attacking", false);
            agent.destination = agent.transform.position;
        }


    }

}
