using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NovelPlayerMain : MonoBehaviour
{
    public NovelPlayer np;
    public SceneMove sceneMove;
    public DataBase db;
    [SerializeField] GameObject standing;

    [SerializeField]TextMeshProUGUI nameText;
    [SerializeField]TextMeshProUGUI contentText;
    [SerializeField]List<Standing> stands = new List<Standing>();  
    [SerializeField]List<GameObject> backgrounds = new List<GameObject>();  

    [SerializeField]SpriteRenderer[] pictureRenderer;

    int index = 0;
    bool type = false;
    string textadd = "";
    public Sprite blackIMG;
    public Standing nullStand;

    Color disAbleColor = new Color(125/255f,125/255f,125/255f);

    [SerializeField]float preY;

    private void Awake() {
        db.Localize();
    }
    private void Start() {
        np.sound.PlayBGM("Dialog1");    
    }

    public void showCommand(int id, float pos,int feel){
        GameObject obj = Instantiate(standing);
        Standing stand = obj.GetComponent<Standing>();
        stand.gameObject.transform.position = Vector3.right*pos;
        stand.gameObject.transform.position += Vector3.up * preY;
        stand.charpack = db.charpacks.Find(x => x.id == id);
        stand.changeFeeling(feel);
        stand.gameObject.SetActive(!pictureRenderer[0].gameObject.activeSelf);
        stand.spriteRenderer.flipX = stand.transform.position.x < 0;
        stands.Add(stand);
    }

    public void hideCommand(int id,int bools){
        Standing stand = stands.Find(x => x.charpack.id.Equals(id));
        stand.gameObject.SetActive((bools.Equals(0)) ? false:true);
    }

    public void feelCommand(int id,int feel){
        Standing stand = stands.Find(x => x.charpack.id.Equals(id));
        stand.changeFeeling(feel);
    }

    public void picture(int id){
        Debug.Log(id);
        if(id < 0){
            if(id.Equals(-2)){
            foreach(SpriteRenderer sprite in pictureRenderer){
                sprite.gameObject.SetActive(false);
            }
            foreach(Standing stand in stands){
                stand.gameObject.SetActive(true);
            }
            }
            if(id.Equals(-1)){
            pictureRenderer[0].sprite = blackIMG;
            pictureRenderer[0].gameObject.SetActive(true);
            foreach(Standing stand in stands){
                stand.gameObject.SetActive(false);
            }
            }
            return;
        
        }
        foreach(SpriteRenderer sprite in pictureRenderer){
            if(sprite.sprite == null){
                sprite.sprite = np.story.pictures[id];
                sprite.gameObject.SetActive(true);
                break;
            }
        }
    
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
            np.sound.Play("pop");
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
        if(index >= np.dig.Count){SkipText(); return;}

        if(np.dig[index].funcType != "Text"){
            switch(np.dig[index].funcType){
                case "S":
                    showCommand(np.dig[index].var1,np.dig[index].var2,np.dig[index].var3); break;
                case "H":
                    hideCommand(np.dig[index].var1,np.dig[index].var2); break;
                case "F":
                    feelCommand(np.dig[index].var1,np.dig[index].var2); break;
                case "P":
                    picture(np.dig[index].var1); break;
            }
            index ++;
            NextText();
            return;
        }

        Standing talkingStand = stands.Find(x => x.charpack.id == np.dig[index].var1);

        foreach(Standing stand in stands){
            if(stand == talkingStand){
                stand.spriteRenderer.color = Color.white;
                stand.transform.position = SetVector3z(stand.transform.position,-1);
            }
            else{
                stand.spriteRenderer.color = disAbleColor;
                stand.transform.Translate(Vector3.back);
                stand.transform.position = SetVector3z(stand.transform.position,0);
            }
        }

        nameText.text = (talkingStand != null) ? talkingStand.charpack.name_ : np.dig[index].sub_name;
        textadd = "";
        textadd += np.dig[index].context;

        StartCoroutine(typing(textadd));
        index += 1;
    }

    public void SkipText(){
        if(np.story.nextStory != null){
            np.stageManager.playStory = np.story.nextStory;
            sceneMove.Move("Talk");
            return;
        }



        if(np.stageManager.play_stage.victoryed){
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