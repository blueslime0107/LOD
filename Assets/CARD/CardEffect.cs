using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public BattleManager battleManager;
    public bool onplayer;

    public bool onbattleEnd;

    public float aliveTime;
    float count;

    private void OnEnable() {
        count = 0f;
        if(onbattleEnd){
            battleManager.on_battle_card_effect.Add(this);
        }
        
    }

    void Update(){
        if(aliveTime > 0f){
            count += Time.deltaTime;
            if(count > aliveTime){
                gameObject.SetActive(false);
            }
        }
    }


    

}
