using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재의 층에 맞게 배경을 로딩하는 스크립트
public class FloorScript : MonoBehaviour
{
    GameObject curBg;
    [SerializeField] GameObject subPanel;
    [SerializeField] BattleLoad subPanelLoad;
    [SerializeField] GameObject mainPanel;
    [SerializeField] BattleLoad mainPanelLoad1;
    [SerializeField] BattleLoad mainPanelLoad2;

    [SerializeField] GameObject[] floorBg; 
    List<GameObject> floorBgObj = new List<GameObject>();
    [SerializeField] int[] floorNum;
    public int curFloor;
    [SerializeField] Lobby lobby;
    [SerializeField] int fl;
    

    // 현재 층의 배경 로딩하기
    void Awake(){
        StageManager sm = FindObjectOfType<StageManager>();
        curFloor = lobby.SnumToIndex(sm.floor);
        lobby.floorNum = floorNum[curFloor];
        foreach(GameObject bg in floorBg){
            GameObject bgobj = Instantiate(bg);
            bgobj.SetActive(false);
            bgobj.transform.SetParent(transform);
            floorBgObj.Add(bgobj);
        }
        curBg = floorBgObj[curFloor];
        curBg.SetActive(true);
    }

    public void FloorGoDown(){
        curFloor -= 1;
        if(curFloor < 0){curFloor = 0; return;}
        StartCoroutine(FloorMove(1));
        
    }

    public void FloorGoUp(){
        curFloor += 1;
        if(curFloor >= floorNum.Length){curFloor -= 1; return;}
        StartCoroutine(FloorMove(-1));
        
    }

    // 층 움직이는 모션과 그 층에 맞는 스테이지 로딩
    IEnumerator FloorMove(int dir){
        lobby.upFloor.ActiveOpenClose();
        lobby.downFloor.ActiveOpenClose();
        while(Vector3.Distance(transform.position, Vector3.up*fl*dir) > 1){
            transform.position = Vector3.Lerp(transform.position,Vector3.up*fl*dir,Time.deltaTime*15f);
            yield return null;
        }
        transform.position = Vector3.up*fl*dir;

        lobby.floorNum = floorNum[curFloor];
        subPanelLoad.Start();
        mainPanelLoad1.Start();
        mainPanelLoad2.Start();
        curBg.SetActive(false);
        transform.position = Vector3.down*fl*dir;

        yield return new WaitForSeconds(1f);
        curBg = floorBgObj[curFloor];
        curBg.SetActive(true);
        while(Vector3.Distance(transform.position, Vector3.zero) > 1){
            transform.position = Vector3.Lerp(transform.position,Vector3.zero,Time.deltaTime*15f);
            yield return null;
        }
        transform.position = Vector3.zero;
        lobby.upFloor.ActiveOpenClose();
        lobby.downFloor.ActiveOpenClose();
        yield return null;
    }




}
