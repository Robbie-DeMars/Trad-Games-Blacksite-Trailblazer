using UnityEngine;

[CreateAssetMenu(fileName = "CombatEncounter", menuName = "Scriptable Objects/CombatEncounter")]
public class CombatEncounter : ScriptableObject
{
    int maxEncounterSize = 5;
    [SerializeField] private int XP;
    [SerializeField] GameObject[] enemies = new GameObject[5];

    public string ID = "Test Encounter";

    /// <summary>
    /// Returns the list of enemies representing the layout of this combat encounter.
    /// </summary>
    /// <returns></returns>
    public GameObject[] GetEnemies()
    {
        GameObject[] returnedEnemies = new GameObject[maxEncounterSize];
        for (int x = 0; x < returnedEnemies.Length; x++)
        {
            returnedEnemies[x] = enemies[x];
        }
        return returnedEnemies; 
    }

    /// <summary>
    /// Rewards the player with the specified XP ammount in this enounter.
    /// </summary>
    public void AwardXP()
    {
        PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + XP);
        PlayerPrefs.Save();
    }
}
