using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class lifeScript : MonoBehaviour
{
    private GameObject nextL;
    private Animator anim;
    private CharacterController controller;
    private NavMeshAgent agent;
    private Vector3 prevPos;
    private float life,a;

    public Image lifebar;
    public AudioSource AAY,death,nextLaudio;


    void Start()
    {
        nextL = GameObject.Find("planeB");
        nextL.SetActive(false);
        a = 0;
        life = 12;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {

        if (life == 0)
        {  
            anim.SetBool("die", true);
            nextL.SetActive(true);
            Destroy(gameObject, 1.5f);
            death.Play();
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform.name == "wood")
        {
            AAY.Play();
            Debug.Log("hitted");
            anim.SetBool("gethit", true);
            life = life - 1;
            lifebar.fillAmount = (float)life / 12;
            if (life == 0)
            {
                if (a == 0)
                {
                    a = 1;
                    nextLaudio.Play();
                }
                

            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("gethit", false);
    }
}
