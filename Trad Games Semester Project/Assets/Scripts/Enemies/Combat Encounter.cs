using UnityEngine;

[CreateAssetMenu(fileName = "CombatEncounter", menuName = "Scriptable Objects/CombatEncounter")]
public class CombatEncounter : ScriptableObject
{
    int maxEncounterSize = 5;

    [SerializeField] GameObject[] enemies = new GameObject[5];

    public GameObject[] GetEnemies()
    {
        GameObject[] returnedEnemies = new GameObject[maxEncounterSize];
        for (int x = 0; x < returnedEnemies.Length; x++)
        {
            returnedEnemies[x] = enemies[x];
        }
        return returnedEnemies;
    }
}
