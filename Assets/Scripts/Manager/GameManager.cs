using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public SceneMove sceneMove;
    public CameraCtrl main_camera_ctrl;

    public StageManager sm;

    public Player[] leftPlayers;
    public Player[] rightPlayers;

    void Awake(){
        sm = FindObjectOfType<StageManager>();

        if(sm != null)
        {for(int i=0;i<sm.playerStage.characters.Length;i++){
            if(sm.playerStage.characters[i] != null)
            {
                leftPlayers[i].gameObject.SetActive(true);
                Character chars = sm.playerStage.characters[i];
                leftPlayers[i].health = chars.health;
                leftPlayers[i].max_health = chars.health;
                leftPlayers[i].poses = chars.char_sprites;
                leftPlayers[i].UpdateHp();
                foreach(CardAbility ability in chars.char_preCards)
                {
                    leftPlayers[i].pre_cards.Add(ability);
                }
            }
        }

        for(int i=0;i<sm.stage.characters.Length;i++){
            if(sm.stage.characters[i] != null)
            {
                rightPlayers[i].gameObject.SetActive(true);
                Character chars = sm.stage.characters[i];
                rightPlayers[i].health = chars.health;
                rightPlayers[i].max_health = chars.health;
                rightPlayers[i].poses = chars.char_sprites;
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
