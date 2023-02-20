using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage{
    public int value;
    public void setDamage(int damage){
        value = damage; 
    }
    public void adDamage(int damage){
        value += damage; 
        if(value <= 0){value = 0;}
    }
}

public class CardGetSituation{
    public List<CardAbility> specialCards = new List<CardAbility>();
}

[System.Serializable]
public class Team{
    public string text;
    public List<Player> players;
    public int carddraw;
    public BattleAI battleAI;
    public List<CardGetSituation> cardGetSituations = new List<CardGetSituation>();
    public int diceRollGague;

    public int getDiceMaMin(bool max=true){
        int newint = 0;

        foreach(Player player in players){
            if(player.dice < newint && max){continue;}
            if(player.dice > newint && !max){continue;}
            newint = player.dice;
        }
        return newint;
        
    }

    public int getDiceAverage(){
        int newint = 0;
        foreach(Player player in players){
            newint += player.dice;
        }
        return newint;
        
    }

    public int getHealthAver(){
        int newint = 0;

        foreach(Player player in players){
            newint += player.health;
        }


        return newint/players.Count;
    }

    public Player getCardPlayer(bool max=true){
        Player newint = players[0];
        foreach(Player player in players){
            if(player.cards.Count < newint.cards.Count && max){continue;}
            if(player.cards.Count > newint.cards.Count && !max){continue;}
            newint = player;
        }
        return newint;
    }
}

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public BattleManager battleManager;
    public SceneMove sceneMove;
    public CameraCtrl main_camera_ctrl;

    public StageManager sm;
    [SerializeField]public GameObject parent_back;
    

    public Stage debugPlayerStage;
    public Stage debugStage;

    [SerializeField] Player[] leftPlayers;
    [SerializeField] Player[] rightPlayers;

    [SerializeField] GameObject[] backGrounds;

    public Tutorial tutorial;
    
    void Reseting(){

        sm = FindObjectOfType<StageManager>();

        if(sm == null){
            GameObject smObj = new GameObject();
            sm = smObj.AddComponent<StageManager>() as StageManager;

            sm.play_stage = debugStage;
            sm.player_battleCard = debugPlayerStage;
            sm.player_cardDic = battleManager.cards;
        }

        soundManager = FindObjectOfType<SoundManager>();
        battleManager.sdm = soundManager;
        

        if(sm != null)
        {
            int battleable = 0;
            for(int i=0;i<sm.player_battleCard.characters.Length;i++){
            if(sm.player_battleCard.characters[i] == null){continue;}
            if(!sm.player_battleCard.characters[i].battleAble){battleable++; continue;}
            leftPlayers[i-battleable].battleManager = battleManager;
            leftPlayers[i-battleable].dice_Indi.battleManager = battleManager;
            leftPlayers[i-battleable].dice_Indi.battleCaculate = battleManager.battleCaculate;
            leftPlayers[i-battleable].dice_Indi.lineRender = battleManager.lineRender;
            leftPlayers[i-battleable].gameObject.SetActive(true);
            Character chars = sm.player_battleCard.characters[i];
            leftPlayers[i-battleable].character = chars;
            leftPlayers[i-battleable].health = chars.health;
            leftPlayers[i-battleable].max_health = chars.health;
            leftPlayers[i-battleable].breakCount = new List<int>(chars.breaks);
            leftPlayers[i-battleable].poses = chars.char_sprites.poses;
            leftPlayers[i-battleable].farAtt = chars.char_sprites.farAtk;
            leftPlayers[i-battleable].UpdateHp();
            leftPlayers[i-battleable].pre_cards.AddRange(chars.char_preCards);
            leftPlayers[i-battleable].team = battleManager.left_team;
            

        }
        battleable = 0;
        for(int i=0;i<sm.play_stage.characters.Length;i++){
            if(sm.play_stage.characters[i] == null){continue;}
            if(!sm.play_stage.characters[i].battleAble){battleable++; continue;}
                rightPlayers[i-battleable].battleManager = battleManager;
                rightPlayers[i-battleable].dice_Indi.battleManager = battleManager;
                rightPlayers[i-battleable].dice_Indi.battleCaculate = battleManager.battleCaculate;
                rightPlayers[i-battleable].dice_Indi.lineRender = battleManager.lineRender;

                rightPlayers[i-battleable].gameObject.SetActive(true);
                Character chars = sm.play_stage.characters[i];
                rightPlayers[i-battleable].character = chars;
                rightPlayers[i-battleable].health = chars.health;
                rightPlayers[i-battleable].max_health = chars.health;
                rightPlayers[i-battleable].breakCount = new List<int>(chars.breaks);
                rightPlayers[i-battleable].poses = chars.char_sprites.poses;
                rightPlayers[i-battleable].farAtt = chars.char_sprites.farAtk;
                rightPlayers[i-battleable].UpdateHp();
                rightPlayers[i-battleable].pre_cards.AddRange(chars.char_preCards);
                rightPlayers[i-battleable].team = battleManager.right_team;
                
            
        }

        battleManager.cards = sm.player_cardDic;

        if(sm.play_stage.tutorialLine > 0){
            battleManager.tutorial.SetActive(true);
        }
        
        }
        if(sm.play_stage.custom_stage){
            GameObject obj = Instantiate(sm.play_stage.custom_stage);
            obj.transform.SetParent(parent_back.transform,false);   
            soundManager.Play(sm.play_stage.custom_BGM);
        }
        else{
        switch(sm.floor){
            case 1: backGrounds[1].SetActive(true); soundManager.Play("BattleFloor");
            break;
            case 2: backGrounds[0].SetActive(true); soundManager.Play("ResourceFloor");
            break;
            case 3: backGrounds[2].SetActive(true); 
            break;
        }

        }



    }

    void Start(){
        Reseting();
        tutorial.TutorialStart();
        battleManager.Battle();
    }

    public Vector3 SetVector3z(Vector3 pre_vec,float z){
        Vector3 vec = pre_vec;
        vec.z = z;
        return vec;

    }

    
}
