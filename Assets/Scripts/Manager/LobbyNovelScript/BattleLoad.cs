using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BattleLoad : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]bool debug;
    [SerializeField]Lobby lobby;
    StageManager sm;
    [SerializeField]int panel;

    public List<Stage> stages = new List<Stage>();

    public TextMeshProUGUI title;
    public DiceIcon diceIcon;

    private void Awake() {
        sm = FindObjectOfType<StageManager>();
    }

    public void Start(){
        Floor floor = new Floor();
        Debug.Log(lobby.floorNum);
        switch(lobby.floorNum){
            case 1:floor = sm.FloorOfBattle; break;
            case 2:floor = sm.FloorOfResource; break;
            case 3:floor = sm.FloorOfSocial; break;
        }
        switch(panel){
            case 1: stages = floor.Stage1; 
            title.text = floor.title1;
            diceIcon.SetRank(floor.rank1);
            break;     
            case 2: stages = floor.Stage2;
            title.text = floor.title2;
            diceIcon.SetRank(floor.rank2); 
            break;    
            case 3: stages = floor.SubStage; 
            break;    
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        if(Input.GetMouseButtonDown(1)){return;}
        if(panel.Equals(3)){
            lobby.OpenSubMenu();
        }
        else{
            lobby.OpenMainStageMenu();
        }
        sm.floor = lobby.floorNum;
        sm.panel = panel;
        RenderStage();
    }

    public void RenderStage(){
        Debug.Log(stages);
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
