using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharItem : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI breaks;

    public int health_value;
    public List<int> breaks_value = new List<int>();

    public TextMeshProUGUI char_name;
    public Image character; 

    public CardUI[] cards = new CardUI[7];

    public void UpdateStat(Character chars){
        health_value = chars.health;
        breaks_value = chars.breaks;
        char_name.text = chars.char_sprites.name_;
        character.sprite = chars.char_sprites.poses[0];

        health.text = health_value.ToString();
        breaks.text = "";
        foreach(int breaked in breaks_value){
            breaks.text += breaked.ToString();
            breaks.text += " ";
        }

        for(int i=0;i<chars.char_preCards.Length;i++){
            if(chars.char_preCards[i] != null){
                cards[i].gameObject.SetActive(true);
                cards[i].card = chars.char_preCards[i];
                cards[i].CardUpdate();
            }
            else{
                cards[i].gameObject.SetActive(false);
            }
        }

    }
}
