using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceIcon : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite[] rank_sprite;
    public int rank;
    
    public void SetRank(int i){
        rank = i;
        spriteRenderer.sprite = rank_sprite[i];

    }
}
