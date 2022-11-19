using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hp_Indi : MonoBehaviour
{
    public Hp[] hp_list;
    public GameObject HPObj;
    public GameObject BreakObj;
    public TextMeshProUGUI HPText; 
    public TextMeshProUGUI BreakText; 

    public void HpUpdate(Player player){
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
}

