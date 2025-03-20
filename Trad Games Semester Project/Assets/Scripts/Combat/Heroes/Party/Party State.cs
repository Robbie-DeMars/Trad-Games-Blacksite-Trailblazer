using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PartyState", menuName = "Scriptable Objects/PartyManagement/PartyState")]
public class PartyState : ScriptableObject
{
    //Defines max party size
    int possiblePartySize = 5;
    
    //Defines main character
    [SerializeField] GameObject[] characterPrefabs;
    //Main Character
    public bool hasMainHero = true;
    int mainHeroIndex = 0;
    [Header("Main Character Levels:")]
    [SerializeField] public LevelStatBlock[] mainLevels;

    //Side Character 1
    public bool hasSide1 = true;
    int side1Index = 1;
    [Header("Side1 Levels:")]
    [SerializeField] public LevelStatBlock[] side1Levels;

    //Side Character 2
    public bool hasSide2 = true;
    int side2Index = 2;
    [Header("Side2 Levels:")]
    [SerializeField] public LevelStatBlock[] side2Levels;

    /// <summary>
    /// Returns the level integer of the main character, starting at 0.
    /// </summary>
    /// <returns></returns>
    public int GetMainLevel()
    {
        return PlayerPrefs.GetInt("MainLevel");
    }

    /// <summary>
    /// Returns the level integer of side1, starting at 0.
    /// </summary>
    /// <returns></returns>
    public int GetSide1Level()
    {
        return PlayerPrefs.GetInt("Side1Level");
    }

    /// <summary>
    /// Returns the level integer of side2, starting at 0.
    /// </summary>
    /// <returns></returns>
    public int GetSide2Level()
    {
        return PlayerPrefs.GetInt("Side2Level");
    }

    /// <summary>
    /// Returns the combat position for the main character
    /// </summary>
    /// <returns></returns>
    public int GetMainPosition()
    {
        return PlayerPrefs.GetInt("MainPos");
    }

    /// <summary>
    /// Returns the combat position of side1
    /// </summary>
    /// <returns></returns>
    public int GetSide1Position()
    {
        return PlayerPrefs.GetInt("Side1Pos");
    }

    /// <summary>
    /// Returns the combat position of side2
    /// </summary>
    /// <returns></returns>
    public int GetSide2Position()
    {
        return PlayerPrefs.GetInt("Side2Pos");
    }

    /// <summary>
    /// Sets the combat position of the main character, -1 for offboard 
    /// </summary>
    public void SetMainPosition(int position)
    {
        PlayerPrefs.SetInt("MainPos", position);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Sets the combat position of the side1 character, -1 for offboard
    /// </summary>
    public void SetSide1Position(int position)
    {
        PlayerPrefs.SetInt("Side1Pos", position);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Sets the combat position of the side2 character, -1 for offboard
    /// </summary>
    public void SetSide2Position(int position)
    {
        PlayerPrefs.SetInt("Side2Pos", position);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Builds the current party in a readable manner for the combat model.
    /// </summary>
    /// <returns> Array of all party members in their current positions. </returns>
    public GameObject[] GetCurrentPartyForCombat()
    {
        GameObject[] party = new GameObject[possiblePartySize];
        if (hasMainHero && GetMainPosition() != -1)
        {
            party[GetMainPosition()] = characterPrefabs[mainHeroIndex];
        }
        if (hasSide1 && GetSide1Position() != -1)
        {
            party[GetSide1Position()] = characterPrefabs[side1Index];
        }
        if (hasSide2 && GetSide2Position() != -1)
        {
            party[GetSide2Position()] = characterPrefabs[side2Index];
        }

        return party;
    }

    /// <summary>
    /// Heals all enemies who are in the off board, preferably at the end of battle
    /// </summary>
    public void HealOffBoardEnemies()
    {
        GameObject[] party = new GameObject[possiblePartySize];
        if (hasMainHero && GetMainPosition() == -1)
        {
            characterPrefabs[mainHeroIndex].GetComponent<HeroBehavior>().ReplenishHP();
        }
        if (hasSide1 && GetSide1Position() == -1)
        {
            characterPrefabs[side1Index].GetComponent<HeroBehavior>().ReplenishHP();
        }
        if (hasSide2 && GetSide2Position() == -1)
        {
            characterPrefabs[side2Index].GetComponent<HeroBehavior>().ReplenishHP();
        }
    }

}
