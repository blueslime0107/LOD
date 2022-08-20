using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{

    public Sprite[] conditions;
    SpriteRenderer render;
    private void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

    public void changeCondi(int value){
        render.sprite = conditions[value];
    }
}
