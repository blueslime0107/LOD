using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{

    public StageManager stageManager;

    [SerializeField] SceneMove sceneM;
    [SerializeField] GameObject darScreen;
    [SerializeField] MenuItem battle_table;
    [SerializeField] MenuItem sub_table;
    [SerializeField] MenuItem main_table;





    [SerializeField] MenuItem enemyCard;
    [SerializeField] MenuItem playerCard;
    [SerializeField] MenuItem BattleButton;

    [Tooltip("서브 스테이지 보드")]
    public GameObject stage_board;
    [Tooltip("서브 스테이지 아이템")]
    public GameObject stage_slot;
    public List<BattleItem> stageItem = new List<BattleItem>();

    public string curMenu = "lobby";

    

    public BattleCardManager playerBattleCard;
    public BattleCardManager enemyBattleCard;

    public Stage stage;
    public Stage player;


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
                battle_table.ActiveOpenClose();
                darScreen.SetActive(false);        
                curMenu = "lobby";
                return;
            }
            if(curMenu.Equals("mainMenu")){
                main_table.ActiveOpenClose();
                darScreen.SetActive(false);        
                curMenu = "lobby";
                return;
            }
            if(curMenu.Equals("mainMenuStage")){
                battle_table.ActiveOpenClose();      
                curMenu = "mainMenu";
                return;
            }
        }
    }

    public void OpenSubMenu(){
        if(curMenu.Equals("surMenu")){
            return;
        }
        sub_table.ActiveOpenClose();
        battle_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu = "surMenu";
    }

    public void OpenMainMenu(){
        if(curMenu.Equals("mainMenu")){
            return;
        }
        main_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu = "mainMenu";
    }

    public void OpenMainStageMenu(){
        if(curMenu.Equals("mainMenuStage")){
            return;
        }
        battle_table.ActiveOpenClose();
        curMenu = "mainMenuStage";
    }
    
    


    public void OpenBattleCard(){
        if(curMenu.Equals("battle")){
            return;
        }
        playerBattleCard.UpdateStat();
        enemyBattleCard.UpdateStat();
        enemyCard.ActiveOpenClose();
        playerCard.ActiveOpenClose();
        BattleButton.ActiveOpenClose();
        curMenu = "battle";
    }

    public void GetStory(){
        if(stage.beforeStory != null)
        {sceneM.MoveStory();}
        else{
            sceneM.MoveBattle();
        }
    }

}
