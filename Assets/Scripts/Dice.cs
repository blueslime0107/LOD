using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int dice_value;

    public bool chartouch;
    GameObject charTarget;

    public Sprite[] dice_img;
    SpriteRenderer render;

    void Awake(){
        render = GetComponent<SpriteRenderer>();
    }

    public void rolldice(){ // 1 부터 6 랜덤 주사위 굴리기
        dice_value = (int)Random.Range(1f,7f);
        render.sprite = dice_img[dice_value-1];
    }

    float distance = 10f;
    void OnMouseDrag() { // 마우스 
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);        
        transform.position = objPosition;
    }

    void OnMouseUp() {
        
        if(chartouch){
            Dice_Indi dice_im = charTarget.GetComponent<Dice_Indi>();
            if(!dice_im.isDiced){
                dice_im.putDice(dice_value);
                gameObject.SetActive(false);
            }
                
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == gameObject.tag){
            charTarget = other.gameObject; // 트리거로 받은 오브젝트는 other.gameObject
            chartouch = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == gameObject.tag)
            chartouch = false;
    }

    public void diceReroll(){
        gameObject.SetActive(true);
    }

    
}
