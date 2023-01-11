using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public DTManager dataBase;
    public SceneMove sceneMove;
    [SerializeField] GameObject parentStanding;
    [SerializeField] GameObject standing;

    [SerializeField]TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI contentText;
    List<StandingScript> standing_list = new List<StandingScript>();  
    int index = 0;
    bool type = false;
    string textadd = "";

    Color disAbleColor = new Color(125/255f,125/255f,125/255f);

    public void Start(){
        for(int i=0;i<dataBase.story.char_list.Length;i++){
            GameObject obj = Instantiate(standing);
            StandingScript script = obj.GetComponent<StandingScript>();
            script.gameObject.transform.position += Vector3.right*dataBase.story.char_x[i];
            standing_list.Add(script);
        }
        NextText();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            if(type){
                StopAllCoroutines();
                type = false;
                contentText.text = textadd;
            }
            else{
                index += 1;
                NextText();
            }
        }
    }

    IEnumerator typing(string text){
        type = true;
        
        for(int i=0;i<=text.Length;i++){
            try
            {if(text[i].Equals('<')){
                i += 4;
            }
            contentText.text = text.Substring(0,i);
            
            }
            catch{
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        type = false;
    }

    void PreSet(){
        standing_list[dataBase.dialogDic[index].char_sprite].spriteRenderer.sprite = dataBase.story.char_list[dataBase.dialogDic[index].char_sprite].character[dataBase.dialogDic[index].char_condi];
        nameText.text = dataBase.dialogDic[index].name;
        textadd = "";
        foreach(string text in dataBase.dialogDic[index].context){
            textadd += text;
            textadd += "<br>";
        }
        StartCoroutine(typing(textadd));
    }

    void NextText(){
        try{foreach(StandingScript stand in standing_list){
            if(standing_list[dataBase.dialogDic[index].char_sprite].Equals(stand)){
                standing_list[dataBase.dialogDic[index].char_sprite].spriteRenderer.sprite = dataBase.story.char_list[dataBase.dialogDic[index].char_sprite].character[dataBase.dialogDic[index].char_condi];
                standing_list[dataBase.dialogDic[index].char_sprite].spriteRenderer.color = Color.white;
            }
            else{
                stand.spriteRenderer.color = disAbleColor;
            }
        }
        standing_list[dataBase.dialogDic[index].char_sprite].spriteRenderer.sprite = dataBase.story.char_list[dataBase.dialogDic[index].char_sprite].character[dataBase.dialogDic[index].char_condi];
        if(dataBase.dialogDic[index].char_show.Equals(1)){
            standing_list[dataBase.dialogDic[index].char_sprite].gameObject.SetActive(true);
        }
        if(dataBase.dialogDic[index].char_show.Equals(2)){
            standing_list[dataBase.dialogDic[index].char_sprite].gameObject.SetActive(false);
        }
        
        if(dataBase.dialogDic[index].name.Equals("#")){
            index += 1;
            NextText();
            return;
        }
        
        nameText.text = dataBase.dialogDic[index].name;
        
        textadd = "";
        foreach(string text in dataBase.dialogDic[index].context){
            textadd += text.Replace('/',',');
            textadd += "<br>";
        }
        Debug.Log(dataBase.stageManager.play_stage.victoryed);

        StartCoroutine(typing(textadd));}
        catch{
            SkipText();
        }
    }

    public void SkipText(){
        if(dataBase.stageManager.play_stage.victoryed){
            sceneMove.Move("Lobby");
        }
        else{
            sceneMove.Move("Lobby");
        }
        
    }

}
