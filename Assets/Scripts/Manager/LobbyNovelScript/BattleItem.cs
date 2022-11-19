using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleItem : MonoBehaviour
{
    public Lobby lobby;
    public Stage stage;
    public DiceIcon diceIcon;
    public TextMeshProUGUI battle_title;
    public GameObject cleared;

    public void UpdateStat(){
        diceIcon.SetRank(stage.rank);
        battle_title.text = stage.title;
        cleared.SetActive(stage.victoryed);
    }

    public void Clicked(){
        lobby.stage = stage;
        lobby.stageManager.play_stage = stage;
        lobby.stageManager.player_card = lobby.player;
        if(stage.playerStageLock != null){
            lobby.stageManager.player_card = stage.playerStageLock;
        }
        lobby.OpenBattleCard();
        
    }
}
