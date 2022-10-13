using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public GameObject cardPrefab;

    public CardDraw[] cards;

    public CardDraw MakeCard(){

        for(int i = 0;i <cards.Length;i++){
            if(!cards[i].gameObject.activeSelf){
                cards[i].gameObject.SetActive(true);
                return cards[i];
            }
         
        }
        return null;
    }

}
