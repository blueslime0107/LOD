using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    Vector2 origin_pos;
    [SerializeField] Vector2 move_pos;
    [SerializeField] float speed;
    RectTransform rect;
    bool moveStat = false;

    private void Awake() {
        rect = GetComponent<RectTransform>();
        origin_pos = rect.anchoredPosition;    
    }

    public void ActiveOpenClose(){
        moveStat = !moveStat;
        if(moveStat)
            StartCoroutine("MoveMove");
        else
            StartCoroutine("MoveOrigin");
                
    }   

    IEnumerator MoveMove(){
        StopCoroutine("MoveOrigin");
        while(true){
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition,move_pos,speed*Time.deltaTime);
            if(Vector2.Distance(rect.anchoredPosition,move_pos) <= 2){
                break;
            }
            yield return null;
        }
        yield return null;
        
    }

    IEnumerator MoveOrigin(){
        StopCoroutine("MoveMove");
        while(true){
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition,origin_pos,speed*Time.deltaTime);
            if(Vector2.Distance(rect.anchoredPosition,origin_pos) <= 2){
                break;
            }
            yield return null;
        }
        yield return null;
        
    }
}
