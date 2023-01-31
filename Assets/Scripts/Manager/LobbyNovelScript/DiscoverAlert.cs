using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoverAlert : MonoBehaviour
{
    public int floorNum;
    public Lobby lobby;

    GameObject mainStageAlert;
    GameObject mainStory1Alert;
    GameObject mainStory2Alert;
    GameObject subStageAlert;
    GameObject floorUpAlert;
    GameObject floorDownAlert;


    [SerializeField]BattleLoad mainStage1;
    [SerializeField]BattleLoad mainStage2;
    [SerializeField]BattleLoad subStage;


    public void ReloadDiscover(){
        foreach(Stage stage in lobby.stageManager.FloorOfBattle.Stage1){
            if(!stage.discovered){
                mainStory1Alert.SetActive(true);
            }
        }
    }
}
