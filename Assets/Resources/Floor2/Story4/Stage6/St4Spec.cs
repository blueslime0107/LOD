using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class St4Spec : StageEvent
{

    public Character char_var;
    public List<Character> char_list = new List<Character>();


    public override void WhenStageWin(StageManager sm)
    {
        bool trigger = false;
        for(int i=0;i<sm.PlayerStages[1].player_Characters.Length;i++){
            if(trigger){
                sm.PlayerStages[1].player_Characters[i-1] = sm.PlayerStages[1].player_Characters[i];
                sm.PlayerStages[1].player_Characters[i] = null;
            }
            if(sm.PlayerStages[1].player_Characters[i] == char_var){
                sm.PlayerStages[1].player_Characters[i] = null;
                trigger = true;
            }
        }
        if(trigger){
            sm.AddPlayerCardChar(char_list);
        }

    }
}
