using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{

    public Sprite[] conditions;
    SpriteRenderer render;

    [SerializeField]Material defalt;
    [SerializeField]Material disabled;
    private void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    public void changeCondi(int value){
        render.sprite = conditions[value];
        if(value == 0){
            render.material = disabled;
        }
        else{
            if(render.material.Equals(disabled)){
                render.material = defalt;
            }
        }
    }
}
