using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StageTool/Charpack", order = 2)]
public class CharPack : ScriptableObject
{
    public string name_;
    public Sprite[] poses = new Sprite[5];
    public bool farAtk = false;
    public GameObject atkEffect;
    public string atkSound;
}
