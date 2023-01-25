using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    public BattleManager bm;

    [SerializeField]float healthVal;
    [SerializeField]float playerVal;
    [SerializeField]float costVal;

    public void WintheGame(){
        foreach(Player player in bm.right_team.players){
            player.AddHealth(-999);
        }
        bm.battle_start = true;
        bm.battle_end = true;
    }

    public void CaculateMisfortune(){
        bm.CardLogText(CaculateMisfortuneLogic(bm.left_team).ToString(),"#0099ff");
        bm.CardLogText("[왼쪽팀 불행지수]","#0099ff");
        bm.CardLogText(CaculateMisfortuneLogic(bm.right_team).ToString(),"#ff2b2b");
        bm.CardLogText("[오른쪽팀 불행지수]","#ff2b2b");
    }

    public void UpdateHp(){
        foreach(Player player in bm.players){
            player.UpdateHp();
        }
    }

    float CaculateMisfortuneLogic(Team team){
        bm.CardLogText("[체력 " + Health(team) +" ]");
        bm.CardLogText("[캐릭 " + Player(team) +" ]");
        bm.CardLogText("[코스트 " + Cost(team) +" ]");

        return Player(team)*100+Health(team)*5+Cost(team)*1.5f;
    }

    int HealthCha(Team team){
        int leftHealth =0;
        foreach(Player player in bm.left_team.players){
            leftHealth += player.health;
        }
        int rightHealth =0;
        foreach(Player player in bm.right_team.players){
            rightHealth += player.health;
        }
        if(team == bm.left_team){
            return rightHealth - leftHealth;
        }else{
            return leftHealth - rightHealth;
        }
    }

    int PlayerCha(Team team){
        if(team == bm.left_team){
            return bm.right_team.players.Count - bm.left_team.players.Count;
        }else{
            return bm.left_team.players.Count - bm.right_team.players.Count;
        }
    }

    int CostCha(Team team){
        int leftCost =0;
        foreach(Player player in bm.left_team.players){
            foreach(CardPack card in player.cards){

            leftCost += card.price;
            }
        }
        int rightCost =0;
        foreach(Player player in bm.right_team.players){
            foreach(CardPack card in player.cards){

            rightCost += card.price;
            }
        }
        if(team == bm.left_team){
            return rightCost - leftCost;
        }else{
            return leftCost - rightCost;
        }
    }

    int Health(Team team){
        int leftHealth =0;
        foreach(Player player in team.players){
            leftHealth += player.health;
        }
        return leftHealth;
    }

    int Player(Team team){
        return team.players.Count;
    }

    int Cost(Team team){
        int leftCost =0;
        foreach(Player player in team.players){
            foreach(CardPack card in player.cards){

            leftCost += card.price;
            }
        }
        return leftCost;
    }





}
