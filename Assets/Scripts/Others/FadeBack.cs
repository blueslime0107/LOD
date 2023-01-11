using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBack : MonoBehaviour
{
    [SerializeField] Image blackImg;
    Animator animator;
    [SerializeField] ParticleSystem fether;

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
    }

    public void FadeIn(){ // 장면 들어갈때
    Debug.Log(animator.GetBool("FadeOut"));
    animator.SetBool("FadeOut",false);
        gameObject.SetActive(false);
        
        gameObject.SetActive(true);
        fether.Play();
    }

    public void FadeOut(){ // 장면 나갈때
    Debug.Log(animator.GetBool("FadeOut"));
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        animator.SetBool("FadeOut",true);
        Debug.Log(animator.GetBool("FadeOut"));
        fether.Play();
    }


}
