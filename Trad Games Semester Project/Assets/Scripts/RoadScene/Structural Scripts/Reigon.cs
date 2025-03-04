using UnityEngine;

[CreateAssetMenu(fileName = "Reigon", menuName = "Scriptable Objects/Reigon")]
public class Reigon : ScriptableObject
{
    [SerializeField] Level[] levels;

    /// <summary>
    /// Returns the current saved level based on PlayerPrefs
    /// </summary>
    /// <returns></returns>
    public Level GetCurrentLevel()
    {
        return levels[PlayerPrefs.GetInt("level")];
    }

    /// <summary>
    /// Returns true if successfully loaded next level, and false if not.
    /// False implies there are no more levels left
    /// </summary>
    /// <returns></returns>
    public void LoadLevel(int level)
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();
    }
}