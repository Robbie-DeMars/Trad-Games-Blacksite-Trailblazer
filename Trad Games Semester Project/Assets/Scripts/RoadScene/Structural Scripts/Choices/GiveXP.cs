using UnityEngine;

[CreateAssetMenu(fileName = "GiveXP", menuName = "Scriptable Objects/Choices/GiveXP")]
public class GiveXP : Choice
{
    public override void Outcome()
    {
        Debug.Log("MONEYYYYYYYYYYYYYY");
    }
}
