using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public SceneMove sceneMove;
    public CameraCtrl main_camera_ctrl;

    public StageManager sm;

    public Stage debugPlayerStage;
    public Stage debugStage;

    public Player[] leftPlayers;
    public Player[] rightPlayers;

    void Awake(){
        sm = FindObjectOfType<StageManager>();

        if(sm == null){
            Debug.Log("not Find");
            GameObject smObj = new GameObject();
            sm = smObj.AddComponent<StageManager>() as StageManager;

            sm.play_stage = debugStage;
            sm.player_card = debugPlayerStage;
        }

        if(sm != null)
        {for(int i=0;i<sm.player_card.characters.Length;i++){
            if(sm.player_card.characters[i] != null)
            {
                leftPlayers[i].gameObject.SetActive(true);
                Character chars = sm.player_card.characters[i];
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
                rightPlayers[i].gameObject.SetActive(true);
                Character chars = sm.play_stage.characters[i];
                rightPlayers[i].health = chars.health;
                rightPlayers[i].max_health = chars.health;
                rightPlayers[i].breakCount = new List<int>(chars.breaks);
                rightPlayers[i].poses = chars.char_sprites.poses;
                rightPlayers[i].farAtt = chars.char_sprites.farAtk;
                rightPlayers[i].UpdateHp();
                foreach(CardAbility ability in chars.char_preCards)
                {
                    rightPlayers[i].pre_cards.Add(ability);
                }
            }
        }}

    }

    void Start(){
        battleManager.Battle();
    }

    public Vector3 SetVector3z(Vector3 pre_vec,float z){
        Vector3 vec = pre_vec;
        vec.z = z;
        return vec;

    }

    
}
