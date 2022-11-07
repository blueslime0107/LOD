using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTManager : MonoBehaviour
{
    public StageManager stageManager;

    public StoryScript story;

    public Dictionary<int,Dialog> dialogDic = new Dictionary<int,Dialog>();

    private void Awake() {
        stageManager = FindObjectOfType<StageManager>();

        story = (!stageManager.play_stage.victoryed) ? stageManager.play_stage.beforeStory : stageManager.play_stage.afterStory;
        
        DialogParse thrParser = GetComponent<DialogParse>();
        Dialog[] dialoges = thrParser.Parse(story.scriptName);
        for(int i = 0;i< dialoges.Length;i++){
            dialogDic.Add(i,dialoges[i]);
        }
        
        
        
        
        
        
        
        }
}