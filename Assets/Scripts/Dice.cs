using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int dice_code = 0;
    public int dice_value;

    bool chartouch;
    GameObject charTarget;

    public Sprite[] dice_img;
    SpriteRenderer render;

    void Awake(){
        render = GetComponent<SpriteRenderer>();
    }

    public void rolldice(){
        dice_value = (int)Random.Range(1f,7f);
        render.sprite = dice_img[dice_value-1];
    }

    float distance = 10f;
    void OnMouseDrag() {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);        
        transform.position = objPosition;
    }

    void OnMouseUp() {
        if(chartouch){
            Dice_Indi dice_im = charTarget.GetComponent<Dice_Indi>();
            dice_im.diceUpdate(dice_value);
            gameObject.SetActive(false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Team1")
            charTarget = gameObject;
            chartouch = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Team1")
            chartouch = false;
    }
}
