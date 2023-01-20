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
    [SerializeField]Image backGround;
    [SerializeField]Image frontGround;
    int index = 0;
    bool type = false;
    string textadd = "";

    Color disAbleColor = new Color(125/255f,125/255f,125/255f);

    public void showCommand(string name, float pos, int flip, float preY){
        GameObject obj = Instantiate(standing);
        Standing stand = obj.GetComponent<Standing>();
        stand.gameObject.transform.position = Vector3.right*pos;
        stand.gameObject.transform.position += Vector3.up * preY;
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
            contentText.text = text.Substring(0,i);
            dataBase.sound.Play("pop");
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

        if(dataBase.dialogDic[index].function_name != null){
            Debug.Log("fucc");
            switch(dataBase.dialogDic[index].function_name){
                case "/show":
                showCommand(dataBase.dialogDic[index].targetChar,dataBase.dialogDic[index].parameters[0],((int)dataBase.dialogDic[index].parameters[1]),dataBase.dialogDic[index].parameters[2]); break;
                case "/hide":
                hideCommand(dataBase.dialogDic[index].targetChar); break;
                case "/stand":
                standCommand(dataBase.dialogDic[index].targetChar,((int)dataBase.dialogDic[index].parameters[0])); break;
            }
            index ++ ;
            NextText();
            return;
        }


        foreach(Standing stand in stands){
            if(dataBase.dialogDic[index].name.Equals(stand.name_)){
                stand.spriteRenderer.sprite = dataBase.story.charStd.Find(x => x.name_.Equals(stand.name_)).character[dataBase.dialogDic[index].char_feel];
                stand.spriteRenderer.color = Color.white;
                stand.transform.position = SetVector3z(stand.transform.position,-1);
            }
            else{
                stand.spriteRenderer.color = disAbleColor;
                stand.transform.Translate(Vector3.back);
                stand.transform.position = SetVector3z(stand.transform.position,0);
            }
        }
        
        nameText.text = dataBase.dialogDic[index].name;
        
        textadd = "";
        textadd += dataBase.dialogDic[index].context;

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

    public Vector3 SetVector3z(Vector3 pre_vec,float z){
        Vector3 vec = pre_vec;
        vec.z = z;
        return vec;

    }

}