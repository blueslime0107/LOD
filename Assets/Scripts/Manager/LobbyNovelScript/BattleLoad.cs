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
    [SerializeField]bool sub; 

    public List<Stage> stages = new List<Stage>();
    [SerializeField] GameObject alertObject;

    public bool newStageDetected;

    private void Awake() {
        sm = FindObjectOfType<StageManager>();
    }

    private void Start() {
        RefreshDiscover();
    }

    public void RefreshDiscover(){
        stages = (sub) ? lobby.curFloor.SubStage : lobby.curFloor.Mainstage;
        newStageDetected = false;

        foreach(Stage stage in stages){
            if(!stage.discovered){
            newStageDetected = true;
            break;
            
            }
        }
        alertObject.SetActive(newStageDetected);
        
    }   

    // 눌렀을때 전투가 로딩되있는 창을 띄운다
    public void OnPointerDown(PointerEventData eventData){
        if(Input.GetMouseButtonDown(1)){return;}
        if(sub){ // 서브스테이지 그룹이라면 바로 띄우기
            lobby.OpenSubMenu();
        }
        else{
            lobby.OpenMainMenu();
        }
        sm.floor = lobby.curFloor;
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
