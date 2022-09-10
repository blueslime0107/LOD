using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDice : MonoBehaviour
{
    public BattleManager battleManager;
    public Sprite[] dice_img;

    public SpriteRenderer render;
    public Player player;
    public LineRenderer lineRender;

    public ParticleSystem left_break;
    public ParticleSystem right_break;

    public bool spinging;

    void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        left_break.Stop();
        right_break.Stop();
        transform.position = new Vector3(battleManager.camera.transform.position.x,battleManager.camera.transform.position.y+1.5f,-2);
        spinging = true;
        StartCoroutine("Spining");
        
    }

    public void SetPlayerPosition(Player player1, Player player2){
        transform.position = (player1.dice_Indi.transform.position + player2.dice_Indi.transform.position) * 0.5f + Vector3.up*0.5f+Vector3.forward*2;
    }

    IEnumerator Spining(){
        while(true){
            transform.Rotate(0,0,120*60*Time.deltaTime);
            yield return null;
        }
    }

    public void DamageReset(){
        render.sprite = dice_img[0];
    }

    public void DamageUpdate(){
        if(spinging){
            StopCoroutine("Spining");          
            transform.localEulerAngles = Vector3.zero;
             spinging = false;
        }
        render.sprite = dice_img[Mathf.Abs(battleManager.battleCaculate.damage)];
    }
}
