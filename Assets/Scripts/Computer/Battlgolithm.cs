using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlgolithm : ScriptableObject
{
    public virtual void isDiceSelect(List<Dice> my_dice, List<Player> my_players){}
    public virtual void isBattleing(List<Player> my_players, List<Player> ene_players){}
}
