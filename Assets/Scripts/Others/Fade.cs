using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{

    [SerializeField] Image blackImg;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem fether;

    void Start(){
        FadeOut();
    }

    public void FadeIn(){
        animator.SetBool("FadeIn",true);
        gameObject.SetActive(true);
        fether.Play();
    }

    public void FadeOut(){
        animator.SetBool("FadeIn",false);
        gameObject.SetActive(true);
        fether.Play();
    }
}
