using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public BattleManager battleManager;
    public Player player;
    public CardAbility ability;
    public bool card_active;
    public bool card_enable = true;
    public CardPack selected_card;

    public List<CardAbility> card_reg = new List<CardAbility>();


    public bool card_activating = false;

    public List<GameObject> effect = new List<GameObject>();

    public void PreSetting(Player play){
        player = play;
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
