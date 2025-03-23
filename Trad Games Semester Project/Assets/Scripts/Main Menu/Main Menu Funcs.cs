using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFuncs : MonoBehaviour
{
    [SerializeField] PartyState party;
    public void LoadCombat()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("MainLevel", 0);
        PlayerPrefs.SetInt("Side1Level", 0);
        PlayerPrefs.SetInt("Side2Level", 0);

        PlayerPrefs.SetInt("MainHP", party.mainLevels[party.GetMainLevel()].Max_HP);
        PlayerPrefs.SetInt("Side1HP", party.side1Levels[party.GetSide1Level()].Max_HP);
        PlayerPrefs.SetInt("Side2HP", party.side2Levels[party.GetSide2Level()].Max_HP);
        
        PlayerPrefs.SetInt("MainPos", 1);
        PlayerPrefs.SetInt("Side1Pos", 2);
        PlayerPrefs.SetInt("Side2Pos", 3);

        PlayerPrefs.Save();
        SceneManager.LoadScene("Combat");

    }

    public void LoadRoad()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("MainLevel", 0);
        PlayerPrefs.SetInt("Side1Level", 0);
        PlayerPrefs.SetInt("Side2Level", 0);

        PlayerPrefs.SetInt("MainHP", party.mainLevels[party.GetMainLevel()].Max_HP);
        PlayerPrefs.SetInt("Side1HP", party.side1Levels[party.GetSide1Level()].Max_HP);
        PlayerPrefs.SetInt("Side2HP", party.side2Levels[party.GetSide2Level()].Max_HP);

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
}
