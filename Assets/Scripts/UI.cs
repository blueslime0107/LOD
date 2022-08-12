using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Button battleStartButton;
    public Text[] hpText;

    void Awake(){
        Debug.Log(battleStartButton.enabled);
        
    }

    // Start is called before the first frame update

}
