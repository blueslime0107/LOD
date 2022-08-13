using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDraw : MonoBehaviour
{
    [SerializeField]
    public BattleManager battleManager;
    SpriteRenderer render;
    [SerializeField]
    Sprite[] card_img; // 카드 이미지

    Vector3 origin_size;
    Vector3 origin_position;

    bool mouseOn = false;
    bool mouseDrag = false;
    [SerializeField]
    int target;
    int card_id;

    // Start is called before the first frame update
    void Awake() // 생성되자마자 실행
    {
        render = GetComponent<SpriteRenderer>();
        origin_position = transform.position;
        origin_size = transform.localScale;
    }

    void Update(){

        if(Input.GetMouseButtonUp(0)){
            transform.position = origin_position;
            transform.localScale = origin_size; 
        }
        if(Input.GetMouseButtonDown(0)){
            if(!mouseOn){
                transform.position += Vector3.forward*2; // 뒤로가서 안보이게
            }            
        }
        if(battleManager.card_gived)   // 이미 카드를 줬을때 사라지기
            Destroy(gameObject);
        
    }




    // 외부에서 실행 카드 이미지 바꾸기
    public void SetImage(int num){
        card_id = num;
        render.sprite = card_img[num];
    }

    // 드래그 중 마우스 따라가기 크기 줄어들기
    void OnMouseDrag(){
        mouseDrag = true;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9f);        
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);  
        transform.position = objPosition;
        transform.localScale = new Vector3(0.10f,0.10f,-1);
    }

    private void OnMouseExit() {
        mouseOn = false;
    }

    void OnMouseEnter() {
        mouseOn = true;
    }

    private void OnMouseUp() { // 자신이 선택됬고 캐릭터를 정했을때 카드 줌
        if(target>0 && mouseOn){
            Debug.Log("didididi");
            battleManager.players[target-1].AddCard(card_id);
            battleManager.card_gived = true;
            battleManager.card_draw -= 1;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.name == "LPlayer1"){
                target = 1;
            }                
            else if(collision.gameObject.name == "LPlayer2"){
                target = 2;
            }                
            else if(collision.gameObject.name == "LPlayer3"){
                target = 3;
            }                
            else if(collision.gameObject.name == "RPlayer4"){
                target = 4;
            }                
            else if(collision.gameObject.name == "RPlayer5"){
                target = 5;
            }                
            else if(collision.gameObject.name == "RPlayer6"){
                target = 6;
            }
        }
        

    }

    void OnTriggerStay2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.name == "LPlayer1"){
                target = 1;
            }                
            else if(collision.gameObject.name == "LPlayer2"){
                target = 2;
            }                
            else if(collision.gameObject.name == "LPlayer3"){
                target = 3;
            }                
            else if(collision.gameObject.name == "RPlayer4"){
                target = 4;
            }                
            else if(collision.gameObject.name == "RPlayer5"){
                target = 5;
            }                
            else if(collision.gameObject.name == "RPlayer6"){
                target = 6;
            }
        }
        

    }

    void OnTriggerExit2D(Collider2D collision) {
        if(mouseDrag){
            if(collision.gameObject.name == "LPlayer1"){
                target = 0;
            }                
            else if(collision.gameObject.name == "LPlayer2"){
                target = 0;
            }                
            else if(collision.gameObject.name == "LPlayer3"){
                target = 0;
            }                
            else if(collision.gameObject.name == "RPlayer4"){
                target = 0;
            }                
            else if(collision.gameObject.name == "RPlayer5"){
                target = 0;
            }                
            else if(collision.gameObject.name == "RPlayer6"){
                target = 0;
            }
        }
        

    }

}
