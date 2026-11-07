using UnityEngine;

[CreateAssetMenu(fileName = "Choice", menuName = "Scriptable Objects/Choices/Choice")]
public abstract class Choice : ScriptableObject
{
    public abstract void Outcome();
}
