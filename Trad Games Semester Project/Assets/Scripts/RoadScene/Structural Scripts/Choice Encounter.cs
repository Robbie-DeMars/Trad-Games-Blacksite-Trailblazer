using UnityEngine;

[CreateAssetMenu(fileName = "ChoiceEncounter", menuName = "Scriptable Objects/Choices/ChoiceEncounter")]
public class ChoiceEncounter : Encounter
{
    [SerializeField] Choice[] choices;
    [SerializeField] public string choiceText;
    [SerializeField] string[] optionTexts;

    /// <summary>
    /// Returns the display descriptor for this option
    /// </summary>
    /// <returns></returns>
    public string Option1Text()
    {
        if (optionTexts.Length == 0)
            return "";
        return optionTexts[0];
    }

    /// <summary>
    /// Returns the display descriptor for this option
    /// </summary>
    /// <returns></returns>
    public string Option2Text()
    {
        if (optionTexts.Length <= 1)
            return "";
        return optionTexts[1];
    }

    /// <summary>
    /// Returns the display descriptor for this option
    /// </summary>
    /// <returns></returns>
    public string Option3Text()
    {
        if (optionTexts.Length <= 2)
            return "";
        return optionTexts[2];
    }

    /// <summary>
    /// Returns the display descriptor for this option
    /// </summary>
    /// <returns></returns>
    public string Option4Text()
    {
        if (optionTexts.Length <= 3)
            return "";
        return optionTexts[3];
    }

    /// <summary>
    /// Executes the effect of this choice, if any
    /// </summary>
    public void Choice1()
    {
        if (choices.Length == 0)
            return;
        choices[0].Outcome();
    }

    /// <summary>
    /// Executes the effect of this choice, if any
    /// </summary>
    public void Choice2()
    {
        if (choices.Length <= 1)
            return;
        choices[1].Outcome();
    }

    /// <summary>
    /// Executes the effect of this choice, if any
    /// </summary>
    public void Choice3()
    {
        if (choices.Length <= 2)
            return;
        choices[2].Outcome();
    }
    
    /// <summary>
    /// Executes the effect of this choice, if any
    /// </summary>
    public void Choice4()
    {
        if (choices.Length <= 3)
            return;
        choices[2].Outcome();
    }

    /// <summary>
    /// Returns the number of choices in this encounter, 0 - 4,294,967,295 possible
    /// </summary>
    /// <returns></returns>
    public int GetChoiceCount()
    {
        return choices.Length;
    }
}
