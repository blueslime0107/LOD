using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 숫자만 넣어도 스프라이트가 바뀌는 아이콘
public class DiceIcon : MonoBehaviour
{
    public Image spriteRenderer;
    public Sprite[] rank_sprite;
    public int rank;
    
    public void SetRank(int i){
        rank = i;
        spriteRenderer.sprite = rank_sprite[i-1];

    }
}
