using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CardExplain : MonoBehaviour
{
    public TextMeshProUGUI cardAbText;
    public Image cardImg; 

    public void updateCard(CardAbility card){
        cardAbText.text = card.ability_message;
        cardImg.sprite = card.illust;
    }
}
