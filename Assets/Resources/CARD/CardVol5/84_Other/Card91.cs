using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card91 : CardAbility
{
    public override void OnBattleReady(CardPack card, Player player, BattleManager match)
    {
        List<CardPack> cardPacks = new List<CardPack>();
        cardPacks.AddRange(card.player.cards);
        cardPacks.Remove(card);
        cardPacks.RemoveAll(x => x.blocked || x.tained);

        if(cardPacks.Count <= 0){return;}

        CardPack cardPack = cardPacks[Random.Range(0,cardPacks.Count)];
        match.BlockCard(cardPack);
        cardPack.cardStyle = card.ability.overCard;
    }

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        List<CardPack> cardPacks = new List<CardPack>();
        cardPacks.AddRange(card.player.cards);
        cardPacks.Remove(card);
        cardPacks.RemoveAll(x => x.blocked || x.tained);

        if(cardPacks.Count <= 0){card.active = true;}

    }

    
    
}
