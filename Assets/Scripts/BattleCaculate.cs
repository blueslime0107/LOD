using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCaculate : MonoBehaviour
{
    public void BattleMatch(GameObject myCha, GameObject eneCha){
        Dice_Indi myChar = myCha.GetComponent<Dice_Indi>();
        Dice_Indi eneChar = eneCha.GetComponent<Dice_Indi>();
        myChar.setDice(0);
        eneChar.setDice(0);
    }
}
