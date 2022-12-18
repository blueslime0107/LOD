using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public int floorNum;
    public StageManager stageManager;

    [SerializeField] SceneMove sceneM;
    [SerializeField] GameObject darScreen;
    [SerializeField] MenuItem battle_table;
    [SerializeField] MenuItem sub_table;
    [SerializeField] MenuItem main_table;
    [SerializeField] MenuItem card_table;





    [SerializeField] MenuItem enemyCard;
    [SerializeField] MenuItem playerCard;
    [SerializeField] MenuItem BattleButton;

    [Tooltip("서브 스테이지 보드")]
    public GameObject stage_board;
    [Tooltip("서브 스테이지 아이템")]
    public GameObject stage_slot;
    [Tooltip("카드 보드")]
    public GameObject card_board;
    public List<BattleItem> stageItem = new List<BattleItem>();

    public List<string> curMenu = new List<string>();

    

    public BattleCardManager playerBattleCard;
    public BattleCardManager enemyBattleCard;

    public Stage stage;
    public Stage player;

    void Awake(){
        stageManager = FindObjectOfType<StageManager>();
        curMenu.Add("lobby");
    }

    public void Start(){
        switch(floorNum){
            case 1:
                player = stageManager.FloorOfBattle.PlayerStage; break;
            case 2:
                player = stageManager.FloorOfResource.PlayerStage; break;
            case 3:
                player = stageManager.FloorOfSocial.PlayerStage; break;
        }
    }

    public void Update(){
        if(Input.GetMouseButtonDown(1)){
            switch(curMenu[curMenu.Count-1]){
                case "surMenu":
                    sub_table.ActiveOpenClose();
                    battle_table.ActiveOpenClose();
                    darScreen.SetActive(false);        
                    curMenu.Remove("surMenu"); break;
                case "mainMenu":
                    main_table.ActiveOpenClose();
                    darScreen.SetActive(false);        
                    curMenu.Remove("mainMenu"); break;
                case "mainMenuStage":
                    battle_table.ActiveOpenClose();      
                    curMenu.Remove("mainMenuStage"); break;
                case "battle":
                    enemyCard.ActiveOpenClose();
                    playerCard.ActiveOpenClose();
                    BattleButton.ActiveOpenClose();
                    curMenu.Remove("battle"); break;

                case "cardMenu":
                    darScreen.SetActive(false);        
                    card_table.ActiveOpenClose();
                    curMenu.Remove("cardMenu"); break;
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
        curMenu.Add("surMenu");
    }

    public void OpenMainMenu(){
        if(curMenu.Equals("mainMenu")){
            return;
        }
        main_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu.Add("mainMenu");
    }

    public void OpenMainStageMenu(){
        if(curMenu.Equals("mainMenuStage")){
            return;
        }
        battle_table.ActiveOpenClose();
        curMenu.Add("mainMenuStage");
    }
    
    public void OpenCardMenu(){
        if(curMenu.Equals("cardMenu")){
            return;
        }
        card_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu.Add("cardMenu");
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
        curMenu.Add("battle");
    }

    public void GetStory(){
        if(stageManager.play_stage.beforeStory != null)
        {sceneM.MoveStory();}
        else{
            sceneM.MoveBattle();
        }
    }

    public int SnumToIndex(int num){
        switch(num){
            case 1: return 1;
            case 2: return 0;
            case 3: return 2;
            default: return 1;
        }
    }

}
