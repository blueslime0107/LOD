using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{
    public BattleManager battleManager;
    public BattleCaculate battleCaculate;
    public Sprite[] dice_img;
    public bool isDiced = false;
    public int dice_value;
    Vector3 saved_pos;

    GameObject target;
    SpriteRenderer render;
    public LineRenderer lineRender;
    void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    public void setDice(int value){
        dice_value = value;
        isDiced = true;
        render.sprite = dice_img[value];
    }

    void OnMouseDown() {
        saved_pos = transform.position;
    }


    void OnMouseDrag() { // 마우스 
        if(battleManager.battle_start){            
            lineRender.SetPosition(1, saved_pos);
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f);        
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);  
            transform.position = objPosition;
            objPosition += Vector3.forward;  
            lineRender.SetPosition(0, objPosition);  
        }
          
    }

    void OnMouseUp() {
        transform.position = saved_pos;
        lineRender.SetPosition(1, Vector3.zero);
        lineRender.SetPosition(0, Vector3.zero); 
        battleCaculate.BattleMatch(gameObject,target);      
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(gameObject.tag == "Team1" && collider.gameObject.tag == "Team2"){
            Debug.Log(collider.gameObject);
            target = collider.gameObject;
        }
        if(gameObject.tag == "Team2" && collider.gameObject.tag == "Team1"){
            target = collider.gameObject;
        }
    }
}
