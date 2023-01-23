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
    StageManager sm;
    [SerializeField]int panel;

    public List<Stage> stages = new List<Stage>();

    public TextMeshProUGUI title;
    public DiceIcon diceIcon;

    [SerializeField] GameObject alert;

    private void Awake() {
        sm = FindObjectOfType<StageManager>();
    }

    public void Start(){
        Floor floor = new Floor();
        switch(lobby.floorNum){
            case 1:floor = sm.FloorOfBattle; break;
            case 2:floor = sm.FloorOfResource; break;
            case 3:floor = sm.FloorOfSocial; break;
        }
        switch(panel){
            case 1: stages = floor.Stage1; 
            alert.SetActive(floor.addedStage1); // 새 스테이지가 왔다면 알리기
            lobby.main_alert.SetActive(true);
            floor.addedStage1 = false;
            title.text = floor.title1;
            diceIcon.SetRank(floor.rank1);
            break;     
            case 2: stages = floor.Stage2;
            alert.SetActive(floor.addedStage2);
            lobby.main_alert.SetActive(true);
            floor.addedStage2 = false;
            title.text = floor.title2;
            diceIcon.SetRank(floor.rank2); 
            break;    
            case 3: stages = floor.SubStage; 
            alert.SetActive(floor.addedSubStage);
            floor.addedSubStage = false;
            break;    
        }
        //floor.addedStage = false;
    }

    // 눌렀을때 전투가 로딩되있는 창을 띄운다
    public void OnPointerDown(PointerEventData eventData){
        if(Input.GetMouseButtonDown(1)){return;}
        alert.SetActive(false);
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
