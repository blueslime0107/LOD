using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class CardAbility : ScriptableObject
{
    public int card_id;
    public string[] xmlFile_path = new string[2];

    public new string name;
    [TextArea]
    public string message;
    [TextArea]
    public string ability_message;
    [TextArea]
    public string story_message;

    public bool tained;

    public int price;

    public Sprite illust;

    public bool usingCount;
    public int pre_count;

    [Space(4f),Header("AI abiliby")]

    [Space(15f),Header ("Ability Sprite")]
    public Sprite overCard;
    public GameObject[] effect;
    public CardAbility[] linked_card;
    public GameObject diceLink;
    public Dice dice;

    //public GameObject owner;
    // [HideInInspector]public bool card_active;
    // public bool card_triggerd

    public void Active(CardPack card){
        card.card_battleActive = true;
        }

    public virtual void StartMatch(CardPack card, BattleManager match){}


    public virtual void BeforeCardDraw(CardPack card, BattleManager match, Player player){}
    public virtual void AfterCardDraw(BattleManager match, Player player){}
    public virtual void WhenCardGet(CardPack card, BattleManager match, Player player, CardPack getCard){}
    public virtual void WhenCardGetImmedi(CardPack card, BattleManager match){}
    public virtual void WhenCardDestroy(CardPack card, CardAbility card_abili){}
    public virtual void WhenCardDisabled(CardPack card, BattleManager match){}

    public virtual void CardActivate(CardPack card, BattleManager match){}
    public virtual void CardSelected(CardPack card, CardPack selected_card,BattleManager match){}
    public virtual void PlayerSelected(CardPack card, Player selected_player, BattleManager match){}
    public virtual void DiceApplyed(CardPack card, Player player){}


    public virtual void OnBattleReady(CardPack card, Player player, BattleManager match){}
    public virtual void OnBattleStart(CardPack card, Player player, BattleManager match){}

    public virtual void OnClashStart(CardPack card, BattleCaculate battle,Player enemy){}
    public virtual void OnClashWin(CardPack card, BattleCaculate battle){}
    public virtual void OnClashLose(CardPack card,BattleCaculate battle){}
    public virtual void OnClashDraw(CardPack card,BattleCaculate battle,Player enemy){}
    // public virtual void OnDamageing(CardPack card,BattleCaculate battle, Player attacker){}
    // public virtual void OnDamaged(CardPack card, BattleCaculate battle, Player defender){}
    public virtual void OnDamage(CardPack card, Player attacker, Damage damage, BattleManager match){}
    public virtual void OnDamaging(CardPack card,  Player defender, Damage damage, BattleManager match){}
    public virtual void WhoEverDamage(CardPack card, Damage damage, BattleManager match){}

    public virtual void OnDeath(CardPack card, Player dead_player, BattleManager match){}

    public virtual void ClashEnded(CardPack card){}

    
    

    public virtual void AttackEffect(CardPack card,Player defender){}


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
