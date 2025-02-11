using Mono.Cecil.Cil;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyState", menuName = "Scriptable Objects/PartyState")]
public class PartyState : ScriptableObject
{
    //Defines max party size
    int possiblePartySize = 5;
    
    //Defines main character
    [SerializeField] GameObject[] characterPrefabs;
    bool hasMainHero = true;
    int mainHeroPosition = 0;
    int mainHeroIndex = 0;

    /// <summary>
    /// Builds the current party in a readable manner for the combat model.
    /// </summary>
    /// <returns> Array of all party members in their current positions. </returns>
    public GameObject[] getCurrentPartyForCombat()
    {
        GameObject[] party = new GameObject[possiblePartySize];
        if (hasMainHero && mainHeroPosition != -1)
            party[mainHeroPosition] = characterPrefabs[mainHeroIndex];
        
        return party;
    }

}
