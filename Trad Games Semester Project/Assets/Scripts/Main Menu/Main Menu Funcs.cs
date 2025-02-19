using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFuncs : MonoBehaviour
{
    public void LoadCombat()
    {
        SceneManager.LoadScene("Combat");
    }

    public void LoadRoad()
    {
        SceneManager.LoadScene("RoadScene");
    }
}
