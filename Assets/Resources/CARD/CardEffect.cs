using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public BattleManager battleManager;
    
    float time;
    public float moveFront=0;
    public float aliveTime;
    public float fade_time;

    public bool imLineRenderer;
    public bool stable;
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
        if(stable){return;}
        if(imLineRenderer){
            StartCoroutine(LineNormal());
        }
        else{
            StartCoroutine(Normal());
        }
        StartCoroutine(Normal());
        time = 0f;
        // if(onbattleEnd){
        //     battleManager.on_battle_card_effect.Add(this);
        // }
        
    }

    private void OnDisable() {
        StopAllCoroutines();
        time = 0f;
        render.color = Color.white;
    }

    IEnumerator Normal(){
        while(true){
            if(time > aliveTime){
            render.color = new Color(1,1,1,1f-(time-aliveTime)/fade_time);
            if(time > aliveTime + fade_time){StopAllCoroutines(); gameObject.SetActive(false);}
            }
            if(moveFront > 0){transform.Translate(Vector3.right*moveFront*Time.deltaTime);}
            time += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LineNormal(){
        lineRender.startWidth = originWidth;
        while(true){
            if(time > aliveTime){
            lineRender.startWidth -= (time-aliveTime)/fade_time*0.01f;
            if(time > aliveTime + fade_time){StopAllCoroutines(); gameObject.SetActive(false);}
            }
            time += Time.deltaTime;
            yield return null;
        }
    }


    

}
