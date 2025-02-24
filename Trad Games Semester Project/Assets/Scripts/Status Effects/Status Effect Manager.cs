using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectManager", menuName = "Scriptable Objects/StatusEffectManager")]
public class StatusEffectManager : ScriptableObject
{
    [SerializeField] AgnosticEffect[] effects;

    //Indexxed at 0
    public AgnosticEffect Defend()
    {
        return Instantiate(effects[0]);
    }
}
