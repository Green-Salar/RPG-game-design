using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class life2Benem : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;
    private NavMeshAgent agent;
    private Vector3 prevPos;
    private float life;
    public Image lifebar;
    public AudioSource AAY, death;

    void Start()
    {
        
        life = 3;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim != null) { Debug.Log("anim aaaaaa"); } else { Debug.Log("anim namin"); }
        if (life == 0)
        {
            anim.SetBool("die", true);
            death.Play();
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
            lifebar.fillAmount = (float)life / 3;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("gethit", false);
    }
}
