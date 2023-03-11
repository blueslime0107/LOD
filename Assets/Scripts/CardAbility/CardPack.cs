using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public BattleManager battleManager;
    public Player player;
    public CardAbility ability;
    public bool active; // 카드 능력 발동
    public bool card_battleActive;
    public LineRenderer diceLink;



    public Player saved_player; // 캐릭터 저장
    public List<Player> saved_player_list = new List<Player>(); // 캐릭터 저장 리스트
    public int saved_int; // 변수 저장
    public CardPack saved_card; // 카드저장
    public CardAbility saved_ability;
    public Dice dice;

    public List<CardAbility> card_reg = new List<CardAbility>();
    public List<CardPack> cardpack_reg = new List<CardPack>();
    public List<CardEffect> effect = new List<CardEffect>();

    public int card_id;
    public new string name;
    public string message;
    public string ability_message;
    public bool tained;
    public Sprite overCard;

    public Sprite illust;

    public int count;
    public int sub_count;
    public int price;

    private void Start() {
        count = ability.pre_count;
        tained = ability.tained;
    }

    

    public void PreSetting(Player play){
        player = play; // 이 카드를 가진 캐릭터의 변수를 가져올 수 있음.
        card_id = ability.card_id; // 카드 ID 다른 카드에서 특정 카드에 대한 이벤트를 위해 필요함
        illust = ability.illust;
        name = ability.name;
        message = ability.message;
        ability_message = ability.ability_message;
        // 이펙트를 현재 플레이어에 복사해둠
        foreach(GameObject effe in ability.effect){
            Vector3 copyVec = effe.transform.position;
            Vector3 copyScale = effe.transform.localScale;
            GameObject card = Instantiate(effe); 
            card.SetActive(false);
            CardEffect card_effect = card.GetComponent<CardEffect>();
            card_effect.battleManager = battleManager;
            card.transform.SetParent(player.gameObject.transform);
            card.transform.localPosition = copyVec;
            card.transform.localScale = copyScale;
            // 오른쪽 팀이면 좌우반전
            if(player.gameObject.tag == "PlayerTeam2"){ 
                card.transform.eulerAngles += Vector3.up*180f;  
                Vector3 pre = card.transform.localPosition;
                pre.x *= -1; 
                card.transform.localPosition = pre;
            }
            effect.Add(card_effect);

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
