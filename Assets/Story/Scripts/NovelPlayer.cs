using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class NovelPlayer : MonoBehaviour
{
    public StageManager stageManager;
    public SoundManager sound;

    public StoryScript story;

    [SerializeField]NovelPlayerMain novelMain;

    [SerializeField]public List<Dialog> dig = new List<Dialog>();

    public void Awake(){
        stageManager = FindObjectOfType<StageManager>();
        int localIndex =0;
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[0])){localIndex = 0;}
        if(LocalizationSettings.SelectedLocale.Equals(LocalizationSettings.AvailableLocales.Locales[1])){localIndex = 1;}

        story = stageManager.playStory;

        Instantiate(story.backGrounds);

        dig = Parse(story.story[localIndex]);

    }

    public void Start(){
        novelMain.NextText();

    }

    public List<Dialog> Parse(TextAsset filename){
        List<Dialog> dialogList = new List<Dialog>();
        TextAsset csvData = filename;//(TextAsset) Resources.Load(filename.Split(".")[0]);

        string[] data = csvData.text.Split(new char[]{'\n'}); // 개행마다 나누기

        for(int i=0; i<data.Length; i++){
            string[] row = data[i].Split(new char[]{';'}); 
            Dialog dialog = new Dialog();
            if(row.Length == 1){ // 그냥 텍스트만 있다면 전 대사에 개행해서 추가한다
                dialogList[dialogList.Count-1].context += "\n" + row[0];
                i++;
                if(i>=data.Length){break;}
                row = data[i].Split(new char[]{';'}); 
            }
            if(row.Length == 1){ // 최대 2줄 까지 처리
                dialogList[dialogList.Count-1].context += "\n" + row[0];
                i++;
                if(i>=data.Length){break;}
                row = data[i].Split(new char[]{';'}); 
            }

            foreach(string index in row){
                dialog.funcType = index; 
            }

            switch(row[0]){
                case "S":
                    dialog.funcType = row[0];
                    dialog.var1 = int.Parse(row[1]);
                    dialog.var2 = int.Parse(row[2]);
                    dialog.var3 = int.Parse(row[3]);
                    break;
                case "M":
                    dialog.funcType = row[0];
                    dialog.var1 = int.Parse(row[1]);
                    dialog.var2 = int.Parse(row[2]);
                    break;
                case "F":
                    dialog.funcType = row[0];
                    dialog.var1 = int.Parse(row[1]);
                    dialog.var2 = int.Parse(row[2]);
                    break;
                case "H":
                    dialog.funcType = row[0];
                    dialog.var1 = int.Parse(row[1]);
                    dialog.var2 = int.Parse(row[2]);
                    break;
                case "P":
                    dialog.funcType = row[0];
                    dialog.var1 = int.Parse(row[1]);
                    break;
                default:
                    dialog.funcType = "Text";
                    try{
                    dialog.var1 = int.Parse(row[0]);}
                    catch{
                        dialog.sub_name = row[0];
                    }
                    dialog.context = row[1]; break;
            }
            dialogList.Add(dialog);
        } 
        return dialogList;
    }





    
}