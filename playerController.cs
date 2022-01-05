using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerController : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private NavMeshAgent agent;
    private float xRotation = 70f;
    private float yRotation = 0f;
    private Vector3 prevPos;
    private Transform enemyTransform;
    private GameObject enemyObject;
    private bool attacking;
    private Vector3 hitPoint;
    private float life = 20;

    void Start()
    {
      
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        prevPos = transform.position;
    }

    void Update()
    {
        Camera.main.transform.position += agent.transform.position - prevPos;
        prevPos = agent.transform.position;
        
        anim.SetBool("run", (((agent.velocity.magnitude / agent.speed) > 0.1) && agent.remainingDistance > 0.5));

        if (enemyObject != null && attacking == true && (enemyTransform.position - agent.transform.position).magnitude < 4)
        {
                anim.SetBool("attacking", true);
                agent.transform.LookAt(enemyTransform.position);
        }
            else
            {
                anim.SetBool("attacking", false);
            }


        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,100.0f))
            {
                hitPoint = hit.point;
                if(hit.transform.tag == "Level")
                {
                    agent.destination = hit.point;
                    attacking = false;
                }
                else if (hit.transform.tag == "enemy")
                {
                    enemyTransform = hit.transform;
                    enemyObject = hit.transform.gameObject;

                    Vector3 between = agent.transform.position - enemyTransform.position;
                    attacking = true;
                    if ((between.magnitude) > 2)
                        {
                        agent.destination = between.normalized * 2f + enemyTransform.position;     
                    }
                }
            }
        }

        //---camera
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * 500 * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 500 * Time.deltaTime;
            xRotation -= mouseY;
            yRotation = mouseX;
            xRotation = Mathf.Clamp(xRotation, 90, 90);

            if (Mathf.Abs(mouseX) > Mathf.Abs(mouseY))
                Camera.main.transform.RotateAround(agent.transform.position, transform.up, yRotation);
            else
                Camera.main.transform.RotateAround(agent.transform.position, Camera.main.transform.right, mouseY);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scroll = -Input.GetAxis("Mouse ScrollWheel");
            Vector3 ray = Camera.main.transform.position - agent.transform.position;
            Camera.main.transform.position += ray * scroll;

        }
        //---- movement for blender
        if ((agent.velocity.magnitude / agent.speed) > 0.1)
        {
            Vector3 normalizedMovement = agent.desiredVelocity.normalized;
            Vector3 forwardVector = Vector3.Project(normalizedMovement, transform.forward);
            Vector3 rightVector = Vector3.Project(normalizedMovement, transform.right);
            float forwardVelocity = forwardVector.magnitude * Vector3.Dot(forwardVector, transform.forward);
            float rightVelocity = rightVector.magnitude * Vector3.Dot(rightVector, transform.right);
            anim.SetFloat("moveX", forwardVelocity);
            anim.SetFloat("moveY", rightVelocity);
        }



    }
}
