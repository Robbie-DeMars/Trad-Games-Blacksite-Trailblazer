using UnityEditor.TerrainTools;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect", menuName = "Scriptable Objects/StatusEffect")]
public class StatusEffect : ScriptableObject
{
    public bool stackable = true;
    public string ID = "";
}
