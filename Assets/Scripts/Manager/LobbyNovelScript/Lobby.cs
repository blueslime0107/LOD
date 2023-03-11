using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    public SoundManager sdm;
    public Floor curFloor;
    public StageManager stageManager;
    public ProperContainer pc;

    [SerializeField] YouGetACard youGetACard;

    [SerializeField] SceneMove sceneM;
    [SerializeField] GameObject darScreen;
    [SerializeField] MenuItem battle_table;
    [SerializeField] MenuItem tableIMG;
    [SerializeField] MenuItem card_table;

    public  MenuCard menuCard;

    [SerializeField] MenuItem enemyCard;
    [SerializeField] MenuItem playerCard;

    [SerializeField] MenuItem BattleButton;
    [SerializeField]  TextMeshProUGUI BattleButton_pricelimit;
    [SerializeField]  TextMeshProUGUI BattleButton_charlimittext;

    [Tooltip("서브 스테이지 보드")]
    public GameObject stage_board;
    [Tooltip("서브 스테이지 아이템")]
    public GameObject stage_slot;
    [Tooltip("카드 보드")]
    public GameObject card_board;
    [HideInInspector] public BattleItem selectedbattleItem;
    public List<BattleItem> stageItem = new List<BattleItem>();
    [Tooltip("도전과제 보드")]
    public TextMeshProUGUI quest_text;

    public List<string> curMenu = new List<string>();

    public BattleCardManager playerBattleCard;
    public BattleCardManager enemyBattleCard;

    public Stage stage;
    public Stage player;
    public SettingMenu settingMenu;

    [SerializeField]GameObject mainAlertObject;

    [SerializeField]BattleLoad substageload;
    [SerializeField]BattleLoad mainstageload;
   
    public MenuItem elevatorLeft;
    public MenuItem elevatorRight;

    void Awake(){
        stageManager = FindObjectOfType<StageManager>();
        pc = FindObjectOfType<ProperContainer>();
        curMenu.Add("lobby");
    }

    public void Start(){
        ReloadPlayerCard();
        if(stageManager.collected_card.Count > 0){
            youGetACard.cardAbility.AddRange(stageManager.collected_card);
            youGetACard.GeTheCard();
            stageManager.AddCardDic(stageManager.collected_card);
            stageManager.collected_card.Clear();
        }
        sdm.Play("Lobby");
    }
    public void ReloadPlayerCard(){
        player = curFloor.PlayerStage;
    }

    public void Update(){
        if(Input.GetMouseButtonDown(1)){
            switch(curMenu[curMenu.Count-1]){
                case "surMenu":
                sdm.Play("Close");
                elevatorLeft.MoveToOrigin();
                elevatorRight.MoveToOrigin();
                    tableIMG.ActiveOpenClose();
                    battle_table.ActiveOpenClose();
                    darScreen.SetActive(false);        
                    curMenu.Remove("surMenu"); break;
                case "mainMenu":
                sdm.Play("Close");
                elevatorLeft.MoveToOrigin();
                elevatorRight.MoveToOrigin();
                    tableIMG.ActiveOpenClose();
                    darScreen.SetActive(false);       
                    battle_table.ActiveOpenClose(); 
                    curMenu.Remove("mainMenu"); break;
                case "battle":
                sdm.Play("Close");
                    enemyCard.ActiveOpenClose();
                    playerCard.ActiveOpenClose();
                    BattleButton.ActiveOpenClose();
                    curMenu.Remove("battle"); 
                    selectedbattleItem.alert.SetActive(false);
                    substageload.RefreshDiscover();
                    mainstageload.RefreshDiscover();
                    break;
                case "cardMenu":
                sdm.Play("Close");
                elevatorLeft.MoveToOrigin();
                elevatorRight.MoveToOrigin();
                    darScreen.SetActive(false);        
                    card_table.ActiveOpenClose();
                    curMenu.Remove("cardMenu"); break;

                case "cardSelectMenu":
                    sdm.Play("Close");
                    menuCard.playerCardPanel.SetActive(false);
                    menuCard.florrPanel.SetActive(true);
                    elevatorLeft.MoveToMove();
                    elevatorRight.MoveToMove();
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
        elevatorLeft.MoveToMove();
        elevatorRight.MoveToMove();
        sdm.Play("Open");
        tableIMG.ActiveOpenClose();
        battle_table.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu.Add("surMenu");
    }

    public void OpenMainMenu(){
        if(curMenu.Equals("mainMenu")){
            return;
        }
        elevatorLeft.MoveToMove();
        elevatorRight.MoveToMove();
        battle_table.ActiveOpenClose();
        sdm.Play("Open");
        tableIMG.ActiveOpenClose();
        darScreen.SetActive(true);
        curMenu.Add("mainMenu");
    }
    
    public void OpenCardMenu(){
        
        if(curMenu.Equals("cardMenu")){

            return;
        }
        elevatorLeft.MoveToMove();
        elevatorRight.MoveToMove();
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
        elevatorLeft.MoveToOrigin();
        elevatorRight.MoveToOrigin();
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

    public void battleButtonCharLimit(){
        playerBattleCard.updatePriceGague();
        int isBattleAble = 0;
        foreach(Character chars in stageManager.player_battleCard.characters){
            if(chars == null){break;}
            if(chars.battleAble){isBattleAble++;}
            
        }

        BattleButton_charlimittext.text = isBattleAble.ToString() + " / "+stage.charlimit.ToString();

        }

    public void GetStory(){
        sdm.Play("Snap");
        if(pc.debugBoolen){
            sceneM.Move("Battle");
            return;
        }
        if(stageManager.play_stage.beforeStory != null && !stageManager.play_stage.victoryed)
        {sceneM.Move("Talk");}
        else{
            sceneM.Move("Battle");
        }
    }

    public void RefreshDiscover(){
        mainAlertObject.SetActive(mainstageload.newStageDetected);
    }

}
