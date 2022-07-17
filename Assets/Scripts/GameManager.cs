using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject[] chars;
    public GameObject[] dices;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCharDice(int d, string i){
        int ch = int.Parse(i);
        Debug.Log(chars[0]);
        Char player = chars[ch - 1].GetComponent<Char>();
        Debug.Log(player.dice);
        player.dice = d;
    }
}
