using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card2 : CardAbility
{
    public override void StartMatch(CardPack card, BattleManager match)
    {
        card.active = false;
    }

    public override void CardActivate(CardPack card, BattleManager match)
    {
        match.SelectingPlayer(card);
    }

    public override void PlayerSelected(CardPack card, Player player ,BattleManager match)
    {
        if(player.tag != card.player.tag || card.active){return;}
        player.SetDice(Mathf.Abs(player.dice-7));
        // card.effect[0].transform.localPosition = player.dice_Indi.transform.localPosition;
        // card.effect[0].SetActive(true);
        EffectPlayerSet(card.effect[0],player,player.dice_Indi.transform,0,0);

        card.active = true;
        match.CardLog(card,player);
    }

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        if(card.active){return;}
        PlayerSelected(card,card.player,match);
    }

}
