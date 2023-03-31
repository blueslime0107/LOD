using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class St4Spec : StageEvent
{
    public override void WhenStageWin(StageManager sm)
    {
        if(sm.db.LoadFromINTStage(23).victoryed){
            sm.PlayerStages.RemoveAll(x => x.player_Characters_id == 2);
        }
    }
}
