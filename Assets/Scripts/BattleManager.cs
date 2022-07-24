using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameManager gameManager;
    public List<Dice> dices = new List<Dice>();

    public void BattleStart(){
        StartCoroutine("BattleBegin");
    }

    IEnumerator BattleBegin() {                
        yield return new WaitForSeconds(1f);
        DiceRoll();
    }

    void DiceRoll(){
        for(int i = 0; i< dices.Count; i++)
            dices[i].rolldice();
    }
}
