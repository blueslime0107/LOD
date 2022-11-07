using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBack : MonoBehaviour
{
    [SerializeField] Image blackImg;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem fether;
    public GameObject backObject;
    public GameObject backGround;

    void Start(){
        changeBackGround(backGround);
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

    void changeBackGround(GameObject bg){
        StartCoroutine(changeBg(bg));
    }

    IEnumerator changeBg(GameObject bg){
        FadeIn();
        yield return new WaitForSeconds(2f);
        GameObject obj = Instantiate(bg);
        obj.transform.SetParent(backObject.transform);
        obj.transform.SetAsFirstSibling();
        Destroy(backObject.transform.GetChild(1).gameObject);
        FadeOut();
        yield return null;
    }
}
