using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public BattleManager battleManager;

    private void OnEnable() {
        battleManager.on_battle_card_effect.Add(this);
    }

    

}
