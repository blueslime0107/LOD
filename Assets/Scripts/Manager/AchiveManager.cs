using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AchiveManager : MonoBehaviour
{

    [SerializeField]TextMeshProUGUI achieveText;
    StageManager stageManager;
    public bool reaminAchieved;

    void Start(){
        stageManager = FindObjectOfType<StageManager>();
        // foreach(AchieveMent achi in stageManager.achiveItms){
        //     if(!achi.stack){
        //         achi.count = 0;
        //     }
        // }
    }



    public void RenderAchieveText(){
        achieveText.text = "";
        string newText = "";
        // foreach(AchieveMent achieve in stageManager.achiveItms){
        //     if(achieve.achieved && !reaminAchieved){continue;}
            
        //     achieveText.text += "--------------------------------";
        //     newText = (achieve.achieved && reaminAchieved) ? "<color=yellow>" : "";
        //     newText += (achieve.stack) ? "[누적]":"";
        //     newText += achieve.quest_text;
        //     newText += "\n";
        //     if(achieve.max_count > 0){

        //     newText += "("+achieve.count.ToString()+"/"+achieve.max_count.ToString()+")";
            
            
        //     }
        //     if(achieve.achieved && reaminAchieved){newText += "</color>";}
        //     newText += "\n";
        //     achieveText.text += newText;

        // }
    }
}
