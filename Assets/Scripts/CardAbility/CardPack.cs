using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack
{
    public BattleManager battleManager;
    public Player player;
    public CardAbility ability;
    public bool active; // 카드 능력 발동
    public bool card_battleActive;
    public LineRenderer diceLink;

    public bool blocked;

    public Player saved_player; // 캐릭터 저장
    public List<Player> saved_player_list = new List<Player>(); // 캐릭터 저장 리스트
    public int saved_int; // 변수 저장
    public CardPack saved_card; // 카드저장
    public CardAbility saved_ability;
    public DiceProperty dice;

    public List<CardAbility> card_reg = new List<CardAbility>();
    public List<CardPack> cardpack_reg = new List<CardPack>();
    public List<CardEffect> effect = new List<CardEffect>();

    public int card_id;
    public string name_;
    public string message;
    public string ability_message;
    public bool tained;
    public CardStyle cardStyle;

    public Sprite illust;

    public int count;
    public int sub_count;
    public int price;

}
