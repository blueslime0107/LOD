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
    public List<int> cards = new List<int>();
    public Sprite[] poses;
    SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        card_geted = true;
    }

    // Update is called once per frame
    void Update()
    {
        //render.sprite = poses[condition];
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
        cards.Add(value);
    }

    public void YouAreDead(){
        died = true;
        gameObject.SetActive(false);
    }

    public void ChangeCondition(int num){
        render.sprite = poses[num];
    }

}
