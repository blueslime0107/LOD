using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseManager : MonoBehaviour
{
    public StoryScript story;
    
    [SerializeField] Interaction interact;

    public Dictionary<int,Dialog> dialogDic = new Dictionary<int,Dialog>();

    private void Awake() {
        DialogParse thrParser = GetComponent<DialogParse>();
        Dialog[] dialoges = thrParser.Parse(story.scriptName);
        for(int i = 0;i< dialoges.Length;i++){
            dialogDic.Add(i,dialoges[i]);
        }

    }

    // public Dialog[] GetDialogs(){
    //     List<Dialog> dialogList = new List<Dialog>();

    //     for(int i=0; i<= EndNum - StartNum; i++){
    //         dialogList.Add(dialogDic[StartNum+i]);
    //     }
    //     return dialogList.ToArray();
    // }


}
