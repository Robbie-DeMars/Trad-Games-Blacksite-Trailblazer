using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenFuncs : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
