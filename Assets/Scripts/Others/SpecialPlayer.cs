using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPlayer : MonoBehaviour
{
    public SpecialAtk specialAtk;
    public bool specialEnd;

    public void SpeicalFade(){
        specialEnd = true;
        gameObject.SetActive(true);
        StartCoroutine(WaitForSpeical());
    }

    IEnumerator WaitForSpeical(){
        while(specialAtk.transform.position.z < 1){
            yield return null;
                }
        SpecialEnd();
    }

    public void SpecialStart(){
        specialAtk.SetCondi(0);
        specialAtk.gameObject.SetActive(true);
    }

    public void SetEnemy(Sprite sprite, int index){
        specialAtk.enemy_sprites[index].sprite = sprite;
    }


    public void SpecialEnd(){
        specialEnd = false;
        specialAtk.gameObject.SetActive(false);
        gameObject.SetActive(false);
        specialAtk.transform.position = Vector3.zero;
        specialAtk.characters.Clear();

    }
}
