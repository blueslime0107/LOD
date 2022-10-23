using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{

    public StageManager stageManager;

    [SerializeField] SceneMove sceneM;
    [SerializeField] GameObject darScreen;
    [SerializeField] MenuItem sub_table;
    [SerializeField] MenuItem enemyCard;
    [SerializeField] MenuItem playerCard;
    [SerializeField] MenuItem BattleButton;

    [Tooltip("서브 스테이지 보드")]
    [SerializeField] GameObject subStage_board;
    [Tooltip("서브 스테이지 아이템")]
    [SerializeField] GameObject subStage;

    public Stage[] subStages;
    List<SubBattleItem> subStageItem = new List<SubBattleItem>();

    string curMenu = "lobby";

    

    public BattleCardManager playerBattleCard;
    public BattleCardManager enemyBattleCard;

    public Stage stage;
    public Stage player;


    public void Update(){
        if(Input.GetMouseButtonDown(1)){
            if(curMenu.Equals("battle")){
                enemyCard.ActiveOpenClose();
                playerCard.ActiveOpenClose();
                BattleButton.ActiveOpenClose();
                curMenu = "surMenu";
                return;
            }
            if(curMenu.Equals("surMenu")){
                sub_table.ActiveOpenClose();
                darScreen.SetActive(false);        
                curMenu = "lobby";
                return;
            }
        }
    }

    public void OpenSubMenu(){
        if(curMenu.Equals("surMenu")){
            return;
        }
        sub_table.ActiveOpenClose();
        darScreen.SetActive(true);
        RenderSubMenu();
        curMenu = "surMenu";
    }
    public void RenderSubMenu(){
        for(int i=0;i<subStages.Length;i++){
            if(subStageItem.Count <= i){
                SubBattleItem obj = Instantiate(subStage).GetComponent<SubBattleItem>();
                obj.transform.SetParent(subStage_board.transform,false);   
                obj.lobby = this;      
                subStageItem.Add(obj);
            }
            subStageItem[i].stage = subStages[i];
            subStageItem[i].UpdateStat();

        }
    }


    public void OpenBattleCard(){
        if(curMenu.Equals("battle")){
            return;
        }
        playerBattleCard.UpdateStat();
        enemyBattleCard.UpdateStat();
        enemyCard.ActiveOpenClose();
        playerCard.ActiveOpenClose();
        BattleButton.ActiveOpenClose();
        curMenu = "battle";
    }

    public void GetStory(){
        if(stage.beforeStory != null)
        {sceneM.MoveStory();}
        else{
            sceneM.MoveBattle();
        }
    }

}
