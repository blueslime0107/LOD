using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Achive1 : AchieveMent
{
    public override void OnCardActive()
    {
        switch(questNum){
            case 1:
                if(Condi(62,"Broken")){
                    count++;
                } break;
        }
    }

    public override void OnBattleFoward()
    {
        switch(questNum){
            case 2:
                if(tag.Equals("Win")){
                    addcount();
                } break;
        }
    }
}
