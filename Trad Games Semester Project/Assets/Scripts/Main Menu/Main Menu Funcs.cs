using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFuncs : MonoBehaviour
{
    [SerializeField] PartyState party;
    
    /// <summary>
    /// Jump straight to level 1 combat
    /// </summary>
    public void LoadCombat()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("CanContinue", 1);

        PlayerPrefs.SetInt("MainLevel", 0);
        PlayerPrefs.SetInt("Side1Level", 0);
        PlayerPrefs.SetInt("Side2Level", 0);

        PlayerPrefs.SetFloat("MainHP", party.mainLevels[party.GetMainLevel()].Max_HP);
        PlayerPrefs.SetFloat("Side1HP", party.side1Levels[party.GetSide1Level()].Max_HP);
        PlayerPrefs.SetFloat("Side2HP", party.side2Levels[party.GetSide2Level()].Max_HP);
        
        PlayerPrefs.SetInt("MainPos", 1);
        PlayerPrefs.SetInt("Side1Pos", 2);
        PlayerPrefs.SetInt("Side2Pos", 3);

        //Set Level to 1 - first level
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("visitedLevel1", 0);
        PlayerPrefs.SetInt("visitedLevel2", 0);
        PlayerPrefs.SetInt("visitedLevel3", 0);

        PlayerPrefs.Save();
        SceneManager.LoadScene("Combat");

    }

    /// <summary>
    /// Start a new game
    /// </summary>
    public void LoadRoad()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("CanContinue", 1);

        PlayerPrefs.SetInt("MainLevel", 0);
        PlayerPrefs.SetInt("Side1Level", 0);
        PlayerPrefs.SetInt("Side2Level", 0);

        PlayerPrefs.SetFloat("MainHP", party.mainLevels[party.GetMainLevel()].Max_HP);
        PlayerPrefs.SetFloat("Side1HP", party.side1Levels[party.GetSide1Level()].Max_HP);
        PlayerPrefs.SetFloat("Side2HP", party.side2Levels[party.GetSide2Level()].Max_HP);

        PlayerPrefs.SetInt("MainPos", 1);
        PlayerPrefs.SetInt("Side1Pos", 2);
        PlayerPrefs.SetInt("Side2Pos", 3);

        //Set Level to 0 - tutorial level
        PlayerPrefs.SetInt("level", 0);
        PlayerPrefs.SetInt("visitedLevel1", 0);
        PlayerPrefs.SetInt("visitedLevel2", 0);
        PlayerPrefs.SetInt("visitedLevel3", 0);

        PlayerPrefs.Save();
        SceneManager.LoadScene("RoadScene");
    }


    /// <summary>
    /// Continue from start of level.
    /// </summary>
    public void ContinueRoad()
    {
        if (PlayerPrefs.GetInt("CanContinue") != 1)
        {
            LoadRoad();
            return;
        }

        //Heal party so people dont get too stuck
        PlayerPrefs.SetFloat("MainHP", party.mainLevels[party.GetMainLevel()].Max_HP);
        PlayerPrefs.SetFloat("Side1HP", party.side1Levels[party.GetSide1Level()].Max_HP);
        PlayerPrefs.SetFloat("Side2HP", party.side2Levels[party.GetSide2Level()].Max_HP);

        PlayerPrefs.Save();
        SceneManager.LoadScene("RoadScene");
    }
}
