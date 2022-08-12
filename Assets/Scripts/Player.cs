using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    Dice_Indi dice_Indi;
    public int health;
    public int dice;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
