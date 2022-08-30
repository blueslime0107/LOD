using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPack : MonoBehaviour
{
    public BattleManager battleManager;
    public CardAbility ability;
    public bool card_active;

    public List<GameObject> effect = new List<GameObject>();

    public void PreSetting(){
        foreach(GameObject effe in ability.effect){
            CardEffect card_effect = effe.GetComponent<CardEffect>();
            card_effect.battleManager = battleManager;
            effe.SetActive(false);
            effect.Add(Instantiate(effe));
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
