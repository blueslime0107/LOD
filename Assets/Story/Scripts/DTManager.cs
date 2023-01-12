using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DTManager : MonoBehaviour
{
    public StageManager stageManager;

    public StoryScript story;

    [SerializeField]Interaction inter;

    [SerializeField]public List<Dialog> dialogDic = new List<Dialog>();

    private void Awake() {
        stageManager = FindObjectOfType<StageManager>();
        if(stageManager){story = (!stageManager.play_stage.victoryed) ? stageManager.play_stage.beforeStory : stageManager.play_stage.afterStory;}
        
        string scriptPath = "";

        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){scriptPath = story.text_path[0];}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){scriptPath = story.text_path[1];}

        Dialog[] dialoges = Parse(scriptPath);


        for(int i = 0;i< dialoges.Length;i++){
            dialogDic.Add(dialoges[i]);
        }

        inter.NextText();

        }

        

    public Dialog[] Parse(string filename){
        List<Dialog> dialogList = new List<Dialog>();
        TextAsset csvData = (TextAsset) Resources.Load(filename.Split(".")[0]);

        Debug.Log(csvData);
        Debug.Log(csvData.text);

        string[] data = csvData.text.Split(new char[]{'\n'});

        for(int i=1; i<data.Length; i++){
            string[] row = data[i].Split(new char[]{','}); 

            Dialog dialog = new Dialog();
            try{
            if(row[1].Contains("/")){
                string[] magevar = row[2].Split(';');
                if(row[1].Equals("/show")){
                    inter.showCommand(magevar[0],int.Parse(magevar[1]),int.Parse(magevar[2]));
                }
                if(row[1].Equals("/hide")){
                    inter.hideCommand(magevar[0]);
                }
                if(row[1].Equals("/stand")){
                    inter.standCommand(magevar[0],int.Parse(magevar[1]));
                }
                
                
            }
            else{
            dialog.name = row[0];
            dialog.char_feel = int.Parse(row[1])-1;
            dialog.context = row[2].Replace("\\n","\n");
            dialogList.Add(dialog);
            }}
            catch{
                break;
            }


            //List<string> contextList = new List<string>();

            // do{
            //     contextList.Add(row[3]);
            //     if(++i < data.Length){
            //      row = data[i].Split(new char[]{','}); 
            //     }
            //     else{
            //         break;
            //     }
            // }while(row[0].ToString().Equals(""));
            
            
            
        } 
        return dialogList.ToArray();
    }

    
}