using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Standing : MonoBehaviour
{
    public CharPack charpack;
    public SpriteRenderer spriteRenderer;

    public void changeFeeling(int feel){
        spriteRenderer.sprite = charpack.feeling[feel];
    }
}
