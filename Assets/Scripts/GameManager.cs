using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;

    void Start(){
        battleManager.Battle();
    }
}
