using UnityEngine;

[CreateAssetMenu(fileName = "DialogueEncounter", menuName = "Scriptable Objects/Dialogues/DialogueEncounter")]
public class DialogueEncounter : ScriptableObject
{
    [SerializeField] private Sprite[] characterImages;
    [SerializeField] private bool[] isEnemy;
    [SerializeField] private string[] dialogue;
    int currentDialogue = 0;
    int currentSprite = 0;

    /// <summary>
    /// Ensures this encounter starts with the correct values
    /// </summary>
    public void Init()
    {
        currentDialogue = 0;
        currentSprite = 0;
    }

    /// <summary>
    /// Returns the next dialogue if one exists, otherwise an empty string.
    /// </summary>
    /// <returns></returns>
    public string GetNextDialogue()
    {
        if (currentDialogue < dialogue.Length)
            return dialogue[currentDialogue++];
        return "";
    }

    /// <summary>
    /// Returns true if this encounter has more dialogue to return.
    /// </summary>
    /// <returns></returns>
    public bool HasMoreDialogue()
    {
        return currentDialogue < dialogue.Length;
    }

    /// <summary>
    /// Returns the next sprite, or the first one if none are left.
    /// </summary>
    /// <returns></returns>
    public Sprite GetNextSprite()
    {
        if (currentSprite < characterImages.Length)
            return characterImages[currentSprite++];
        return characterImages[0];
    }

    /// <summary>
    /// Returns true if it is a villian (right), or false if hero (left)
    /// </summary>
    /// <returns></returns>
    public bool IsVillian()
    {
        return isEnemy[currentSprite];
    }

}
