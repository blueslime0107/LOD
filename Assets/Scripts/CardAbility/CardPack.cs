using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public BattleManager battleManager;
    public Player player;
    public CardAbility ability;
    public bool card_active; // 카드 능력 발동
    public bool card_lateActive;
    public LineRenderer diceLink;

    public Player saved_player;
    public List<Player> saved_player_list = new List<Player>();
    public int saved_int;
    public CardPack saved_card;
    public CardAbility saved_ability;
    public Dice dice;

    public List<CardAbility> card_reg = new List<CardAbility>();
    public List<CardPack> cardpack_reg = new List<CardPack>();

    public bool card_activating = false; // 액티브 사용중

    public List<GameObject> effect = new List<GameObject>();

    public int card_id;
    public new string name;
    public string message;
    public string ability_message;
    public Sprite overCard;

    public Sprite illust;

    public int gague;
    public int max_gague;


    

    public void PreSetting(Player play){
        player = play; // 이 카드를 가진 캐릭터의 변수를 가져올 수 있음.
        card_id = ability.card_id; // 카드 ID 다른 카드에서 특정 카드에 대한 이벤트를 위해 필요함
        illust = ability.illust;
        name = ability.name;
        message = ability.message;
        ability_message = ability.ability_message;
        // 이펙트를 현재 플레이어에 복사해둠
        foreach(GameObject effe in ability.effect){
            CardEffect card_effect = effe.GetComponent<CardEffect>();
            card_effect.battleManager = battleManager;
            effe.SetActive(false);
            GameObject card = Instantiate(effe); 
            card.transform.SetParent(player.gameObject.transform, !card_effect.onplayer);
            // 오른쪽 팀이면 좌우반전
            if(player.gameObject.tag == "PlayerTeam2"){ 
                card.transform.eulerAngles += Vector3.up*180f;  
            }
            effect.Add(card);

        }
        // 주사위끼리 선을 잇는게 있으면 카드 넣기
        if(ability.diceLink != null){
            GameObject card = Instantiate(ability.diceLink);
            card.transform.SetParent(player.gameObject.transform, false);
            diceLink = card.GetComponent<LineRenderer>();
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
