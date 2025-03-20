using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField] int maxCombatEncounters;
    [SerializeField] int minCombatEncounters;
    [SerializeField] CombatEncounter[] combatEncounters;
    [SerializeField] DialogueEncounter[] dialogueEncounters;
    [SerializeField] ChoiceEncounter[] choiceEncounters;


    /// <summary>
    /// Returns a random combat encounter from this reigon.
    /// </summary>
    /// <returns></returns>
    public CombatEncounter GetRandomCombatEncounter(Singleton s)
    {
        s.combats++;
        return combatEncounters[Random.Range(0, combatEncounters.Length)];
    }

    /// <summary>
    /// Returns the first combat encounter with the passed string ID.
    /// Returns the first combat encounter if no such ID exists.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public CombatEncounter GetCombatEncounterViaID(string ID)
    {
        for (int x = 0; x < combatEncounters.Length; x++)
        {
            if (combatEncounters[x].Equals(ID))
                return combatEncounters[x];
        }
        return combatEncounters[0];
    }

    /// <summary>
    /// Returns true if there are more choice encounters, else false
    /// </summary>
    /// <returns></returns>
    public bool HasMoreChoiceEncounters(Singleton s)
    {
        return s.choices < choiceEncounters.Length;
    }

    /// <summary>
    /// Returns the next choice encounter, or starts from the beginning if it runs out.
    /// </summary>
    /// <returns></returns>
    public ChoiceEncounter GetNextChoiceEncounter(Singleton s)
    {
        
        int choice = s.choices;
        s.choices++;
        if (!HasMoreChoiceEncounters(s))
            choice = 0;
        return choiceEncounters[choice];
    }

    /// <summary>
    /// Returns true if there are more dialogue encounters, else false
    /// </summary>
    /// <returns></returns>
    public bool HasMoreDialogueEncounters(Singleton s)
    {
        return s.dialogues < dialogueEncounters.Length;
    }

    /// <summary>
    /// Returns the next dialogue encounter, or starts from the beginning if it runs out.
    /// </summary>
    /// <returns></returns>
    public DialogueEncounter GetNextDialogueEncounter(Singleton s)
    {
        
        int dialogue = s.dialogues;
        s.dialogues++;
        if (!HasMoreDialogueEncounters(s))
            dialogue = 0;
        return dialogueEncounters[dialogue];
    }

    /// <summary>
    /// Returns true if there are more dialogue encounters, else false
    /// </summary>
    /// <returns></returns>
    public bool HasMoreCombatEncounters(Singleton s)
    {
        int roll = Random.Range(minCombatEncounters, maxCombatEncounters + 1);
        return s.combats < roll;
    }
}
