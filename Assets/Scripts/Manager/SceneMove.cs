using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public GameObject setting_menu;
    public FadeBack fade_obj;

    // public void Start(){
    //     fade_obj.FadeIn();
    // }

    public void Move(string scene){
        StartCoroutine(MoveScene(scene));
    }

    IEnumerator MoveScene(string scene){
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(scene);
        
    }

}
