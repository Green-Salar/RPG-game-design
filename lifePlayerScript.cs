using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class lifePlayerScript : MonoBehaviour
{
    private Animator anim;
    private CharacterController controller;
    private float life;
    public Image lifebar;
    public AudioSource AAY,knife,bil;
   public AudioSource death;
    void Start()
    {
      
        life =150;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (life == 0)
        {
            anim.SetBool("die", true);
            Destroy(gameObject, 1.5f);
            Invoke("QuitGame", 3.0f);
            death.Play();
        }
    }
    void QuitGame() { Application.Quit(); Debug.Log("SSSSSSSS"); }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "woodE")
        {
            AAY.Play();
            anim.SetBool("gethit", true);
            life = life - 1;
            lifebar.fillAmount = (float)life / 150;
        }
        if (other.transform.name == "sword")
        {
            anim.SetBool("gethit", true);
            life = life - 2;
            lifebar.fillAmount = (float)life / 150;
        }
        if (other.transform.name == "bil")
        {
            bil.Play();
            anim.SetBool("gethit", true);
            life = life - 3;
            if (life < 0) life = 0;
            lifebar.fillAmount = (float)life / 150;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("gethit", false);
    }
}
