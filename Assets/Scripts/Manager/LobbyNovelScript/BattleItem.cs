using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleItem : MonoBehaviour
{
    public Lobby lobby;
    public Stage stage;
    public Image rank_img;
    public Sprite[] rank_sprite;
    public TextMeshProUGUI battle_title;

    public void UpdateStat(){
        Debug.Log(stage.rank);
        Debug.Log(rank_sprite);
        rank_img.sprite = rank_sprite[stage.rank-1];
        battle_title.text = stage.title;
    }

    public void Clicked(){
        lobby.stage = stage;
        lobby.stageManager.play_stage = stage;
        lobby.stageManager.player_card = lobby.player;
        lobby.OpenBattleCard();
        
    }
}
