using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttEffect : MonoBehaviour
{
    float time;
    public float wait_time;
    public float fade_time;
    SpriteRenderer render;

    void Awake(){
        render = GetComponent<SpriteRenderer>();
    }

    void OnEnable(){
        render.color = Color.white;
        StartCoroutine(FadeOut());
    }

    private void OnDisable() {
        StopCoroutine(FadeOut());
        time = 0;
        render.color = Color.white;
    }

    IEnumerator FadeOut(){
        while(true){
            if(time < fade_time){
            render.color = new Color(1,1,1,1f-time/fade_time);
            }
            else{
                time = 0;
                break;
                
            }
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
        yield return null;
    }
}
