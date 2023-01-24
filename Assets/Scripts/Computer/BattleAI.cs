using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleAI : MonoBehaviour
{
    public bool active;

    public List<T> GetShuffleList<T>(List<T> _list){

        for (int i = _list.Count - 1; i > 0; i--)
        {
            int rnd = UnityEngine.Random.Range(0, i);

            T temp = _list[i];
            _list[i] = _list[rnd];
            _list[rnd] = temp;
        }

        return _list;
    }

    [Space (15f), Header ("DiceSelect")]
    public bool ds_upper;
    public bool ds_rev;

    [Space (15f), Header ("DiceWho")]
    public bool dw_health;
    public bool dw_rev;

    [Space (15f), Header ("BattleWho")]
    public bool bw_dice;
    public bool bw_health;
    public bool bw_rev;

    [Space (15f), Header ("BattleWho")]
    public bool bt_dice;
    public bool bt_health;
    public bool bt_rev;
    public bool bt_atkNoDice;
    public bool bt_bigDam;
    public bool bt_kill;

    [Space (15f), Header ("BattleWho_NoDice")]
    public bool btnd_health;
    public bool btnd_rev;
    public bool btnd_bigDam;
    public bool btnd_kill;




    [HideInInspector]public bool did = false; // 연산을 하면 다시 안하기
    Player battleTarget;
    public BattleManager bm;
    public BattleFunction bf;
    string my_team;
    string my_dicetag;

    List<Player> my_players = new List<Player>();
    List<Player> ene_players = new List<Player>();
    List<Dice> my_dice = new List<Dice>();

    Battlgolithm onDiceSelect;
    Battlgolithm onBattleing;

    public void AIPreSet(){
        my_team = (gameObject.tag.Equals("PlayerTeam1")) ? "Left" : "Right";
        my_dicetag = (gameObject.tag.Equals("PlayerTeam1")) ? "Team1" : "Team2";

        if(gameObject.tag.Equals("PlayerTeam1")){
            my_players = bm.left_team.players;
            ene_players = bm.right_team.players;
        }
        else{
            my_players = bm.right_team.players;
            ene_players = bm.left_team.players;
            
        }

        my_dice = bm.dices.FindAll(x => x.tag.Equals(my_dicetag));
    }

    public void isDiceSelect(){
        if(!bf.CurTeamTxt().Equals(my_team) || did){return;}
        did = true; // 이 계산을 한번만 하겠다


        // 어떤 주사위를
        List<Dice> dice = bm.dices.FindAll(x => x.tag.Equals(my_dicetag) && x.player.health >0); // 자신팀 주사위 가져오기
        if(ds_upper)
            dice.Sort(SortDice);
        if(ds_rev)
        dice.Reverse();

        // 어떤 캐릭터에게 줄까?
        List<Player> player = my_players.FindAll(x => !x.died && x.health >0);
        if(dw_health)
            player.Sort(SortHealth);
        if(dw_rev)
            player.Reverse();

        try
        {for(int i=0;i<dice.Count;i++){
            bf.DiceToPlayer(dice[i],player[i]);
        }}
        catch{

        }
        

    }

    public void isBattleing(){
        if(!bf.CurTeamTxt().Equals(my_team)){return;}


        List<Player> myplayer = my_players.FindAll(x => !x.died && x.dice > 0);
        if(bw_dice)
            {myplayer.Sort(SortPlayerDice);}
        if(bw_health)
            {myplayer.Sort(SortHealth);}
        if(bw_rev)
            {myplayer.Reverse();}
            
        List<Player> eneplayer = ene_players.FindAll(x => !x.died);

        battleTarget = myplayer[0];

        if(eneplayer.Find(x => x.dice > 0) == null){
            Debug.Log("they has no dice");
            if(btnd_health)
                eneplayer.Sort(SortHealth);

            if(btnd_bigDam)
                eneplayer.Sort(SortBigDam);

            if(btnd_rev)
                eneplayer.Reverse();

            if(btnd_kill)
                eneplayer.Sort(SortKillChance);
        }   
        else{

        if(bt_dice)
            eneplayer.Sort(SortPlayerDice);

        if(bt_health)
            eneplayer.Sort(SortHealth);

        if(bt_bigDam)
            eneplayer.Sort(SortBigDam);
        

        if(bt_rev)
            eneplayer.Reverse();

        if(!bt_atkNoDice)
            eneplayer.Sort(SortPlayerHavDice);

        if(bt_kill){
            eneplayer.Sort(SortKillChance);
        }


        }

        Player myPlayerOne = myplayer.Find(x => x.dice <= bm.battleCaculate.ones_power);
        Player enemySix = eneplayer.Find(x => x.dice >= 6);
        if(myPlayerOne != null && enemySix != null){
            myplayer[0] = myPlayerOne;
            eneplayer[0] = enemySix;
        }

        bf.TargetPlayer(myplayer[0],eneplayer[0]);
    }

    public void isGettingCard(List<CardAbility> cardlist){
        List<Player> myplayer = my_players.FindAll(x => !x.died);
        bm.GiveCard(cardlist[0],myplayer[0]);
        cardlist.Remove(cardlist[0]);      

    }

///////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////

    private int SortPlayerDice(Player pl1, Player pl2){
        if(pl1.dice < pl2.dice){
            return 1;
        }
        else if(pl1.dice > pl2.dice){
            return -1;
        }
        return 0;
    }
    
    private int SortHealth(Player pl1, Player pl2){
        if(pl1.health < pl2.health){
            return 1;
        }
        else if(pl1.health > pl2.health){
            return -1;
        }
        return 0;
    }

    private int SortPlayerHavDice(Player pl1, Player pl2){
        if(pl1.dice > 0){
            return -1;
        }
        else{
            return 1;
        }
    }

    private int SortDice(Dice pl1, Dice pl2){
        if(pl1.dice_value < pl2.dice_value){
            return 1;
        }
        else if(pl1.dice_value > pl2.dice_value){
            return -1;
        }
        return 0;
    }

    private int SortBigDam(Player pl1, Player pl2){
        if(pl1.dice-battleTarget.dice > pl2.dice - battleTarget.dice){
            return -1;
        }
        else if(pl1.dice-battleTarget.dice < pl2.dice - battleTarget.dice){
            return 1;
        }
        return 0;
    }

    private int SortKillChance(Player pl1, Player pl2){
        if(pl1.health + pl1.dice - battleTarget.dice <= 0){
            return -1;
        }
        else{
            return 1;
        }
    }

}
