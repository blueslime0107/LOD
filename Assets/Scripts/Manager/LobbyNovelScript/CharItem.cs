using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 전투카드창에 등장하는 캐릭터들의 항목
public class CharItem : MonoBehaviour
{
    public Lobby lobby;
    public Stage cur_stage;
    public Character cur_char;

    public TextMeshProUGUI health;
    public TextMeshProUGUI breaks;

    public int health_value;
    public List<int> breaks_value = new List<int>();

    public TextMeshProUGUI char_name;
    public Image character; 

    public CardUI[] cards = new CardUI[7];

    [SerializeField]TextMeshProUGUI selfCostText;

    [SerializeField]GameObject disableEdit;
    [SerializeField]GameObject healthEdit;
    [SerializeField]Button cardEdit;
    [SerializeField]TMP_InputField healthedit_inputField;

    [SerializeField]Image ableToFight_img;
    [SerializeField]Sprite[] fightSprite = new Sprite[2];
    [SerializeField]GameObject disabledPanel;

    [SerializeField] BattleCardManager battleCardManager;

    // 눌렀을때 카드덱 편집 창으로 가기
    public void OpenCardSelectMenu(){
        battleCardManager.SaveDisable();
        lobby.menuCard.selectingStage = cur_stage;
        lobby.menuCard.selectingChar = cur_char;
        lobby.OpenCardSelectMenu();
    }

    // 이름, 스프라이트, 카드덱들을 로딩하기
    public void UpdateStat(Character chars){
        healthEdit.SetActive(lobby.pc.debugBoolen);
        if(cur_stage == lobby.stage){
            cardEdit.interactable = lobby.pc.debugBoolen;
            disableEdit.SetActive(lobby.pc.debugBoolen);
        }
        else{
            cardEdit.interactable = !cur_stage.noEditChar;
        }
        cur_char = chars;
        health_value = chars.health;
        breaks_value = chars.breaks;
        char_name.text = chars.char_sprites.name_;
        character.sprite = chars.char_sprites.poses[0];

        health.text = health_value.ToString(); // 파괴수치
        breaks.text = "";
        foreach(int breaked in breaks_value){
            breaks.text += breaked.ToString();
            breaks.text += " ";
        }

        selfCostText.text = cur_stage.GetSelfPriceSum(cur_char).ToString();

        for(int i=0;i<chars.char_preCards.Length;i++){ // 카드덱
            if(chars.char_preCards[i] != null){
                cards[i].card = chars.char_preCards[i];
                cards[i].CardUpdate();
                cards[i].gameObject.SetActive(true);
            }
            else{
                cards[i].gameObject.SetActive(false);
            }
        }


        fightAbleRender();
    }

    public void changeFightAble(bool able){
        if(cur_char == null){return;}
        cur_char.battleAble = able;
        fightAbleRender();
        lobby.battleButtonCharLimit();

    }
    public void changeFightAbleToggle(){
        if(cur_char == null){return;}
        cur_char.battleAble = !cur_char.battleAble;
        fightAbleRender();
        lobby.battleButtonCharLimit();

    }
    public void EditHealth(){
        healthedit_inputField.gameObject.SetActive(true);
    }
    public void EditHealthInputField(){
        cur_char.health = int.Parse(healthedit_inputField.text);
        healthedit_inputField.gameObject.SetActive(false);
    }

    void fightAbleRender(){
        if(cur_stage == lobby.stage && !lobby.pc.debugBoolen){return;}
        ableToFight_img.sprite = (cur_char.battleAble) ? fightSprite[0] : fightSprite[1];
        disabledPanel.SetActive(!cur_char.battleAble);
    }

}
