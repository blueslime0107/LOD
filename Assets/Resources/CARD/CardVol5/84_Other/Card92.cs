using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Card92 : CardAbility
{
    public override void WhenCardGetImmedi(CardPack card, BattleManager match)
    {
        card.player.AddHealth(20);
        foreach(DiceProperty diceProperty in card.player.dice_Indi.dice_list){
            diceProperty.value = 7;
        }
        card.player.dice_Indi.updateDice();
        card.cardStyle = card.ability.overCard;
        EffectPlayerSet(card.effect[0],card.player,card.player.transform,0,-2);
        card.effect[0].gameObject.SetActive(true);
    }

    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        foreach(DiceProperty diceProperty in card.player.dice_Indi.dice_list){
            diceProperty.value = 7;
        }
        card.player.dice_Indi.updateDice();
    }

    public override void OnClashLose(CardPack card, BattleCaculate battle, Player enemy)
    {
        card.count++;
        if(card.count > 5){
            card.player.dice_Indi.dice_list.Clear();
            card.player.dice_Indi.updateDice();
            battle.bm.GiveCard(linked_card[0],card.player,true);
            battle.bm.GiveCard(linked_card[1],card.player,true);
            card.effect[0].gameObject.SetActive(false);
            battle.bm.DestroyCard(card,card.player);
            
            
        }
    }

    public override void AttackEffect(CardPack card,Player defender)
    {
        defender.battleManager.sdm.Play("CatherPower");
        EffectPlayerSet(card.effect[1],card.player,card.player.transform,0,0,true);
    }
}
