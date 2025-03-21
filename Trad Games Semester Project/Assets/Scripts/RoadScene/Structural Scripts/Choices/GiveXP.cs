using UnityEngine;

[CreateAssetMenu(fileName = "GiveXP", menuName = "Scriptable Objects/Choices/GiveXP")]
public class GiveXP : Choice
{
    [SerializeField] int ammount;
    public override void Outcome()
    {
        PlayerPrefs.SetInt("XP", PlayerPrefs.GetInt("XP") + ammount);
        PlayerPrefs.Save();
    }
}
