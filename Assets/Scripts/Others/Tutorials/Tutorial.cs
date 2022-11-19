using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]GameObject sprite_mask;
    [SerializeField]GameObject[] scenes;
    [SerializeField]BattleManager bm;

    void Start(){
        if(bm.gameManager.sm.play_stage.tutorialLine > 0){
            if(bm.gameManager.sm.play_stage.tutorialLine.Equals(1)){
                StartCoroutine("tutorial1");
            }
            if(bm.gameManager.sm.play_stage.tutorialLine.Equals(2)){
                sprite_mask.SetActive(false);
                StartCoroutine("tutorial2");
            }
        }
        else{
            gameObject.SetActive(false);
        }
    }

    IEnumerator tutorial1(){
        bm.dices[0].diceLock.Add(4);
        bm.dices[1].diceLock.Add(2);
        bm.dices[0].diceLock.Add(3);
        bm.dices[1].diceLock.Add(6);
        bm.dices[0].diceLock.Add(1);
        bm.dices[1].diceLock.Add(6);
        bm.dices[0].diceLock.Add(2);
        bm.dices[1].diceLock.Add(5);
        bm.dices[0].diceLock.Add(5);
        bm.dices[1].diceLock.Add(1);
        bm.dices[0].diceLock.Add(4);
        bm.dices[1].diceLock.Add(2);
        bm.dices[0].diceLock.Add(6);
        bm.dices[1].diceLock.Add(1);
        bm.dices[0].diceLock.Add(4);
        bm.dices[1].diceLock.Add(1);
        yield return new WaitForSeconds(0.1f);  
        bm.ui.battleStartButton.gameObject.SetActive(false);
        scenes[0].SetActive(true);
        while(bm.left_players.FindAll(x => x.dice>0 || x.died).Count < bm.left_players.Count){yield return null;}
        yield return null;
        scenes[0].SetActive(false);

        scenes[1].SetActive(true);
        bm.ui.battleStartButton.gameObject.SetActive(true);
        while(!bm.battle_start){yield return null;}
        yield return null;
        scenes[1].SetActive(false);

        scenes[2].SetActive(true);
        sprite_mask.SetActive(false);
        while(!bm.battleing){yield return null;}
        yield return null;
        scenes[2].SetActive(false);

        while(bm.battleing){yield return null;}
        yield return new WaitForSeconds(0.5f);        

        scenes[3].SetActive(true);
        hole_hide(false);
        while(!Input.GetMouseButtonDown(0)){yield return null;}
        hole_hide(true);
        yield return null;
        scenes[3].SetActive(false);

        while(!bm.battleing){yield return null;}
        while(bm.battleing){yield return null;}
        yield return new WaitForSeconds(0.5f); 

        scenes[4].SetActive(true);
        hole_hide(false);
        while(!Input.GetMouseButtonDown(0)){yield return null;}
        hole_hide(true);
        yield return null;
        scenes[4].SetActive(false);

        while(!bm.battleing){yield return null;}
        while(bm.battleing){yield return null;}
        yield return new WaitForSeconds(0.5f); 

        scenes[5].SetActive(true);
        hole_hide(false);
        while(!Input.GetMouseButtonDown(0)){yield return null;}
        yield return null;
        hole_hide(true);
        scenes[5].SetActive(false);
        
        




        yield return null;
    }
    IEnumerator tutorial2(){
        while(bm.card_left_draw <= 0){yield return null;}
        sprite_mask.SetActive(true);

        while(bm.card_left_draw > 0){
            scenes[6].SetActive(Input.GetMouseButton(0));
            yield return null;
        }
        scenes[6].SetActive(false);

        scenes[7].SetActive(true);
        hole_hide(false);
        while(!Input.GetMouseButtonDown(0)){yield return null;}
        yield return null;
        hole_hide(true);
        scenes[7].SetActive(false);

        scenes[8].SetActive(true);
        hole_hide(false);
        while(bm.cardTouching == null){yield return null;}
        yield return null;
        hole_hide(true);
        scenes[8].SetActive(false);

        yield return null;
    }

    void hole_hide(bool bollen){
        bm.ui.battleStartButton.gameObject.SetActive(bollen);
        sprite_mask.SetActive(!bollen);
    }
}
