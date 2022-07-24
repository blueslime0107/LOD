using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{

    public Sprite[] dice_img;
    SpriteRenderer render;
    void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    public void diceUpdate(int value){
        render.sprite = dice_img[value];
    }
}
