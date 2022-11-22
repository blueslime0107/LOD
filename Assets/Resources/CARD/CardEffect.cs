using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public BattleManager battleManager;
    public bool onplayer;
    public bool OnAttack;
    public bool onbattleEnd;
    
    float time;
    public float moveFront=0;
    public float aliveTime;

    public bool fadeOut;
    public float fade_time;

    public bool imLineRenderer;
    SpriteRenderer render;
    LineRenderer lineRender;
    float originWidth;

    void Awake(){
        
        if(imLineRenderer){
            lineRender = GetComponent<LineRenderer>();
            originWidth = lineRender.startWidth;
        }
        render = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        render.color = Color.white;
        if(fadeOut){
            StartCoroutine(FadeOut());
        }
        else{
            StartCoroutine(Normal());
        }
        time = 0f;
        if(onbattleEnd){
            battleManager.on_battle_card_effect.Add(this);
        }
        
    }

    private void OnDisable() {
        StopAllCoroutines();
        time = 0f;
        render.color = Color.white;
    }

    IEnumerator Normal(){
        while(true){
            if(aliveTime > 0f){
            time += Time.deltaTime;
            if(time > aliveTime){
                gameObject.SetActive(false);
            }
            transform.Translate(Vector3.right*moveFront*Time.deltaTime);
            }
            yield return null;
        }
    }

    IEnumerator FadeOut(){
        if(imLineRenderer)
            lineRender.startWidth = originWidth;
        while(true){
            if(time < fade_time){
                render.color = new Color(1,1,1,1f-time/fade_time);
                if(imLineRenderer){
                    lineRender.startWidth -= time/fade_time*0.01f;
                }
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
