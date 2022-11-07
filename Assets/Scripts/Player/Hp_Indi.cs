using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hp_Indi : MonoBehaviour
{
    public Hp[] hp_list;
    public TextMeshProUGUI HPText; 
    public TextMeshProUGUI BreakText; 

    public void HpUpdate(int hp, int max_hp){
        HPText.text = hp.ToString();
        for(int i = 0; i<10; i++){
            if(max_hp<i+1)
                hp_list[i].changeCondi(0);
            if(hp<=i)
                hp_list[i].changeCondi(1);
            if(hp>i)
                hp_list[i].changeCondi(2);
            if(hp>i+10)
                hp_list[i].changeCondi(3);
            }
    }
}

