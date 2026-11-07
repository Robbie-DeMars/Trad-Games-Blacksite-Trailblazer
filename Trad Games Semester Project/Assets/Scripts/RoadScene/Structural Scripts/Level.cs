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
    int roll;
    [SerializeField] public bool ordered;
    [SerializeField] Encounter[] encounterOrder;

    /// <summary>
    /// Sets the number of combat encounters in this reigon between the min and max randomly.
    /// If this level is ordered, instead sets the max value
    /// </summary>
    public void RollCombat()
    {
        if (!ordered)
            roll = Random.Range(minCombatEncounters, maxCombatEncounters + 1);
        else
            roll = maxCombatEncounters;
    }
    /// <summary>
    /// Returns the next encounter if this level is ordered
    /// </summary>
    /// <returns></returns>
    public Encounter GetNextEncounter(Singleton s)
    {
        if (!ordered)
            return null;
        //Find which encounter type it is
        if (encounterOrder[s.combats + s.dialogues + s.choices] is CombatEncounter)
        {
            s.combats++;
            return encounterOrder[s.combats + s.dialogues + s.choices - 1];
        }
        else if (encounterOrder[s.combats + s.dialogues + s.choices] is DialogueEncounter)
        {
            s.dialogues++;
            return encounterOrder[s.combats + s.dialogues + s.choices - 1];
        }
        else if (encounterOrder[s.combats + s.dialogues + s.choices] is ChoiceEncounter)
        {
            s.choices++;
            return encounterOrder[s.combats + s.dialogues + s.choices - 1];
        }
        else
        {
            return null;
        }
    }

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
            if (combatEncounters[x].ID.Equals(ID))
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
        if (!HasMoreChoiceEncounters(s))
            choice = 0;
        s.choices++;
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
        if (!HasMoreDialogueEncounters(s))
            dialogue = 0;
        s.dialogues++;
        return dialogueEncounters[dialogue];
    }

    /// <summary>
    /// Returns true if there are more dialogue encounters, else false
    /// </summary>
    /// <returns></returns>
    public bool HasMoreCombatEncounters(Singleton s)
    {
        return s.combats < roll;
    }

    /// <summary>
    /// Returns the remaining number of encounters in this level.
    /// </summary>
    /// <returns></returns>
    public int GetRemainingEncounters(Singleton s)
    {
        return roll + choiceEncounters.Length + dialogueEncounters.Length - s.combats - s.choices - s.dialogues;
    }

    /// <summary>
    /// Returns the total number of encounters in this level.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public int GetTotalEncounters(Singleton s)
    {
        return roll + choiceEncounters.Length + dialogueEncounters.Length;
    }
}
