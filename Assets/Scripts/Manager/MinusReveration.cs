using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusReveration : MonoBehaviour
{
    public Lobby lobby;
    public GameObject purpleOr;
    public GameObject minusdiceBattleButton;

    public GameObject minusDiceEnter;

    public GameObject minusEnterButton;

    StageManager sm;

    public void Start(){
        sm = FindObjectOfType<StageManager>();
        purpleOr.SetActive(sm.minusdice);
        minusdiceBattleButton.SetActive(sm.minusdice);
        lobby.substageload.gameObject.SetActive(!sm.minusdice);
        lobby.mainstageload.gameObject.SetActive(!sm.minusdice);
    }
}
