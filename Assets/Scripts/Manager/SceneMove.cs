using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{

    public Animator transtion;
    public float transtionTime = 1f;


    // public void Start(){
    //     fade_obj.FadeIn();
    // }

    public void Move(string scene){
        StartCoroutine(MoveScene(scene));
    }

    IEnumerator MoveScene(string scene){
        transtion.SetTrigger("Start");
        yield return new WaitForSeconds(transtionTime);
        SceneManager.LoadScene(scene);
    }

}
