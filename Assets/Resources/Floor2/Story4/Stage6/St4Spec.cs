using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class St4Spec : StageEvent
{

    public Character char_var;


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
    }
}
