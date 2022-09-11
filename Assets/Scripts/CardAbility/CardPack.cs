using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public BattleManager battleManager;
    public Player player;
    public CardAbility ability;
    public bool card_active; // 카드 능력 발동
    public CardPack selected_card;

    public Player saved_player;
    public List<Player> saved_player_list = new List<Player>();
    public int saved_int;
    public CardPack saved_card;
    public CardAbility saved_ability;

    public List<CardAbility> card_reg = new List<CardAbility>();
    public List<CardPack> cardpack_reg = new List<CardPack>();

    public bool card_activating = false; // 액티브 사용중

    public List<GameObject> effect = new List<GameObject>();

    public int card_id;
    public new string name;
    public string message;
    public string ability_message;

    public Sprite illust;

    public int gague;
    public int max_gague;
    

    public void PreSetting(Player play){
        player = play;
        card_id = ability.card_id;
        illust = ability.illust;
        name = ability.name;
        message = ability.message;
        ability_message = ability.ability_message;
        foreach(GameObject effe in ability.effect){
            CardEffect card_effect = effe.GetComponent<CardEffect>();
            card_effect.battleManager = battleManager;
            effe.SetActive(false);
            GameObject card = Instantiate(effe);
            card.transform.SetParent(player.gameObject.transform, !card_effect.onplayer);
            effect.Add(card);
        }
        //gameObject.SetActive(false);
    }



    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
