using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleLoad : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]bool isSub;
    [SerializeField]Lobby lobby;
    [SerializeField]StageManager stageManager;
    [SerializeField]string mainStageName;
    public List<Stage> stages = new List<Stage>();

    void Awake(){
        switch(mainStageName){
            case "Floor1SubStage":
                stages = stageManager.Floor1SubStage; break;
            case "Floor1Stage1":
                stages = stageManager.Floor1Stage1; break;
            case "Floor2SubStage":
                stages = stageManager.Floor2SubStage; break;
            case "Floor2Stage1":
                stages = stageManager.Floor2Stage1; break;
            case "Floor3SubStage":
                stages = stageManager.Floor3SubStage; break;
            case "Floor3Stage1":
                stages = stageManager.Floor3Stage1; break;
            case "Floor1Stage2":
                stages = stageManager.Floor1Stage2; break;
            case "Floor2Stage2":
                stages = stageManager.Floor2Stage2; break;
            case "Floor3Stage2":
                stages = stageManager.Floor3Stage2; break;
                
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        if(isSub){
            lobby.OpenSubMenu();
        }
        else{
            lobby.OpenMainStageMenu();
        }
        RenderStage();
    }

    public void RenderStage(){
        Debug.Log(stages);
        Debug.Log(mainStageName);
        foreach(BattleItem item in lobby.stageItem){
            item.gameObject.SetActive(false);
        }
        for(int i=0;i<stages.Count;i++){
            if(lobby.stageItem.Count <= i){
                BattleItem obj = Instantiate(lobby.stage_slot).GetComponent<BattleItem>();
                Debug.Log(obj);
                Debug.Log(lobby.stage_board);
                obj.transform.SetParent(lobby.stage_board.transform,false);   
                obj.lobby = lobby;      
                lobby.stageItem.Add(obj);
            }
            lobby.stageItem[i].gameObject.SetActive(true);
            lobby.stageItem[i].stage = stages[i];
            lobby.stageItem[i].UpdateStat();

        }
    }

}
