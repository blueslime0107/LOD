using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Cards/1완벽할 모범생", order = 1)]
public class Card1 : CardAbility
{

    public override void OnBattleStart(CardPack card, Player player, BattleManager match)
    {
        int count = 0;
        card.diceLink.positionCount = 0;
        for(int i = 0;i<match.players.Count;i++){
            foreach(Player players in match.players){
                if(players.dice.Equals(player.dice+count) && players != player){
                    count += 1;
                    card.diceLink.positionCount += 2;
                    card.diceLink.SetPosition(card.diceLink.positionCount-2,players.dice_Indi.gameObject.transform.position);
                    card.diceLink.SetPosition(card.diceLink.positionCount-1,card.player.dice_Indi.gameObject.transform.position);
                }

            }

                
        }
        player.SetDice(player.dice+count);
        card.diceLink.gameObject.SetActive(true);

    }

}
