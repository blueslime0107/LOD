using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class YouGetACard : MonoBehaviour
{
    [SerializeField] CardPrefap card;
    public List<CardAbility> cardAbility;

    [SerializeField] Animator animator;
    
    [SerializeField] Image card_img;
    [SerializeField] TextMeshProUGUI card_name;
    [SerializeField] TextMeshProUGUI card_comment;
    [SerializeField] TextMeshProUGUI ability_message;
    [SerializeField] TextMeshProUGUI story_message;

    bool textwriting = true;


    IEnumerator cardWriting() {
        if(Input.GetMouseButtonDown(0)){yield return null;}
        Debug.Log("path");
        while(true){
        if(Input.GetMouseButtonDown(0)){
            if(textwriting){
                Debug.Log("textwrigint_skip");
                textwriting = false; 
                animator.Play("CardGet",0,1.0f);
                StopCoroutine("TypeWrite");
                card_name.text = card.ability.name;
                card_comment.text = card.ability.message ;
                ability_message.text = card.ability.ability_message ;
                story_message.text = card.ability.story_message; 
                
                yield return null;
            }
            else if(cardAbility.Count > 0){
                GeTheCard();
                break;
            }
            else{
                animator.SetTrigger("AfterCardGet");
                break;
            }
            
        }
        yield return null;
        }
    }

    public void GeTheCard(){
        card_name.text = "";
        card_comment.text = "";
        ability_message.text ="";
        story_message.text = "";
        textwriting = true;
        Debug.Log(cardAbility[0].name);
        card.loaded = false;
        card.cardUpdate(cardAbility[0]);
        card_img.sprite = cardAbility[0].illust;
        Debug.Log(card.ability.name);
        StartCoroutine("cardWriting");
        animator.Play("CardGet",0,0f);
        cardAbility.RemoveAt(0);
    }

    public void WriteCard(){
        if(!textwriting){return;}
        StartCoroutine(TypeWrite(card.ability.name,1,0.1f));
        StartCoroutine(TypeWrite(card.ability.message,2,0.1f));
    }
    public void WriteAbility(){
        if(!textwriting){return;}
        StartCoroutine(TypeWrite(card.ability.ability_message,3,0.02f));
        StartCoroutine(TypeWrite(card.ability.story_message,4,0.02f));
    }

    IEnumerator TypeWrite(string text,int index,float delay=0.05f){
        string newString = text;
        for(int i=0;i<=newString.Length;i++){
            try{
            switch(index){
                case 1:
                    card_name.text = newString.Substring(0,i); break;
                case 2:
                    card_comment.text = newString.Substring(0,i); break;
                case 3:
                    ability_message.text = newString.Substring(0,i); 
                    if(ability_message.text.Equals(newString)){textwriting = false;}
                    break;
                case 4:
                    story_message.text = newString.Substring(0,i); break;
            }
            
            }
            catch{
                break;
            }
            yield return new WaitForSeconds(delay);
        }
    }

}
