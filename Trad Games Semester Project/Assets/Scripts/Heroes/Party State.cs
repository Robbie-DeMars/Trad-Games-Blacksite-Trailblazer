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
    int mainHeroIndex = 0;
    [Header("Main Character Stats: HP, Max HP, MaxAP, Atk, Def, Position")]
    [SerializeField] public int[] mainStats;

    //Side Character 1
    bool hasSide1 = true;
    int side1Index = 1;
    [Header("Main Character Stats: HP, Max HP, MaxAP, Atk, Def, Position")]
    [SerializeField] public int[] side1Stats;

    //Side Character 2
    bool hasSide2 = true;
    int side2Index = 2;
    [Header("Main Character Stats: HP, Max HP, MaxAP, Atk, Def, Position")]
    [SerializeField] public int[] side2Stats;




    /// <summary>
    /// Builds the current party in a readable manner for the combat model.
    /// </summary>
    /// <returns> Array of all party members in their current positions. </returns>
    public GameObject[] GetCurrentPartyForCombat()
    {
        GameObject[] party = new GameObject[possiblePartySize];
        if (hasMainHero && mainStats[5] != -1)
        {
            party[mainStats[5]] = characterPrefabs[mainHeroIndex];
            Debug.Log("Added main to " + mainStats[5]);
        }
        if (hasSide1 && side1Stats[5] != -1)
        {
            party[side1Stats[5]] = characterPrefabs[side1Index];
            Debug.Log("Added side1 to " + side1Stats[5]);
        }
        if (hasSide2 && side2Stats[5] != -1)
        {
            party[side2Stats[5]] = characterPrefabs[side2Index];
            Debug.Log("Added side2 to " + side2Stats[5]);
        }

        return party;
    }

}
