using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDice : MonoBehaviour
{
    public BattleManager battleManager;

    public DiceIcon diceOBJ;
    public Player player;
    public LineRenderer lineRender;

    public ParticleSystem left_break;
    public ParticleSystem right_break;

    public bool spinging;

    private void OnEnable() {
        left_break.Stop();
        right_break.Stop();
        spinging = true;
        StartCoroutine("Spining");
        StartCoroutine("SpiningSound");
        
    }


    IEnumerator Spining(){
        diceOBJ.updateDice(0);
        while(true){
            transform.Rotate(0,0,120*60*Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator SpiningSound(){
        while(true){
            battleManager.sdm.Play("RollDice");
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DamageReset(){
        diceOBJ.updateDice(0);
    }

    public void DamageUpdate(){
        if(spinging){
            battleManager.sdm.Play("Snap");
            StopAllCoroutines();          
            transform.localEulerAngles = Vector3.zero;
            spinging = false;
        }
        diceOBJ.updateDice(Mathf.Abs(battleManager.battleCaculate.damage.value));
    }
}
