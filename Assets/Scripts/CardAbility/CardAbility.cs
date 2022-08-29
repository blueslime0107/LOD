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
    [TextArea]
    public string story_message;

    public Sprite illust;
    public GameObject effect;
    public GameObject inst_effect;

    //public GameObject owner;
    // [HideInInspector]public bool card_active;
    // public bool card_triggerd;


    public virtual void CardActivate(BattleManager match){}
    public virtual void DiceApplyed(Player player){}
    public virtual void MatchStarted(Player player, BattleManager match){}
    public virtual void OnBattleWin(BattleCaculate battle){}
    public virtual void OnBattleLose(GameObject player){}
    public virtual void OnDamageing(BattleCaculate battle, Player attacker){}
    public virtual void OnDamaged(BattleCaculate battle, Player defender){}
    public virtual void AttackEffect(Transform transform){}
    public virtual void ImmediEffect(Transform transform){}

    // public virtual void Actived(){
    //     card_active = true;
    //     card_triggerd = true;
    // }

    // public virtual void DeActive(){
    //     card_triggerd = false;
    // }
}
