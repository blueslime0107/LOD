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
    public GameObject[] effect;
    public CardAbility[] linked_card;

    //public GameObject owner;
    // [HideInInspector]public bool card_active;
    // public bool card_triggerd;
    public void Active(CardPack card){card.card_active = true;}

    public virtual void StartMatch(CardPack card, BattleManager match){}


    public virtual void BeforeCardDraw(CardPack card, BattleManager match, Player player){}
    public virtual void AfterCardDraw(BattleManager match, Player player){}
    public virtual void ImmediCardDraw(CardPack card, BattleManager match, Player player){}
    public virtual void WhenCardDestroy(CardPack card, CardAbility card_abili){}

    public virtual void CardActivate(CardPack card, BattleManager match){}
    public virtual void CardSelected(CardPack card, BattleManager match){}
    public virtual void DiceApplyed(CardPack card, Player player){}
    public virtual void MatchStarted(CardPack card, Player player, BattleManager match){}
    public virtual void OnBattleStart(CardPack card, BattleCaculate battle){}
    public virtual void OnBattleWin(CardPack card, BattleCaculate battle){}
    public virtual void OnBattleLose(CardPack card,GameObject player){}
    // public virtual void OnDamageing(CardPack card,BattleCaculate battle, Player attacker){}
    // public virtual void OnDamaged(CardPack card, BattleCaculate battle, Player defender){}
    public virtual void OnDamage(CardPack card, Player attacker,    BattleManager match, int damage){}
    public virtual void OnDamaging(CardPack card,  Player defender,   BattleManager match, int damage){}
    public virtual void WhoEverDamage(CardPack card, int damage){}

    public virtual void OnDeath(CardPack card, BattleManager match){}
    public virtual void OnDeathOurTeam(CardPack card){}
    public virtual void OnDeathEneTeam(CardPack card){}

    public virtual void BattleEnded(CardPack card){}

    public virtual void ImmediEffect(CardPack card, Transform transform){}
    

    public virtual void AttackEffect(Transform transform){}


    public void EffectPlayerSet(GameObject effect, Player player, Transform tran, float x,float y){
        if(player.gameObject.tag == "PlayerTeam1"){
            effect.transform.eulerAngles = Vector3.up*180;
        }
        else{
            effect.transform.eulerAngles = Vector3.zero;
        }
        effect.transform.position = tran.position + Vector3.right*x*player.TeamVector() + Vector3.up*y;
        //card.effect[0].transform.position = player.transform.position + Vector3.right*player.TeamVector();
    }
    // public virtual void Actived(){
    //     card_active = true;
    //     card_triggerd = true;
    // }

    // public virtual void DeActive(){
    //     card_triggerd = false;
    // }
}
