using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceOBJ : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] diceIMG;

    public TextMeshPro diceNumber;

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



}
