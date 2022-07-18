using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Indi : MonoBehaviour
{

    public Char player;
    public Sprite[] dice_img;
    SpriteRenderer render;
    void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    void Update(){
        render.sprite = dice_img[player.dice];
    }

}
