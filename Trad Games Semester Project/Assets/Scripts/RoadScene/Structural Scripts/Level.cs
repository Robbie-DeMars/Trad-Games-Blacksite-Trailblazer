using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField] CombatEncounter[] combatEncounters;
    [SerializeField] DialogueEncounter[] dialogueEncounters;
    [SerializeField] ChoiceEncounter[] choiceEncounters;

    int dialogueProgression = 0;
    int choiceProgression = 0;

    public CombatEncounter GetRandomCombatEncounter()
    {
        return combatEncounters[Random.Range(0, combatEncounters.Length - 1)];
    }

    public bool HasMoreChoiceEncounters()
    {
        if (choiceProgression < choiceEncounters.Length)
            return true;
        return false;
    }

    public ChoiceEncounter GetNextChoiceEncounter()
    {
        choiceProgression++;
        return choiceEncounters[choiceProgression - 1];
    }

}
