using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Level")]
public class Level : ScriptableObject
{
    [SerializeField] CombatEncounter[] combatEncounters;
    [SerializeField] DialogueEncounter[] dialogueEncounters;
    [SerializeField] ChoiceEncounter[] choiceEncounters;

    /// <summary>
    /// Prepares this level to be completed
    /// </summary>
    public void Init()
    {
        //PlayerPrefs.SetInt("choices", 0);
        //PlayerPrefs.SetInt("dialogues", 0);
        //PlayerPrefs.Save();
    }

    /// <summary>
    /// Returns a random combat encounter from this reigon.
    /// </summary>
    /// <returns></returns>
    public CombatEncounter GetRandomCombatEncounter()
    {
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
        //return PlayerPrefs.GetInt("choices") < choiceEncounters.Length;
        return s.choices < choiceEncounters.Length;
    }

    /// <summary>
    /// Returns the next choice encounter, or starts from the beginning if it runs out.
    /// </summary>
    /// <returns></returns>
    public ChoiceEncounter GetNextChoiceEncounter(Singleton s)
    {
        //int choice = PlayerPrefs.GetInt("choices");
        //PlayerPrefs.SetInt("choices", choice + 1);
        //PlayerPrefs.Save();
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
        //return PlayerPrefs.GetInt("dialogues") < dialogueEncounters.Length;
        return s.dialogues < dialogueEncounters.Length;
    }

    /// <summary>
    /// Returns the next dialogue encounter, or starts from the beginning if it runs out.
    /// </summary>
    /// <returns></returns>
    public DialogueEncounter GetNextDialogueEncounter(Singleton s)
    {
        //int dialogue = PlayerPrefs.GetInt("dialogues");
        //PlayerPrefs.SetInt("dialogues", dialogue + 1);
        //PlayerPrefs.Save();
        int dialogue = s.dialogues;
        s.dialogues++;
        if (!HasMoreDialogueEncounters(s))
            dialogue = 0;
        return dialogueEncounters[dialogue];
    }
}
