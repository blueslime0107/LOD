using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lobby : MonoBehaviour
{
    public SoundManager sdm;
    public int floorNum;
    public StageManager stageManager;
    public ProperContainer pc;

    [SerializeField] YouGetACard youGetACard;

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
    [SerializeField]  TextMeshProUGUI BattleButton_pricelimit;
    [SerializeField]  TextMeshProUGUI BattleButton_charlimittext;

    [Tooltip("서브 스테이지 보드")]
    public GameObject stage_board;
    [Tooltip("서브 스테이지 아이템")]
    public GameObject stage_slot;
    [Tooltip("카드 보드")]
    public GameObject card_board;
    public List<BattleItem> stageItem = new List<BattleItem>();
    [Tooltip("도전과제 보드")]
    public TextMeshProUGUI quest_text;

    public List<string> curMenu = new List<string>();

    public GameObject main_alert;
    public GameObject sub_alert;

    public BattleCardManager playerBattleCard;
    public BattleCardManager enemyBattleCard;

    public Stage stage;
    public Stage player;
    public SettingMenu settingMenu;

    void Awake(){
        stageManager = FindObjectOfType<StageManager>();
        pc = FindObjectOfType<ProperContainer>();
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
        if(stageManager.collected_card.Count > 0){
            youGetACard.cardAbility.AddRange(stageManager.collected_card);
            youGetACard.GeTheCard();
            stageManager.AddCardDic(stageManager.collected_card);
            stageManager.collected_card.Clear();
        }
        sdm.Play("Lobby");
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
        if(sub_alert.activeSelf){sub_alert.SetActive(false);}
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
        if(main_alert.activeSelf){main_alert.SetActive(false);}
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

    public int SnumToIndex(int num){
        switch(num){
            case 1: return 1;
            case 2: return 0;
            case 3: return 2;
            default: return 1;
        }
    }

    


}
