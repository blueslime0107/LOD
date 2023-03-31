using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 숫자만 넣어도 스프라이트가 바뀌는 아이콘
public class DiceIcon : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite[] rank_sprite;
    public TextMeshProUGUI text;
    public int rank;
    
    public void SetRank(int i){
        rank = i;
        spriteRenderer.sprite = rank_sprite[i];

    }

    public void updateDice(int value){
        if(value < rank_sprite.Length-1){
            spriteRenderer.sprite = rank_sprite[value];
            text.text = "";

        }
        else{
            spriteRenderer.sprite = rank_sprite[rank_sprite.Length-1];
            text.text = value.ToString();
        }
    }
}
