using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 쉽게 현재 위치와 바뀔위치로 속도에맞게 움직일 수 있음
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
        if(!gameObject.activeSelf){return;}
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
                rect.anchoredPosition = move_pos;
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
                rect.anchoredPosition = origin_pos;
                break;
            }
            yield return null;
        }
        yield return null;
        
    }

    public void MoveToMove(){
        if(gameObject.activeSelf){
            StartCoroutine("MoveMove");
        }
        else{
            rect.anchoredPosition = move_pos;
        }
    }

    public void MoveToOrigin(){
        if(gameObject.activeSelf){
            StartCoroutine("MoveOrigin");
        }
        else{
            rect.anchoredPosition = origin_pos;
        }
    }
}
