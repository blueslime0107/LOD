using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage{
    public int value;
    public void setDamage(int damage){
        value = damage; 
    }
    public void adDamage(int damage){
        value += damage; 
        if(value <= 0){value = 0;}
    }
}

public class CardGetSituation{
    public List<CardAbility> specialCards = new List<CardAbility>();
}

[System.Serializable]
public class Team{
    public string text;
    public List<Player> players;
    public int carddraw;
    public BattleAI battleAI;
    public List<CardGetSituation> cardGetSituations = new List<CardGetSituation>();
    public int diceRollGague;

    public int getDiceMaMin(bool max=true){
        int newint = 0;

        foreach(Player player in players){
            if(player.dice < newint && max){continue;}
            if(player.dice > newint && !max){continue;}
            newint = player.dice;
        }
        return newint;
        
    }

    public int getDiceAverage(){
        int newint = 0;
        foreach(Player player in players){
            newint += player.dice;
        }
        return newint;
        
    }

    public int getHealthAver(){
        int newint = 0;

        foreach(Player player in players){
            newint += player.health;
        }


        return newint/players.Count;
    }

    public Player getCardPlayer(bool max=true){
        Player newint = players[0];
        foreach(Player player in players){
            if(player.cards.Count < newint.cards.Count && max){continue;}
            if(player.cards.Count > newint.cards.Count && !max){continue;}
            newint = player;
        }
        return newint;
    }

    public List<CardPack> getAllCard(){
        List<CardPack> newCardPAck = new List<CardPack>();
        foreach(Player player in players){
        newCardPAck.AddRange(player.cards);

        }
        return newCardPAck;
    }
}

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;
    public BattleManager battleManager;
    public SceneMove sceneMove;
    public CameraCtrl main_camera_ctrl;
    public DataBase dataBase;

    public StageManager sm;
    [SerializeField]public GameObject parent_back;
    

    public Stage debugPlayerStage;
    public Stage debugStage;

    [SerializeField] Player[] leftPlayers;
    [SerializeField] Player[] rightPlayers;

    [SerializeField] GameObject[] backGrounds;

    public Tutorial tutorial;
    
    void Reseting(){

        sm = FindObjectOfType<StageManager>();
        sm.db = dataBase;

        if(sm == null){
            GameObject smObj = new GameObject();
            sm = smObj.AddComponent<StageManager>() as StageManager;

            sm.play_stage = debugStage;
            sm.player_battleCard = debugPlayerStage;
            sm.player_cardDic = battleManager.cards;
        }

        soundManager = FindObjectOfType<SoundManager>();
        battleManager.sdm = soundManager;
        

        if(sm != null)
        {
            int battleable = 0;
            for(int i=0;i<sm.player_battleCard.characters.Length;i++){
            if(sm.player_battleCard.characters[i] == null){continue;}
            if(!sm.player_battleCard.characters[i].battleAble){battleable++; continue;}
            leftPlayers[i-battleable].battleManager = battleManager;
            leftPlayers[i-battleable].dice_Indi.battleManager = battleManager;
            leftPlayers[i-battleable].dice_Indi.battleCaculate = battleManager.battleCaculate;
            leftPlayers[i-battleable].dice_Indi.lineRender = battleManager.lineRender;
            leftPlayers[i-battleable].gameObject.SetActive(true);
            Character chars = sm.player_battleCard.characters[i];
            if(chars.char_sprites.atkEffect)
            leftPlayers[i-battleable].atkEffect = SpawnEffect(chars.char_sprites.atkEffect,leftPlayers[i-battleable]);
            leftPlayers[i-battleable].character = chars;
            leftPlayers[i-battleable].health = chars.health;
            leftPlayers[i-battleable].max_health = chars.health;
            leftPlayers[i-battleable].breakCount = new List<int>(chars.breaks);
            leftPlayers[i-battleable].poses = chars.char_sprites.poses;
            leftPlayers[i-battleable].farAtt = chars.char_sprites.farAtk;
            leftPlayers[i-battleable].UpdateHp();
            leftPlayers[i-battleable].pre_cards.AddRange(chars.char_preCards);
            leftPlayers[i-battleable].team = battleManager.left_team;
            if(chars.specialAtk){leftPlayers[i-battleable].specialAtk = Instantiate(chars.specialAtk).GetComponent<SpecialAtk>();
            leftPlayers[i-battleable].specialAtk.gameObject.SetActive(false);
            leftPlayers[i-battleable].specialAtk.transform.Rotate(Vector3.down * 180);
            leftPlayers[i-battleable].specialAtk.sm = soundManager;
            }

        }
        battleable = 0;
        for(int i=0;i<sm.play_stage.characters.Length;i++){
            if(sm.play_stage.characters[i] == null){continue;}
            if(!sm.play_stage.characters[i].battleAble){battleable++; continue;}
                rightPlayers[i-battleable].battleManager = battleManager;
                rightPlayers[i-battleable].dice_Indi.battleManager = battleManager;
                rightPlayers[i-battleable].dice_Indi.battleCaculate = battleManager.battleCaculate;
                rightPlayers[i-battleable].dice_Indi.lineRender = battleManager.lineRender;
                rightPlayers[i-battleable].gameObject.SetActive(true);
                Character chars = sm.play_stage.characters[i];
                if(chars.char_sprites.atkEffect)
                rightPlayers[i-battleable].atkEffect = SpawnEffect(chars.char_sprites.atkEffect,rightPlayers[i-battleable]);
                rightPlayers[i-battleable].character = chars;
                rightPlayers[i-battleable].health = chars.health;
                rightPlayers[i-battleable].max_health = chars.health;
                rightPlayers[i-battleable].breakCount = new List<int>(chars.breaks);
                rightPlayers[i-battleable].poses = chars.char_sprites.poses;
                rightPlayers[i-battleable].farAtt = chars.char_sprites.farAtk;
                rightPlayers[i-battleable].UpdateHp();
                rightPlayers[i-battleable].pre_cards.AddRange(chars.char_preCards);
                rightPlayers[i-battleable].team = battleManager.right_team;

                if(chars.specialAtk){rightPlayers[i-battleable].specialAtk = Instantiate(chars.specialAtk).GetComponent<SpecialAtk>();
                rightPlayers[i-battleable].specialAtk.gameObject.SetActive(false);
                rightPlayers[i-battleable].specialAtk.sm = soundManager;
                }
                
            
        }

        battleManager.cards = sm.player_cardDic;

        if(sm.play_stage.tutorialLine > 0){
            battleManager.tutorial.SetActive(true);
        }
        
        }
        if(sm.play_stage.custom_stage){
            GameObject obj = Instantiate(sm.play_stage.custom_stage);
            obj.transform.SetParent(parent_back.transform,false);   
            soundManager.PlayBGM(sm.play_stage.custom_BGM);
        }
        else{
            GameObject obj = Instantiate(backGrounds[sm.preFloor]);
            obj.transform.SetParent(parent_back.transform,false);   
            soundManager.PlayBGM(sm.floor.battleBGM);
        }



    }

    void Start(){
        Reseting();
        tutorial.TutorialStart();
        battleManager.Battle();
    }

    public Vector3 SetVector3z(Vector3 pre_vec,float z){
        pre_vec.Set(pre_vec.x,pre_vec.y,z);
        return pre_vec;

    }

    public CardEffect SpawnEffect(GameObject effe,Player player){
        Vector3 copyVec = effe.transform.position;
        Vector3 copyScale = effe.transform.localScale;
        GameObject card = Instantiate(effe); 
        card.SetActive(false);
        CardEffect card_effect = card.GetComponent<CardEffect>();
        card_effect.battleManager = battleManager;
        card.transform.SetParent(player.gameObject.transform);
        card.transform.localPosition = copyVec;
        card.transform.localScale = copyScale;
        // 오른쪽 팀이면 좌우반전
        if(player.gameObject.tag == "PlayerTeam2"){ 
            card.transform.eulerAngles += Vector3.up*180f;  
            Vector3 pre = card.transform.localPosition;
            pre.x *= -1; 
            card.transform.localPosition = pre;
        }
        return card_effect;
    }

    public CardPack PreSetting(CardPack card, Player play){
        
        CardPack c = card;

        c.count = c.ability.pre_count;
        c.tained = c.ability.tained;
        c.player = play; // 이 카드를 가진 캐릭터의 변수를 가져올 수 있음.
        c.card_id = c.ability.card_id; // 카드 ID 다른 카드에서 특정 카드에 대한 이벤트를 위해 필요함
        c.illust = c.ability.illust;
        c.name_ = c.ability.name;
        c.message = c.ability.message;
        c.ability_message = c.ability.ability_message;
        // 이펙트를 현재 플레이어에 복사해둠
        foreach(GameObject effe in c.ability.effect){
            c.effect.Add(SpawnEffect(effe,c.player));

        }
        if(c.ability.diceLink != null){
            GameObject car = Instantiate(c.ability.diceLink);
            car.transform.SetParent(c.player.gameObject.transform, false);
            c.diceLink = car.GetComponent<LineRenderer>();
        }
        return c;
    }

    
}
