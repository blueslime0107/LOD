using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackColorEff : MonoBehaviour
{

    [SerializeField]SpriteRenderer spriteRenderer;
    float timeLeft;
    [SerializeField]Color32 originColor;
    Color32 targetColor;


    public void changeColor(byte r,byte g,byte b, byte a){
        targetColor = new Color32(r,g,b,a);
        timeLeft = 1.0f;
        spriteRenderer.color = targetColor;
        StartCoroutine("StartColorChange");
    }

    IEnumerator StartColorChange(){
        while(timeLeft > 0){
            
            spriteRenderer.color = Color32.Lerp(spriteRenderer.color,originColor,Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
            yield return null;
            
        }

        Debug.Log("didnt");
        spriteRenderer.color = originColor;
        gameObject.SetActive(false);
        yield return null;
    }

    private void OnDisable() {
            spriteRenderer.color = originColor;
    }

}
