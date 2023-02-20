using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

// 로비에 왔을때 층에 맞는 스테이지를 로딩하는 스크립트
public class BattleLoad : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]bool debug;
    [SerializeField]Lobby lobby;
    [SerializeField]StageManager sm;
    [SerializeField]int panel; 

    public List<Stage> stages = new List<Stage>();

    public TextMeshProUGUI title;
    public DiceIcon diceIcon;
    [SerializeField] GameObject alertObject;

    public bool newStageDetected;

    private void Awake() {
        sm = FindObjectOfType<StageManager>();
    }

    private void Start() {
        BattleLoading();
    }

    public void BattleLoading(){
        Floor floor = new Floor();
        sm = FindObjectOfType<StageManager>();
        switch(lobby.floorNum){
            case 1:floor = sm.FloorOfBattle;break;
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
        RefreshDiscover();
        //floor.addedStage = false;
    }

    public void RefreshDiscover(){
        newStageDetected = false;
        alertObject.SetActive(newStageDetected);
        foreach(Stage stage in stages){
            if(stage.discovered){continue;}
            newStageDetected = true;
            alertObject.SetActive(newStageDetected);
        }
        if(panel == 3){return;}
            lobby.RefreshDiscover();
        
    }

    // 눌렀을때 전투가 로딩되있는 창을 띄운다
    public void OnPointerDown(PointerEventData eventData){
        if(Input.GetMouseButtonDown(1)){return;}
        if(panel.Equals(3)){ // 서브스테이지 그룹이라면 바로 띄우기
            lobby.OpenSubMenu();
        }
        else{
            lobby.OpenMainStageMenu();
        }
        sm.floor = lobby.floorNum;
        sm.panel = panel;
        RenderStage();
    }

    public void RenderStage(){ // 전투로딩 스크립트
        foreach(BattleItem item in lobby.stageItem){
            item.gameObject.SetActive(false);
        }
        for(int i=0;i<stages.Count;i++){
            if(lobby.stageItem.Count <= i){
                BattleItem obj = Instantiate(lobby.stage_slot).GetComponent<BattleItem>();
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
