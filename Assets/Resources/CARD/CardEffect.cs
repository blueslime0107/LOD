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
    public Vector3 movPoint;
    public float movSpeed;

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

    public void BoomPurple(){
        battleManager.backColorEff.changeColor(255,0,255,255);
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
            if(Vector3.Distance(transform.position,movPoint) > 0.01f && movPoint != Vector3.zero){
                transform.position = Vector3.MoveTowards(transform.position,movPoint,movSpeed*Time.deltaTime);
                yield return null;

                continue;
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
