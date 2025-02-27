using UnityEngine;

[CreateAssetMenu(fileName = "Reigon", menuName = "Scriptable Objects/Reigon")]
public class Reigon : ScriptableObject
{
    [SerializeField] Level level1;
    [SerializeField] Level level2;
    [SerializeField] Level level3;

    public Level GetCurrentLevel()
    {
        if (PlayerPrefs.GetInt("level") == 1)
        {
            return level1;
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            return level2;
        }
        else if (PlayerPrefs.GetInt("level") == 3)
        {
            return level3;
        }
        return level1;
    }

}
