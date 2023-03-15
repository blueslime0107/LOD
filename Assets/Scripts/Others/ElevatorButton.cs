using UnityEngine;
using UnityEngine.UI;

public class ElevatorButton : MonoBehaviour
{
    public Button button;
    public int stageFloor;
    public DiceIcon diceIcon;
    public Image stageImg;
    public Image alartImg;

    StageManager sm;

    public bool newStageDetected;


    private void Start() {
        button.interactable = false;
        
        diceIcon.SetRank(stageFloor+1);
        BattleLoading();
    }

    public void BattleLoading(){
        sm = FindObjectOfType<StageManager>();
        if(sm.Floors[stageFloor].allStages().Count > 0){button.interactable = true;}
        RefreshDiscover();
        //floor.addedStage = false;
    }

    public void RefreshDiscover(){
        newStageDetected = false;
        foreach(Stage stage in sm.Floors[stageFloor].allStages()){
            if(stage.discovered){continue;}
            newStageDetected = true;
            button.interactable = true;
        }
        alartImg.gameObject.SetActive(newStageDetected);
    }
}
