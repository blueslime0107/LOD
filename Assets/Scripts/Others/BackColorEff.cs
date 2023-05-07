using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackColorEff : MonoBehaviour
{
    [SerializeField]BattleManager battleManager;
    [SerializeField]SpriteRenderer spriteRenderer;
    float timeLeft;
    [SerializeField]Color32 originColor;
    [SerializeField]Color32 fadeColor;
    Color32 targetColor;

    public void changeColor(byte r,byte g,byte b, byte a){
        targetColor = new Color32(r,g,b,a);
        timeLeft = 1.0f;
        spriteRenderer.color = targetColor;
        gameObject.SetActive(true);
        StartCoroutine("StartColorChange");
    }

    IEnumerator StartColorChange(){
        while(timeLeft > 0){
            
            spriteRenderer.color = Color32.Lerp(spriteRenderer.color,originColor,Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
            yield return null;
            
        }

        spriteRenderer.color = originColor;
        gameObject.SetActive(false);
        yield return null;
    }

    private void OnDisable() {
        spriteRenderer.color = originColor;
    }

    public void BattleStart(){
        gameObject.SetActive(true);
        StopAllCoroutines();
        spriteRenderer.color = originColor;
    }
    

    public void BattleFin(){
        if(battleManager.battleing){return;}
        StartCoroutine(Fadein());
    }

    IEnumerator Fadein(){
        float newtimeLeft = 1.0f;
        while(newtimeLeft > 0){
            
            spriteRenderer.color = Color32.Lerp(spriteRenderer.color,fadeColor,Time.deltaTime / newtimeLeft);
            newtimeLeft -= Time.deltaTime;
            yield return null;
            
        }
        gameObject.SetActive(false);
        yield return null;
    }

}
