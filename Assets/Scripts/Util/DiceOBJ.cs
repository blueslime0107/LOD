using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceOBJ : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] diceIMG;

    public TextMeshPro diceNumber;

    public Dice_Indi player;
    public int diceIndex;

    public void updateDice(int value){
        if(value < diceIMG.Length-1){
            spriteRenderer.sprite = diceIMG[value];
            diceNumber.text = "";

        }
        else{
            spriteRenderer.sprite = diceIMG[diceIMG.Length-1];
            diceNumber.text = value.ToString();
        }
    }

    private void OnMouseDown() {
        if(diceIndex <= 0){return;}
        DiceProperty newDice = new DiceProperty();
        newDice = player.dice_list[0];
        player.dice_list[0] = player.dice_list[diceIndex];
        player.dice_list[diceIndex] = newDice;
        player.updateDice();
    }



}