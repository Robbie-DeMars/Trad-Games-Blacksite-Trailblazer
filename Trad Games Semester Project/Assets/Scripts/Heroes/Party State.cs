using Mono.Cecil.Cil;
using UnityEngine;

[CreateAssetMenu(fileName = "PartyState", menuName = "Scriptable Objects/PartyState")]
public class PartyState : ScriptableObject
{
    //Defines max party size
    int possiblePartySize = 5;
    
    //Defines main character
    [SerializeField] GameObject[] characterPrefabs;
    //Main Character
    bool hasMainHero = true;
    int mainHeroPosition = 2;
    int mainHeroIndex = 0;

    //Side Character 1
    bool hasSide1 = true;
    int Side1Position = 1;
    int Side1Index = 1;

    //Side Character 2
    bool hasSide2 = true;
    int Side2Position = 3;
    int Side2Index = 2;




    /// <summary>
    /// Builds the current party in a readable manner for the combat model.
    /// </summary>
    /// <returns> Array of all party members in their current positions. </returns>
    public GameObject[] GetCurrentPartyForCombat()
    {
        GameObject[] party = new GameObject[possiblePartySize];
        if (hasMainHero && mainHeroPosition != -1)
        {
            party[mainHeroPosition] = characterPrefabs[mainHeroIndex];
            Debug.Log("Added main to " + mainHeroPosition);
        }
        if (hasSide1 && Side1Position != -1)
        {
            party[Side1Position] = characterPrefabs[Side1Index];
            Debug.Log("Added side1 to " + Side1Position);
        }
        if (hasSide2 && Side2Position != -1)
        {
            party[Side2Position] = characterPrefabs[Side2Index];
            Debug.Log("Added side2 to " + Side2Position);
        }

        return party;
    }

}
