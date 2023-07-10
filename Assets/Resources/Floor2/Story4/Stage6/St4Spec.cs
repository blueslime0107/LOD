using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class St4Spec : StageEvent
{
    public override void WhenStageWin(StageManager sm)
    {

        sm.player_cardDic.RemoveAll(x=>x.card_id == 18);

        if(sm.db.LoadFromINTStage(23).victoryed){
            sm.PlayerStages.RemoveAll(x => x.player_Characters_id == 2);
            if(sm.PlayerStages.Find(x => x.player_Characters_id == 8) != null){return;}
            StagePlayerSave st= new StagePlayerSave();
            st.player_Characters_id = 8;
            sm.PlayerStages.Add(st);
            sm.db.UpdatePlayerCard(sm.PlayerStages);
        }

        if(sm.db.LoadFromINTStage(36).victoryed){
            MinusReveration minusReveration = FindObjectOfType<MinusReveration>();
            minusReveration.minusEnterButton.SetActive(true);
        }


        

    }
}
