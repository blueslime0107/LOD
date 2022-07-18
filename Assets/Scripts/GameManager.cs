using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] chars;
    public Dice_Indi[] chars_dice;
    public GameObject[] dices;
    // Start is called before the first frame update

    public void SetCharDice(int d, string i){
        int ch = int.Parse(i);
        Char player = chars[ch-1].GetComponent<Char>();
        player.dice = d;
        
    }
}
