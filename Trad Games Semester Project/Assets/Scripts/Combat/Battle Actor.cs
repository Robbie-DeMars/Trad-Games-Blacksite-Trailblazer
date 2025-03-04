using UnityEngine;

[CreateAssetMenu(fileName = "BattleActor", menuName = "Scriptable Objects/BattleActors/BattleActor")]
public class BattleActor : ScriptableObject
{
    [SerializeField] public int MaxHP = 10;
    public float HP = 10;
    [SerializeField] public float Atk = 5;
    [SerializeField] public float Def = 2;
}
