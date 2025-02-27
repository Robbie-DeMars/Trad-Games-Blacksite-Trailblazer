using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFuncs : MonoBehaviour
{
    [SerializeField] PartyState party;
    public void LoadCombat()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("MainHP", party.mainStats[0]);
        PlayerPrefs.SetInt("Side1HP", party.side1Stats[0]);
        PlayerPrefs.SetInt("Side2HP", party.side2Stats[0]);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Combat");

    }

    public void LoadRoad()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("MainHP", party.mainStats[0]);
        PlayerPrefs.SetInt("Side1HP", party.side1Stats[0]);
        PlayerPrefs.SetInt("Side2HP", party.side2Stats[0]);

        //Set Level to 1
        PlayerPrefs.SetInt("level", 1);

        PlayerPrefs.Save();
        SceneManager.LoadScene("RoadScene");
    }
}
