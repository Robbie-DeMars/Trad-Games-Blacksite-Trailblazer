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
    int level = 0;
    int Atk = 5;
    int Def = 2;
    int HP = 10;
    int maxHP = 10;




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
            //party[mainHeroPosition].GetComponent<Hero>().MaxHP = maxHP;
            //party[mainHeroPosition].GetComponent<Hero>().HP = HP;
            //party[mainHeroPosition].GetComponent<Hero>().Atk = Atk;
            //party[mainHeroPosition].GetComponent<Hero>().Def = Def;
        }
        
        return party;
    }

}
