using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    public BattleManager bm;

    public void WintheGame(){
        foreach(Player player in bm.right_players){
            player.YouAreDead();
        }
        bm.battle_start = true;
        bm.battle_end = true;
    }

    public void UpdateHp(){
        foreach(Player player in bm.players){
            player.UpdateHp();
        }
    }
}