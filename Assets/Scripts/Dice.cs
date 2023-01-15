using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Player player;
    public int dice_value;
    public Vector2 self_pos;
    public bool chartouch;
    GameObject charTarget;
    [SerializeField] public BattleManager battleManager;
    public bool cannot_roll = false;
    [HideInInspector]public bool rolled;

    //public int dice_num;

    public Sprite[] dice_img;
    public bool farAtt = false;
    SpriteRenderer render;
    [HideInInspector]public List<int> diceLock = new List<int>();

    void Awake(){
        enabled = true;
        gameObject.SetActive(true);
        render = GetComponent<SpriteRenderer>();
    }

    public void rolldice(Vector2 vector){ // 1 부터 6 랜덤 주사위 굴리기
        if(cannot_roll)
            return;
        gameObject.SetActive(true);
        StartCoroutine(rollingDice(vector));
        StartCoroutine(rollingDiceSound());
    }

    IEnumerator rollingDice(Vector2 vector){
        rolled = false;
        self_pos = vector;
        transform.position = Vector3.up*4.5f;
        yield return new WaitForSeconds(0.2f);
        Vector2 velo = Vector2.zero;
        while(true){
            transform.position = Vector2.SmoothDamp(transform.position,vector,ref velo,5f*Time.deltaTime);
            dice_value = (int)Random.Range(1f,7f);
            render.sprite = dice_img[dice_value-1];
            transform.Rotate(Time.deltaTime*Vector3.forward*360f);
            yield return null;
        }
    
    }
    IEnumerator rollingDiceSound(){
        while(true){
            battleManager.sdm.Play("RollDice");
            yield return new WaitForSeconds(0.1f);
        }
    }

    

    public void StopRollingDice(){
        StopAllCoroutines();
        rolled = true;
        transform.localEulerAngles = Vector3.zero;
        dice_value = (int)Random.Range(1f,7f);
        render.sprite = dice_img[dice_value-1];
        battleManager.sdm.Play("HitTable");
        if(diceLock.Count>0){
            setDice(diceLock[0]);
            diceLock.RemoveAt(0);
        }
    }

    public void setDice(int value){
        dice_value = value;
        render.sprite = dice_img[dice_value-1];
    }

    private void OnMouseDown() {
        battleManager.sdm.Play("DiceGrab");
    }

    void OnMouseDrag() { // 마우스 
        if(!battleManager.cur_team.players.Contains(player)){return;}
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);        
        transform.position = objPosition;
    }

    Dice_Indi dice_im;

    void OnMouseUp() {
        try{
            if(chartouch){
            if(!dice_im.isDiced){
                rolled = false;
                gameObject.SetActive(false);
                dice_im.putDice(this);
                
            }
                
        }
        }
        catch{

        }
        
        
    }

    // 주사위가 주사위지정 칸에 닿았는가?
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == gameObject.tag){
            charTarget = other.gameObject; // 트리거로 받은 오브젝트는 other.gameObject
            try{
                
                dice_im = charTarget.GetComponent<Dice_Indi>();
                dice_im.player.ShowCardDeck(false);
                
            }
            catch{}
            chartouch = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == gameObject.tag)
            chartouch = false;
    }

    // 외부실행 주사위 다시 보이게함
    public void diceReroll(){
        gameObject.SetActive(true);
        rolldice(self_pos);
    }

    
}
