using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField] CombatEncounter[] encountersInReigon;

    /// <summary>
    /// Returns a combat encounter at random from the encounters provided.
    /// </summary>
    /// <returns> Combat Encounter </returns>
    public CombatEncounter GetRandomCombatEncounter()
    {
        int encounter = Random.Range(0, encountersInReigon.Length);
        return encountersInReigon[encounter];
    }

}
