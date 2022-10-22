using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] SceneMove sceneM;
    [SerializeField] GameObject darScreen;
    [SerializeField] MenuItem sub_table;
    [SerializeField] MenuItem enemyCard;
    [SerializeField] MenuItem playerCard;
    [SerializeField] MenuItem BattleButton;

    string curMenu = "lobby";


    public void Update(){
        if(Input.GetMouseButtonDown(1)){
            if(curMenu.Equals("battle")){
                enemyCard.ActiveOpenClose();
                playerCard.ActiveOpenClose();
                BattleButton.ActiveOpenClose();
                curMenu = "surMenu";
                return;
            }
            if(curMenu.Equals("surMenu")){
                sub_table.ActiveOpenClose();
                darScreen.SetActive(false);        
                curMenu = "lobby";
                return;
            }
        }
    }


    public void OpenSubMenu(){
        if(curMenu.Equals("surMenu")){
            return;
        }
        sub_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu = "surMenu";
    }

    public void OpenBattleCard(){
        if(curMenu.Equals("battle")){
            return;
        }
        enemyCard.ActiveOpenClose();
        playerCard.ActiveOpenClose();
        BattleButton.ActiveOpenClose();
        curMenu = "battle";
    }

    public void GetStory(){
        sceneM.MoveStory();
    }

}
