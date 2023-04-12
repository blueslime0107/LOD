using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Xml;

public class StageManager : MonoBehaviour
{
    public DataBase db;
    public SaveManager saveManager;
    public StageManagerDB stageManagerDB;
    public TextAsset[] textAssets;

    [Space(30f)]

    public Floor floor;

    [HideInInspector]public Stage play_stage; // 플레이 할 스테이지
    [HideInInspector]public Stage player_battleCard; // 플레이어 카드
    public List<CardAbility> player_cardDic;
    public List<CardAbility> collected_card;

    public List<AchieveMent> achiveItms= new List<AchieveMent>();

    public List<StagePlayerSave> PlayerStages = new List<StagePlayerSave>();
    public List<Floor> Floors = new List<Floor>();

    public int preFloor;

    [Space (15f), Header ("Debug")]
    public bool nogiveStage;
    public bool nogiveChar;
    public bool noGotoNewbie;
    public StoryScript playStory;

    public List<StageEvent> stageEvents = new List<StageEvent>();

    private void Awake() {
        var obj = FindObjectsOfType<StageManager>();
        DataBase db = FindObjectOfType<DataBase>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
        
    
        if(noGotoNewbie){return;}
        saveManager.Load();
    }

    public void LoadDataFromDB(){
        db = FindObjectOfType<DataBase>();
        player_cardDic.Clear();

        preFloor = stageManagerDB.curFloor;
        // 카드 도감 상태 불러오기
        foreach(int card in stageManagerDB.player_cardDic){
            player_cardDic.Add(db.LoadFromINTCard(card));
        }

        // 캐릭터 카드소지 불러오기
        PlayerStages.Clear();
        foreach(StagePlayerSave stagePlayer in stageManagerDB.stagePlayerSaves){
            StagePlayerSave stagePlayerSave = new StagePlayerSave();
            stagePlayerSave.player_Characters_id = stagePlayer.player_Characters_id; 
            stagePlayerSave.player_cards = stagePlayer.player_cards;
            // PlayerStages에 값들을 저장
            PlayerStages.Add(stagePlayerSave);
        }

        foreach(Floor floor in Floors){
            floor.Mainstage.Clear();
            floor.SubStage.Clear();
        }

        List<Stage> checkStage = new List<Stage>();
        foreach(StageProperSave sps in stageManagerDB.stageID){
            Stage stageProper = db.LoadFromINTStage(sps.stage);
            stageProper.discovered = sps.discovered;
            stageProper.noPrice = sps.noPrice;
            stageProper.victoryed = sps.victoryed;
            if(stageProper.stageAddress.sub){Floors[stageProper.stageAddress.floor-1].SubStage.Add(stageProper);}
            else{Floors[stageProper.stageAddress.floor-1].Mainstage.Add(stageProper);}
            checkStage.Add(stageProper);
        }
        foreach(Stage stage in checkStage){
            foreach(Stage priceStage in stage.priceStage){
                if(stage.victoryed && !checkStage.Contains(priceStage)){
                    if(priceStage.stageAddress.sub){Floors[priceStage.stageAddress.floor-1].SubStage.Add(priceStage);}
                    else{Floors[priceStage.stageAddress.floor-1].Mainstage.Add(priceStage);}
                }
            }
            foreach(CardAbility priceCard in stage.priceCards){
                if(stage.victoryed && !player_cardDic.Contains(priceCard)){
                    player_cardDic.Add(priceCard);
                }
            }
        }

        
        

        foreach(StageEvent stageEvent in stageEvents){
            stageEvent.WhenStageWin(this);
        }

        


        db.UpdatePlayerCard(PlayerStages);
    }
    public void SavetoDB(){

        stageManagerDB.curFloor = preFloor;

        // 카드 도감 저장
        stageManagerDB.player_cardDic.Clear();
        foreach(CardAbility card in player_cardDic){
            stageManagerDB.player_cardDic.Add(card.card_id);
        }

        // 캐릭터 카드 소지상태 저장
        stageManagerDB.stagePlayerSaves.Clear();
        // PlayerStages 의 값들을 불러와서
        foreach(StagePlayerSave stagePlayer in PlayerStages){
            stagePlayer.player_cards.Clear();
            StagePlayerSave stagePlayerSave = new StagePlayerSave();
            stagePlayerSave.player_Characters_id = stagePlayer.player_Characters_id;
            foreach(CardAbility card in db.unlockable_chars[stagePlayer.player_Characters_id].char_preCards){
                if(card == null){continue;}
                stagePlayerSave.player_cards.Add(card.card_id);
                stagePlayer.player_cards.Add(card.card_id);
            }
            // 데이터베이스에 저장
            stageManagerDB.stagePlayerSaves.Add(stagePlayerSave);
        }
        Debug.Log(PlayerStages.Count);

        stageManagerDB.stageID.Clear();
        foreach(Floor fl in Floors){
            foreach(Stage stage in fl.Mainstage){
                StageProperSave stageProper = new StageProperSave();
                stageProper.stage = stage.id;
                stageProper.discovered = stage.discovered;
                stageProper.noPrice = stage.noPrice;
                stageProper.victoryed = stage.victoryed;
                stageManagerDB.stageID.Add(stageProper);
            }
            foreach(Stage stage in fl.SubStage){
                StageProperSave stageProper = new StageProperSave();
                stageProper.stage = stage.id;
                stageProper.discovered = stage.discovered;
                stageProper.noPrice = stage.noPrice;
                stageProper.victoryed = stage.victoryed;
                stageManagerDB.stageID.Add(stageProper);
            }
        }

        }
    

    public void AddStageFun(List<Stage> stages){

        if(nogiveStage){return;}
        foreach(Stage stage in stages){
            if(stage.stageAddress.sub){Floors[stage.stageAddress.floor-1].SubStage.Add(stage);}
            else{Floors[stage.stageAddress.floor-1].Mainstage.Add(stage);}
        }

        db.UpdatePlayerCard(PlayerStages);
    }

    public void AddPlayerCardChar(StagePlayerSave stagePlayer){

        if(nogiveChar){return;}
        StagePlayerSave st= new StagePlayerSave();
        st.player_Characters_id = stagePlayer.player_Characters_id;
        PlayerStages.Add(st);
        Debug.Log(PlayerStages.Count);
        db.UpdatePlayerCard(PlayerStages);
    }

    public void AddCardDic(List<CardAbility> cards){
        player_cardDic.AddRange(cards);
        saveManager.Save();
    }


}
