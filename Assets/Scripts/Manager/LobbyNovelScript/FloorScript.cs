using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재의 층에 맞게 배경을 로딩하는 스크립트
public class FloorScript : MonoBehaviour
{
    StageManager sm;
    GameObject curBg;
    [SerializeField] GameObject subPanel;
    [SerializeField] BattleLoad subPanelLoad;
    [SerializeField] GameObject mainPanel;
    [SerializeField] BattleLoad mainPanelLoad;

    [SerializeField] MenuItem elevatorLeftDoor;
    [SerializeField] MenuItem elevatorRightDoor;

    [SerializeField] GameObject[] floorBg; 
    List<GameObject> floorBgObj = new List<GameObject>();
    public Floor curFloor;
    [SerializeField] Lobby lobby;

    [SerializeField] int fl;
    

    // 현재 층의 배경 로딩하기
    void Awake(){
        sm = FindObjectOfType<StageManager>();
        curFloor = sm.Floors[0];
        lobby.curFloor = curFloor;
        foreach(GameObject bg in floorBg){
            GameObject bgobj = Instantiate(bg);
            bgobj.SetActive(false);
            bgobj.transform.SetParent(transform);
            floorBgObj.Add(bgobj);
        }
        curBg = floorBgObj[sm.Floors.IndexOf(curFloor)];
        curBg.SetActive(true);
    }

    public void GoToFloor(int floorNum){
        if(floorNum == sm.Floors.IndexOf(curFloor)){return;}
        int i = (floorNum > sm.Floors.IndexOf(curFloor) ? 1 :-1);
        curFloor = sm.Floors[floorNum];
        StartCoroutine(FloorMove(i));
        
    }

    public void RefreshStageCard(){
        lobby.curFloor = curFloor;
        lobby.ReloadPlayerCard();
        subPanelLoad.BattleLoading();
        Debug.Log("mainpanel");
        mainPanelLoad.BattleLoading();
    }

    // 층 움직이는 모션과 그 층에 맞는 스테이지 로딩
    IEnumerator FloorMove(int dir){
        elevatorLeftDoor.MoveToMove();
        elevatorRightDoor.MoveToMove();
        yield return new WaitForSeconds(0.5f);
        while(Vector3.Distance(transform.position, Vector3.up*fl*dir) > 1){
            transform.position = Vector3.Lerp(transform.position,Vector3.up*fl*dir,Time.deltaTime*15f);
            yield return null;
        }
        transform.position = Vector3.up*fl*dir;

        RefreshStageCard();
        curBg.SetActive(false);
        transform.position = Vector3.down*fl*dir;

        yield return new WaitForSeconds(1f);
        curBg = floorBgObj[sm.Floors.IndexOf(curFloor)];
        curBg.SetActive(true);
        while(Vector3.Distance(transform.position, Vector3.zero) > 1){
            transform.position = Vector3.Lerp(transform.position,Vector3.zero,Time.deltaTime*15f);
            yield return null;
        }
        transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        elevatorLeftDoor.MoveToOrigin();
        elevatorRightDoor.MoveToOrigin();
        yield return null;
    }




}
