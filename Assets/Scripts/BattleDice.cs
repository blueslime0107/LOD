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
    void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        transform.position = new Vector3(battleManager.camera.transform.position.x,battleManager.camera.transform.position.y+1.5f,-2);
        //battleManager.camera.transform.position + Vector3.down*0.5f + Vector3.forward*8;
        
    }

    void Update()
    {
        render.sprite = dice_img[Mathf.Abs(battleManager.battleCaculate.damage)];
    }
}
