using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEvent : ScriptableObject, IResetOnExitPlay
{
    public bool triggered = false;

    public StagePlayerSave remove_char;
    public List<StagePlayerSave> add_char;

    public virtual void WhenStageWin(StageManager sm){}

    public void ResetOnExitPlay()
     {
        triggered = false;
     } 
}
