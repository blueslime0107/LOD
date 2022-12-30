using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hp_Indi : MonoBehaviour
{
    public Hp[] hp_list;
    public GameObject HPObj;
    public GameObject BreakObj;
    public GameObject BreakEfk;
    public TextMeshProUGUI HPText; 
    public TextMeshProUGUI BreakText; 

    public void HpUpdate(Player player){
        if(player.health <= 0){
            player.health = 0;
            return;
            }
        HPText.text = player.health.ToString();
        BreakText.text = (player.breakCount.Count <= 0) ? "-" : player.breakCount[0].ToString();
        int max_hp = player.max_health;
        for(int i = 0; i<10; i++){
            if(max_hp<i+1)
                hp_list[i].changeCondi(0);
            if(player.health<=i)
                hp_list[i].changeCondi(1);
            if(player.health>i)
                hp_list[i].changeCondi(2);
            if(player.health>i+10)
                hp_list[i].changeCondi(3);
            }
    }

    private void OnEnable() {
        BreakEfk.SetActive(false);
    }

    public void ActiveEff(){
        BreakEfk.SetActive(false);
        BreakEfk.SetActive(true);

    }
}

