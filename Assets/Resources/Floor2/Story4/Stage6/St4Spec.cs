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
        for(int i=0;i<sm.FloorOfResource.PlayerStage.characters.Length;i++){
            if(trigger){
                sm.FloorOfResource.PlayerStage.characters[i-1] = sm.FloorOfResource.PlayerStage.characters[i];
                sm.FloorOfResource.PlayerStage.characters[i] = null;
            }
            if(sm.FloorOfResource.PlayerStage.characters[i] == char_var){
                sm.FloorOfResource.PlayerStage.characters[i] = null;
                trigger = true;
            }
        }
        if(trigger){
            sm.AddPlayerCardChar(char_list);
        }

    }
}
