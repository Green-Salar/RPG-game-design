using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class life2Script : MonoBehaviour
{
    private GameObject nextL;
    private GameObject player;
    private Animator anim;
    private CharacterController controller;
    private NavMeshAgent agent;
    private Vector3 prevPos;
    private float life;
    public Image lifebar;
    private Vector3 scaleChange;
    public AudioSource AAY, death, nextLaudio;
    private float a;
    void Start()
    {
        player = GameObject.Find("Player");
        nextL = GameObject.Find("planeC");
        nextL.SetActive(false);
        life = 5;
        a = 0;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        scaleChange = new Vector3(1f, 1f, 1f);
    }

    void Update()
    {

        if (life == 0)
        {
            if (a == 0)
            {
                player.transform.localScale += scaleChange;
                player.GetComponent<NavMeshAgent>().speed += 4;
                a = 1;
                death.Play();
                nextLaudio.Play();

            }
            anim.SetBool("die", true);
            nextL.SetActive(true);
            Destroy(gameObject, 1.5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
      
        if (other.transform.name == "wood")
        {
            AAY.Play();
            anim.SetBool("gethit", true);
            life = life - 1;
            lifebar.fillAmount = (float)life / 5;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("gethit", false);
    }
}
