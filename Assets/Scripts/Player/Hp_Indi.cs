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
        HPText.text = player.health.ToString();
        BreakText.text = (player.breakCount.Count <= 0) ? "-" : player.breakCount[0].ToString();
        for(int i = 0; i<10; i++){
            
            if(player.health<=i)
                hp_list[i].changeCondi(1);
            if(player.health>i)
                hp_list[i].changeCondi(2);
            if(player.health>i+10)
                hp_list[i].changeCondi(3);
            if(player.health>i+20)
                hp_list[i].changeCondi(4);
            if(player.health>i+30)
                hp_list[i].changeCondi(5);
            if(player.health>i+40)
                hp_list[i].changeCondi(6);
            if(player.health>i+50)
                hp_list[i].changeCondi(7);
            if(player.health>i+60)
                hp_list[i].changeCondi(8);
            if(player.health>i+70)
                hp_list[i].changeCondi(9);
            if(player.health>i+80)
                hp_list[i].changeCondi(10);
            if(player.health>i+90)
                hp_list[i].changeCondi(11);
            if(player.max_health<=i)
                hp_list[i].changeCondi(0);}
    }

    private void OnEnable() {
        BreakEfk.SetActive(false);
    }

    public void ActiveEff(){
        BreakEfk.SetActive(false);
        BreakEfk.SetActive(true);

    }
}

