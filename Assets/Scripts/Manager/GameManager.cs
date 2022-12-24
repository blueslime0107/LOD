using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage{
    public int value;
    public bool nocounter = false;

    public void setDamage(int damage){
        value = damage;
        
    }
}

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public SceneMove sceneMove;
    public CameraCtrl main_camera_ctrl;

    public StageManager sm;

    public Stage debugPlayerStage;
    public Stage debugStage;

    [SerializeField] Player[] leftPlayers;
    [SerializeField] Player[] rightPlayers;

    [SerializeField] GameObject[] backGrounds;
    
    void Reseting(){

        sm = FindObjectOfType<StageManager>();

        if(sm == null){
            GameObject smObj = new GameObject();
            sm = smObj.AddComponent<StageManager>() as StageManager;

            sm.play_stage = debugStage;
            sm.player_battleCard = debugPlayerStage;
            sm.player_cardDic = battleManager.cards;
        }

        

        if(sm != null)
        {
            for(int i=0;i<sm.player_battleCard.characters.Length;i++){
            if(sm.player_battleCard.characters[i] != null)
            {
                leftPlayers[i].battleManager = battleManager;
                leftPlayers[i].dice_Indi.battleManager = battleManager;
                leftPlayers[i].dice_Indi.battleCaculate = battleManager.battleCaculate;
                leftPlayers[i].dice_Indi.lineRender = battleManager.lineRender;
                leftPlayers[i].gameObject.SetActive(true);
                Character chars = sm.player_battleCard.characters[i];
                leftPlayers[i].health = chars.health;
                leftPlayers[i].max_health = chars.health;
                leftPlayers[i].breakCount = new List<int>(chars.breaks);
                leftPlayers[i].poses = chars.char_sprites.poses;
                leftPlayers[i].farAtt = chars.char_sprites.farAtk;
                leftPlayers[i].UpdateHp();
                foreach(CardAbility ability in chars.char_preCards)
                {
                    leftPlayers[i].pre_cards.Add(ability);
                }
            }
        }

        for(int i=0;i<sm.play_stage.characters.Length;i++){
            if(sm.play_stage.characters[i] != null)
            {
                rightPlayers[i].battleManager = battleManager;
                rightPlayers[i].dice_Indi.battleManager = battleManager;
                rightPlayers[i].dice_Indi.battleCaculate = battleManager.battleCaculate;
                rightPlayers[i].dice_Indi.lineRender = battleManager.lineRender;

                rightPlayers[i].gameObject.SetActive(true);
                Character chars = sm.play_stage.characters[i];
                rightPlayers[i].health = chars.health;
                rightPlayers[i].max_health = chars.health;
                rightPlayers[i].breakCount = new List<int>(chars.breaks);
                rightPlayers[i].poses = chars.char_sprites.poses;
                rightPlayers[i].farAtt = chars.char_sprites.farAtk;
                rightPlayers[i].UpdateHp();
                rightPlayers[i].pre_cards.AddRange(chars.char_preCards);
                
            }
        }

        battleManager.cards = sm.player_cardDic;

        if(sm.play_stage.tutorialLine > 0){
            battleManager.tutorial.SetActive(true);
        }
        
        }

        switch(sm.floor){
            case 1: backGrounds[1].SetActive(true); break;
            case 2: backGrounds[0].SetActive(true); break;
            case 3: backGrounds[2].SetActive(true); break;
        }


    }

    void Start(){
        Reseting();
        battleManager.Battle();
    }

    public Vector3 SetVector3z(Vector3 pre_vec,float z){
        Vector3 vec = pre_vec;
        vec.z = z;
        return vec;

    }

    
}
