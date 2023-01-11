using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public SoundManager sdm;
    public int floorNum;
    public StageManager stageManager;

    [SerializeField] SceneMove sceneM;
    [SerializeField] GameObject darScreen;
    [SerializeField] MenuItem battle_table;
    [SerializeField] MenuItem sub_table;
    [SerializeField] MenuItem main_table;
    [SerializeField] MenuItem card_table;

    public  MenuCard menuCard;




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
                sdm.Play("Close");
                    sub_table.ActiveOpenClose();
                    battle_table.ActiveOpenClose();
                    darScreen.SetActive(false);        
                    curMenu.Remove("surMenu"); break;
                case "mainMenu":
                sdm.Play("Close");
                    main_table.ActiveOpenClose();
                    darScreen.SetActive(false);        
                    curMenu.Remove("mainMenu"); break;
                case "mainMenuStage":
                sdm.Play("Close");
                    battle_table.ActiveOpenClose();      
                    curMenu.Remove("mainMenuStage"); break;
                case "battle":
                sdm.Play("Close");
                    enemyCard.ActiveOpenClose();
                    playerCard.ActiveOpenClose();
                    BattleButton.ActiveOpenClose();
                    curMenu.Remove("battle"); break;

                case "cardMenu":
                sdm.Play("Close");
                    darScreen.SetActive(false);        
                    card_table.ActiveOpenClose();
                    curMenu.Remove("cardMenu"); break;

                case "cardSelectMenu":
                    sdm.Play("Close");
                    enemyCard.ActiveOpenClose();
                    playerCard.ActiveOpenClose();  
                    BattleButton.ActiveOpenClose();   
                    card_table.ActiveOpenClose();
                    menuCard.cardSelecting = false;
                    playerBattleCard.UpdateStat();
                    enemyBattleCard.UpdateStat();
                    curMenu.Remove("cardSelectMenu"); break;
                    
            }
        }
    }

    public void OpenSubMenu(){
        if(curMenu.Equals("surMenu")){
            return;
        }
        sdm.Play("Open");
        sub_table.ActiveOpenClose();
        battle_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu.Add("surMenu");
    }

    public void OpenMainMenu(){
        if(curMenu.Equals("mainMenu")){
            return;
        }
        sdm.Play("Open");
        main_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu.Add("mainMenu");
    }

    public void OpenMainStageMenu(){
        if(curMenu.Equals("mainMenuStage")){
            return;
        }
        sdm.Play("Open");
        battle_table.ActiveOpenClose();
        curMenu.Add("mainMenuStage");
    }
    
    public void OpenCardMenu(){
        if(curMenu.Equals("cardMenu")){
            return;
        }
        sdm.Play("CardDrawOpen");
        card_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu.Add("cardMenu");
    }

    public void OpenCardSelectMenu(){
        if(curMenu.Equals("cardSelectMenu")){
            return;
        }
        sdm.Play("CardDrawOpen");
        enemyCard.ActiveOpenClose();
        playerCard.ActiveOpenClose();  
        BattleButton.ActiveOpenClose();   
        card_table.ActiveOpenClose();
        curMenu.Add("cardSelectMenu");
        menuCard.cardSelecting = true;
        menuCard.RenderCard();
    }


    public void OpenBattleCard(){
        if(curMenu.Equals("battle")){
            return;
        }
        sdm.Play("Paper3");
        playerBattleCard.UpdateStat();
        enemyBattleCard.UpdateStat();
        enemyCard.ActiveOpenClose();
        playerCard.ActiveOpenClose();
        BattleButton.ActiveOpenClose();
        curMenu.Add("battle");
    }

    public void GetStory(){
        sdm.Play("Snap");
        if(stageManager.play_stage.beforeStory != null)
        {sceneM.Move("Talk");}
        else{
            sceneM.Move("Battle");
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
