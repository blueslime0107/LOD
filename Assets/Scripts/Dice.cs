using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public int dice_code = 0;

    public Sprite[] dice_img;
    SpriteRenderer render;
    public GameManager gameManager;
    public int dice_value;

    public int team_type;
    bool draging;
    bool char_collided;
    string char_name;


    void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    void Start(){
        StartCoroutine("rolling");
    }

    // Update is called once per frame
    void Update()
    {
        OnDiceDrag();
    }

    IEnumerator rolling(){
        for(int i = 0; i < 30; i++){
            yield return new WaitForSeconds(0.05f);
            transform.Rotate(new Vector3(0,0,30));
            dice_value = (int)Random.Range(1f,7f);
            render.sprite = dice_img[dice_value-1];
        }
        transform.rotation = Quaternion.Euler(0,0,0);
        

    }


    void OnDiceDrag() {
        if ((Input.GetMouseButtonUp(0)) && char_collided)
        {
            gameManager.SetCharDice(dice_value, char_name);
            gameObject.SetActive(false);
        }
        if ((Input.GetMouseButton(0)) && draging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10);
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition);

        }
        
    }

    private void OnMouseDrag() {
        draging = true;
    }

    private void OnMouseUp() {
        draging = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Team1" && team_type == 1){
            char_collided = true; 
            char_name = collision.gameObject.name;    
        }
        if(collision.gameObject.tag == "Team2" && team_type == 2){
            char_collided = true;  
            char_name = collision.gameObject.name;      
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Team1" && team_type == 1){
            char_collided = false;  
            char_name = collision.gameObject.name;      
        }
        if(collision.gameObject.tag == "Team2" && team_type == 2){
            char_collided = false;   
            char_name = collision.gameObject.name;     
        }
    }

}
