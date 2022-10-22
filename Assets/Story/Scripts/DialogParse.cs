using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogParse : MonoBehaviour
{
    public Dialog[] Parse(string filename){
        List<Dialog> dialogList = new List<Dialog>();
        TextAsset csvData = Resources.Load<TextAsset>(filename);

        string[] data = csvData.text.Split(new char[]{'\n'});

        for(int i =1; i<data.Length;){
            string[] row = data[i].Split(new char[]{','}); 

            Dialog dialog = new Dialog();

            dialog.char_sprite = int.Parse(row[0])-1;
            dialog.char_condi = int.Parse(row[1])-1;
            try
            {dialog.char_show = int.Parse(row[4]);}
            catch{dialog.char_show = 0;}
            dialog.name = row[2];
            List<string> contextList = new List<string>();

            do{
                contextList.Add(row[3]);
                if(++i < data.Length){
                 row = data[i].Split(new char[]{','}); 
                }
                else{
                    break;
                }
            }while(row[0].ToString().Equals(""));

            dialog.context = contextList.ToArray();
            dialogList.Add(dialog);
            
        } 
        return dialogList.ToArray();
    }
}
