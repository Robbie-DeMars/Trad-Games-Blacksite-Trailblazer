using UnityEngine;

[CreateAssetMenu(fileName = "Reigon", menuName = "Scriptable Objects/Reigon")]
public class Reigon : ScriptableObject
{
    [SerializeField] Level[] levels;


    public Level GetCurrentLevel()
    {
        return levels[PlayerPrefs.GetInt("level")];
    }
}
