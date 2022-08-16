using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAbility : ScriptableObject
{
    public int card_id;
    public new string name;
    [TextArea]
    public string message;
    [TextArea]
    public string ability_message;

    public Sprite illust;


    public virtual void CardActivate(BattleManager match){}
    public virtual void DiceApplyed(Player player){}
    public virtual void MatchStarted(Player player, BattleManager match){}
    public virtual void OnBattleWin(BattleCaculate battle){}
    public virtual void OnBattleLose(GameObject player){}
    public virtual void OnDamageing(BattleCaculate battle, Player attacker){}
    public virtual void OnDamaged(BattleCaculate battle, Player defender){}
}
