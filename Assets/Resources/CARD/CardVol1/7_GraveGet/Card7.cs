using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/7도굴꾼 절친", order = 7)]
public class Card7 : CardAbility
{

    public override void OnDeath(CardPack card, Player dead_player, BattleManager match)
    {
        Debug.Log("died");
        if(dead_player.tag != card.player.tag){
            match.AddCardPoint(card.player.tag);
        }
        else{
            card.active = true;
        }
        
    }

    public override void StartMatch(CardPack card, BattleManager match)
    {
        if(!card.active){return;}
        card.count = (int)Random.Range(2f,6f);
        
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        if(!card.active || card.count.Equals(0)){return;}
        int dice;
        dice = card.player.dice;
        card.player.SetDice(card.count);
        card.count = dice;
        
    }

    public override void ClashEnded(CardPack card)
    {
        if(!card.active || card.count.Equals(0)){return;}
        int dice = card.player.dice;
        card.player.SetDice(card.count);
        card.count = dice;
    }
}
