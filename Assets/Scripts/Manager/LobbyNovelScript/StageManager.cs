using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    public Stage play_stage; // 플레이 할 스테이지
    public Stage player_card; // 플레이어 카드

    public List<Stage> Floor1Stage1 = new List<Stage>();
    public List<Stage> Floor1Stage2 = new List<Stage>();
    public List<Stage> Floor1SubStage = new List<Stage>();
    public List<Stage> Floor2Stage1 = new List<Stage>();
    public List<Stage> Floor2Stage2 = new List<Stage>();
    public List<Stage> Floor2SubStage = new List<Stage>();
    public List<Stage> Floor3Stage1 = new List<Stage>();
    public List<Stage> Floor3Stage2 = new List<Stage>();
    public List<Stage> Floor3SubStage = new List<Stage>();

    private void Awake() {
        var obj = FindObjectsOfType<StageManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }

}
