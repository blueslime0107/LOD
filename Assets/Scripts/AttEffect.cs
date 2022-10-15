using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttEffect : MonoBehaviour
{
    public Vector3 pre_set;
    public bool player_set;
    public bool farAtk;
    float time;
    public float fade_time;
    SpriteRenderer render;

    void Awake(){
        render = GetComponent<SpriteRenderer>();
    }

    void OnEnable(){
        render.color = Color.white;
        transform.Translate(pre_set);
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
