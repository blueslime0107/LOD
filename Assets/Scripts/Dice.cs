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

    void OnMouseDrag() { // 마우스 
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);        
        transform.position = objPosition;
    }

    void OnMouseUp() {
        try{
            if(chartouch){
            Dice_Indi dice_im = charTarget.GetComponent<Dice_Indi>();
            if(!dice_im.isDiced){
                dice_im.putDice(dice_value);
                gameObject.SetActive(false);
            }
                
        }
        }
        catch{

        }
        
        
    }

    // 주사위가 주사위지정 칸에 닿았는가?
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == gameObject.tag){
            charTarget = other.gameObject; // 트리거로 받은 오브젝트는 other.gameObject
            chartouch = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == gameObject.tag)
            chartouch = false;
    }

    // 외부실행 주사위 다시 보이게함
    public void diceReroll(){
        gameObject.SetActive(true);
    }

    
}
