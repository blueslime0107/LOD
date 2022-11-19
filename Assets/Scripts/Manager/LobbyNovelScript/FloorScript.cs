using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] int prefloorNum;
    [SerializeField] Lobby lobby;
    [SerializeField] int fl;

    void Awake(){
        curFloor = prefloorNum;
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

    IEnumerator FloorMove(int dir){
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
        yield return null;
    }



}
