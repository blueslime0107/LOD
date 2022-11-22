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

    public List<int> diceSelect = new List<int>() {0,0,0};
    public List<int> battleWho = new List<int>() {0,0,0};
    public List<int> battleTarget = new List<int>() {0,0,0,0,0,0};

    public bool did = false; // 연산을 하면 다시 안하기
    public BattleManager bm;
    public BattleFunction bf;
    public string my_team;
    public string my_dicetag;
    
    public string[] battle_type;

    public string battleType;

    public List<Player> my_players = new List<Player>();
    public List<Player> ene_players = new List<Player>();
    public List<Dice> my_dice = new List<Dice>();

    Battlgolithm onDiceSelect;
    Battlgolithm onBattleing;

    public void AIPreSet(){
        my_team = (gameObject.tag.Equals("PlayerTeam1")) ? "Left" : "Right";
        my_dicetag = (gameObject.tag.Equals("PlayerTeam1")) ? "Team1" : "Team2";

        if(gameObject.tag.Equals("PlayerTeam1")){
            my_players = bm.left_players;
            ene_players = bm.right_players;
        }
        else{
            my_players = bm.right_players;
            ene_players = bm.left_players;
            
        }

        my_dice = bm.dices.FindAll(x => x.tag.Equals(my_dicetag));
    }

    public void isDiceSelect(){
        if(!bf.TeamBool2Str().Equals(my_team) || did){return;}
        did = true; // 이 계산을 한번만 하겠다

        List<Dice> dice = bm.dices.FindAll(x => x.tag.Equals(my_dicetag)); // 자신팀 주사위 가져오기
        dice = dice.FindAll(x => x.player.health >0);

        if(diceSelect[0] > 0)
            dice.Sort(SortDice);
        if(diceSelect[0].Equals(2))
            dice.Reverse();

        List<Player> player = my_players.FindAll(x => !x.died);
        player = player.FindAll(x => x.health >0);
        if(diceSelect[1] > 0)
            player.Sort(SortHealth);
        if(diceSelect[1].Equals(2))
            player.Reverse();
        if(diceSelect[1].Equals(0))
            GetShuffleList(player);
        try
        {for(int i=0;i<dice.Count;i++){
            bf.DiceToPlayer(dice[i],player[i]);
        }}
        catch{

        }

    }

    public void isBattleing(){
        if(!bf.TeamBool2Str().Equals(my_team)){return;}


        List<Player> myplayer = my_players.FindAll(x => !x.died && x.dice > 0);
        if(battleWho[0] > 0)
            myplayer.Sort(SortPlayerDice);
        if(battleWho[0].Equals(2))
            myplayer.Reverse();

        if(battleWho[1] > 0)
            myplayer.Sort(SortHealth);
        if(battleWho[1].Equals(2))
            myplayer.Reverse();

        myplayer.Sort(SortPlayerHavDice);

        foreach(Player pla in myplayer){
            Debug.Log(pla.gameObject.name);
        }

        List<Player> eneplayer = ene_players.FindAll(x => !x.died);

        if(myplayer[0].dice.Equals(6)){
            foreach(Player pla in eneplayer){
                if(pla.dice.Equals(1)){
                    pla.dice += 6;
                }
            }
        }

        if(battleTarget[0] > 0)
            eneplayer.Sort(SortPlayerDice);
        if(battleTarget[0].Equals(2))
            eneplayer.Reverse();

        if(battleTarget[1] > 0)
            eneplayer.Sort(SortHealth);
        if(battleTarget[1].Equals(1))
            eneplayer.Reverse();

        if(battleTarget[3].Equals(0))
            eneplayer.Sort(SortPlayerHavDice);

        bf.TargetPlayer(myplayer[0],eneplayer[0]);
        // List<Player> my_players_dice = my_players.FindAll(x => x.dice >0);
        // List<Player> ene_players_dice = ene_players.FindAll(x => x.dice >0);
        // if(my_players_dice.Count <= 0){return;}
        // bf.TargetPlayer(my_players_dice[0],ene_players_dice[0]);
    }

    public void isGettingCard(List<CardAbility> cardlist){
        List<Player> myplayer = my_players.FindAll(x => !x.died);
        bm.GiveCard(cardlist[0],myplayer[0]);
        cardlist.Remove(cardlist[0]);
        foreach(Player player in myplayer){
            for(int i = 0;i<player.cards.Count;i++){
                foreach(CardAbility card in cardlist){
                    player.cards[i].ability.WhenCardDestroy(player.cards[i],card);
                }
            }
        }        

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
        return 0;
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

}
