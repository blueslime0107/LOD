using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEvent : ScriptableObject
{
    public virtual void WhenStageWin(StageManager sm){}
}
