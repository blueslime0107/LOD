using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class CardDatabase : ScriptableObject, ISerializationCallbackReceiver{
    public CardAbility[] cards;
    public Dictionary<CardAbility, int> GetId = new Dictionary<CardAbility, int>();

    public void OnAfterDeserialize(){
        GetId = new Dictionary<CardAbility, int>();
        foreach(CardAbility card in cards){
            GetId.Add(card, card.card_id);
        }
    }

    public void OnBeforeSerialize(){

    }
}