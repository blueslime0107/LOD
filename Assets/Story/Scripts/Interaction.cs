using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public DTManager dataBase;
    public SceneMove sceneMove;
    [SerializeField] GameObject standing;

    [SerializeField]TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI contentText;
    [SerializeField]List<Standing> stands = new List<Standing>();  
    int index = 0;
    bool type = false;
    string textadd = "";

    Color disAbleColor = new Color(125/255f,125/255f,125/255f);

    public void showCommand(string name, int pos, int flip){
        GameObject obj = Instantiate(standing);
        Standing stand = obj.GetComponent<Standing>();
        stand.gameObject.transform.position = Vector3.right*pos;
        stand.gameObject.SetActive(true);
        stand.name_ = name;
        stand.spriteRenderer.flipX = (flip.Equals(0)) ? false : true;
        stands.Add(stand);
    }

    public void hideCommand(string name){
        Standing stand = stands.Find(x => x.name_.Equals(name));
        stand.gameObject.SetActive(false);
    }

    public void standCommand(string name, int feel){
        Standing stand = stands.Find(x => x.name_.Equals(name));
        stand.spriteRenderer.sprite = dataBase.story.charStd.Find(x => x.name_.Equals(name)).character[feel];
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            if(type){
                StopAllCoroutines();
                type = false;
                contentText.text = textadd;
            }
            else{
                NextText();
            }
        }
    }

    IEnumerator typing(string text){
        type = true;
        
        for(int i=0;i<=text.Length;i++){
            try{
            // {if(text[i].Equals('<')){
            //     i += 4;
            // }
            contentText.text = text.Substring(0,i);
            
            }
            catch{
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        type = false;
    }

    // void PreSet(){
    //     stands[dataBase.dialogDic[index].char_sprite].spriteRenderer.sprite = dataBase.story.charStd[dataBase.dialogDic[index].char_sprite].character[dataBase.dialogDic[index].char_feel];
    //     nameText.text = dataBase.dialogDic[index].name;
    //     // textadd = "";
    //     // foreach(string text in dataBase.dialogDic[index].context){
    //     //     textadd += text;
    //     //     textadd += "<br>";
    //     // }
    //     textadd += dataBase.dialogDic[index].context;
    //     StartCoroutine(typing(textadd));
    // }

    public void NextText(){
        if(index >= dataBase.dialogDic.Count){SkipText(); return;}
        foreach(Standing stand in stands){
            if(dataBase.dialogDic[index].name.Equals(stand.name_)){
                stand.spriteRenderer.sprite = dataBase.story.charStd.Find(x => x.name_.Equals(stand.name_)).character[dataBase.dialogDic[index].char_feel];
                stand.spriteRenderer.color = Color.white;
            }
            else{
                stand.spriteRenderer.color = disAbleColor;
            }
        }
        //stands[dataBase.dialogDic[index].char_sprite].spriteRenderer.sprite = dataBase.story.charStd[dataBase.dialogDic[index].char_sprite].character[dataBase.dialogDic[index].char_feel];
        
        // if(dataBase.dialogDic[index].name.Equals("#")){
        //     index += 1;
        //     NextText();
        //     return;
        // }
        
        nameText.text = dataBase.dialogDic[index].name;
        
        textadd = "";
        // foreach(string text in dataBase.dialogDic[index].context){
        //     textadd += text.Replace('/',',');
        //     textadd += "<br>";
        // }
        textadd += dataBase.dialogDic[index].context;
        //Debug.Log(dataBase.stageManager.play_stage.victoryed);

        StartCoroutine(typing(textadd));
        index += 1;
    }

    public void SkipText(){
        if(dataBase.stageManager == null){return;}
        if(dataBase.stageManager.play_stage.victoryed){
            sceneMove.Move("Lobby");
        }
        else{
            sceneMove.Move("Battle");
        }
        
    }

}