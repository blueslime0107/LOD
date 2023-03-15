using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardField : MonoBehaviour
{
    public List<Card_text> origincards = new List<Card_text>();

    [SerializeField] float space;
    [SerializeField] float minSpace;
    [SerializeField] float maxSpace;

    void Update()
    {
        List<Card_text> cards = origincards.FindAll(x => x.gameObject.activeSelf);
        space = 0;

        float fullSpace = minSpace * (cards.Count-1) * 0.5f;
        if(fullSpace > maxSpace){space = maxSpace*2/(cards.Count-1);
        fullSpace = space * (cards.Count-1) * 0.5f;}  
        else{space = minSpace;}
        for(int i=0;i<cards.Count;i++){
            if(i == 0){cards[i].rect.anchoredPosition = Vector2.left * fullSpace; 
            if(cards[i].highLighted){cards[i].rect.anchoredPosition += Vector2.up*10;
            }continue;}
            if(cards[i-1].highLighted){cards[i].rect.anchoredPosition = Vector2.right*cards[i-1].rect.anchoredPosition.x + Vector2.right*85; continue;}
            cards[i].rect.anchoredPosition = Vector2.right*cards[i-1].rect.anchoredPosition.x + Vector2.right*space;
            if(cards[i].highLighted){cards[i].rect.anchoredPosition += Vector2.up*10;}
        }
    }
}
