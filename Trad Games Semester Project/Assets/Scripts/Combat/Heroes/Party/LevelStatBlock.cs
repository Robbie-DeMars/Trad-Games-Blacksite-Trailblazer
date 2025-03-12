using UnityEngine;

[CreateAssetMenu(fileName = "LevelStatBlock", menuName = "Scriptable Objects/PartyManagement/LevelStatBlock")]
public class LevelStatBlock : ScriptableObject
{
    [Header("Character Stats: Max HP, MaxAP, Atk, Def")]
    [SerializeField] public int Max_HP;
    [SerializeField] public int Max_AP;
    [SerializeField] public int Atk;
    [SerializeField] public int Def;
}
