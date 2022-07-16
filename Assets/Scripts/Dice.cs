using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    
    public Sprite[] dice_img;
    SpriteRenderer render;

    void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            render.sprite = dice_img[(int)Random.Range(0f,6f)];
        }
    }

    IEnumerator rolling{

    }
}
