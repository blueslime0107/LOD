using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    BattleManager battleManager;
    [SerializeField]
    Dice_Indi dice_Indi;

    public Transform movePoint;
    public int player_id = 0;
    public int condition = 0;
    public int health;
    public bool card_geted = true;
    public int dice;
    public bool died;
    public List<CardAbility> cards = new List<CardAbility>();
    public Sprite[] poses;
    SpriteRenderer render;


    bool isMoving;
    Vector3 moveTarget;
    float moveSpeed;



    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        card_geted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving){
            transform.position = Vector3.MoveTowards(transform.position,moveTarget,moveSpeed * Time.deltaTime);
            //transform.Translate(transform.position*Time.deltaTime);   // //////////002 목표로 이동하는것에는 속도에 델타타임을 곱해야 한다.//
            if(Vector3.Distance(transform.position,moveTarget) < 0.001f){
                isMoving = false;
            }
        }
    }

    public void SetPointMove(Vector3 point, float spd){
        moveTarget = point;
        moveSpeed = spd;
        isMoving = true;
    }

    public void SetDice(int value){
        dice_Indi.setDice(value);
    }

    public void AddDice(int value){
        dice_Indi.setDice(dice + value);
    }

    public void Damage(int value){
        health -= value;
    }

    public void AddCard(int value){
        cards.Add(battleManager.cards[value]);
    }

    public void YouAreDead(){
        died = true;
        gameObject.SetActive(false);
    }

    public void ChangeCondition(int num){
        render.sprite = poses[num];
    }

}
