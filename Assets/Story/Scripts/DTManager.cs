using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class DTManager : MonoBehaviour
{
    public StageManager stageManager;
    public SoundManager sound;

    public StoryScript story;

    [SerializeField]Interaction inter;

    [SerializeField]public List<Dialog> dialogDic = new List<Dialog>();

    private void Awake() {
        stageManager = FindObjectOfType<StageManager>();
        if(stageManager){story = (!stageManager.play_stage.victoryed) ? stageManager.play_stage.beforeStory : stageManager.play_stage.afterStory;}

        TextAsset scriptPath = new TextAsset();

        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){scriptPath = story.texts[0];}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){scriptPath = story.texts[1];}

        Dialog[] dialoges = Parse(scriptPath);


        for(int i = 0;i< dialoges.Length;i++){
            dialogDic.Add(dialoges[i]);
        }

        
        inter.NextText();
        
        }

        

    public Dialog[] Parse(TextAsset filename){
        List<Dialog> dialogList = new List<Dialog>();
        TextAsset csvData = filename;//(TextAsset) Resources.Load(filename.Split(".")[0]);

        string[] data = csvData.text.Split(new char[]{'\n'});

        for(int i=1; i<data.Length; i++){
            string[] row = data[i].Split(new char[]{','}); 
            Dialog dialog = new Dialog();
            if(row.Length == 1){
                dialogList[dialogList.Count-1].context += "\n" + row[0];
                i++;
                if(i>=data.Length){break;}
                row = data[i].Split(new char[]{','}); 
            }
            if(row.Length == 1){
                dialogList[dialogList.Count-1].context += "\n" + row[0];
                i++;
                if(i>=data.Length){break;}
                row = data[i].Split(new char[]{','}); 
            }
            if(row[1].Contains("/")){
                string[] magevar = row[2].Split(';');
                if(row[1].Equals("/show")){
                    dialog.function_name = row[1];
                    dialog.targetChar = magevar[0];
                    dialog.parameters.Add(float.Parse(magevar[1]));
                    dialog.parameters.Add(float.Parse(magevar[2]));
                    dialog.parameters.Add(story.charStd.Find(x => x.name_.Equals(magevar[0])).preY);

                }
                if(row[1].Equals("/hide")){
                    dialog.function_name = row[1];
                    dialog.targetChar = magevar[0];
                }
                if(row[1].Equals("/stand")){
                    dialog.function_name = row[1];
                    dialog.targetChar = magevar[0];
                    dialog.parameters.Add(int.Parse(magevar[1])-1);
                } 
                dialogList.Add(dialog);
            }
            else{
            dialog.name = row[0];
            dialog.char_feel = int.Parse(row[1])-1;
            dialog.context = row[2].Replace("\\n","\n");
            dialogList.Add(dialog);
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