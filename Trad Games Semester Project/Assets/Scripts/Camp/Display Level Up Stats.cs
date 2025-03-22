using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayLevelUpStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] PartyState partyState;
    [SerializeField] CanvasGroup subMenuCanvasGroup = null;
    CanvasGroup canvasGroup;
    TextMeshProUGUI tooltipText;
    bool DisplayLevelUp = true;

    public enum TooltipType
    {
        NORMAL,
        COMBAT
    }

    public enum CharacterType
    {
        MAIN,
        SIDE1,
        SIDE2
    }
    [SerializeField] private CharacterType character;
    [SerializeField] private TooltipType type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (type == TooltipType.NORMAL)
        {
            canvasGroup = GameObject.FindGameObjectsWithTag("Normal Tooltip")[0].GetComponent<CanvasGroup>();
            tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        else if (type == TooltipType.COMBAT)
        {
            canvasGroup = GameObject.FindGameObjectsWithTag("Combat Tooltip")[0].GetComponent<CanvasGroup>();
            tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (subMenuCanvasGroup != null && subMenuCanvasGroup.alpha == 0)
            return;
        if (character == CharacterType.MAIN)
        {
            LevelStatBlock hero = partyState.mainLevels[partyState.GetMainLevel()];
            if (partyState.MainHasNextLevel() && DisplayLevelUp)
            {
                LevelStatBlock heroLeveledUp = partyState.mainLevels[partyState.GetMainLevel() + 1];
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + " --> " + heroLeveledUp.Max_HP + "\n" +
                    "ATK: " + hero.Atk + " --> " + heroLeveledUp.Atk + "\n" +
                    "DEF: " + hero.Def + " --> " + heroLeveledUp.Def + "\n" +
                    "AP: " + hero.Max_AP + " --> " + heroLeveledUp.Max_AP + "\n";
            }
            else
            {
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + "\n" +
                    "ATK: " + hero.Atk + "\n" +
                    "DEF: " + hero.Def + "\n" +
                    "AP: " + hero.Max_AP + "\n";
            }
        }
        else if (character == CharacterType.SIDE1)
        {
            LevelStatBlock hero = partyState.side1Levels[partyState.GetSide1Level()];
            if (partyState.Side1HasNextLevel() && DisplayLevelUp)
            {
                LevelStatBlock heroLeveledUp = partyState.side1Levels[partyState.GetSide1Level() + 1];
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + " --> " + heroLeveledUp.Max_HP + "\n" +
                    "ATK: " + hero.Atk + " --> " + heroLeveledUp.Atk + "\n" +
                    "DEF: " + hero.Def + " --> " + heroLeveledUp.Def + "\n" +
                    "AP: " + hero.Max_AP + " --> " + heroLeveledUp.Max_AP + "\n";
            }
            else
            {
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + "\n" +
                    "ATK: " + hero.Atk + "\n" +
                    "DEF: " + hero.Def + "\n" +
                    "AP: " + hero.Max_AP + "\n";
            }
        }
        else if (character == CharacterType.SIDE2)
        {
            LevelStatBlock hero = partyState.side2Levels[partyState.GetSide2Level()];
            if (partyState.Side2HasNextLevel() && DisplayLevelUp)
            {
                LevelStatBlock heroLeveledUp = partyState.side2Levels[partyState.GetSide2Level() + 1];
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + " --> " + heroLeveledUp.Max_HP + "\n" +
                    "ATK: " + hero.Atk + " --> " + heroLeveledUp.Atk + "\n" +
                    "DEF: " + hero.Def + " --> " + heroLeveledUp.Def + "\n" +
                    "AP: " + hero.Max_AP + " --> " + heroLeveledUp.Max_AP + "\n";
            }
            else
            {
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + "\n" +
                    "ATK: " + hero.Atk + "\n" +
                    "DEF: " + hero.Def + "\n" +
                    "AP: " + hero.Max_AP + "\n";
            }
        }
        canvasGroup.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        canvasGroup.alpha = 0;
    }

    private void OnMouseEnter()
    {
        if (subMenuCanvasGroup != null && subMenuCanvasGroup.alpha == 0)
            return;
        if (character == CharacterType.MAIN)
        {
            LevelStatBlock hero = partyState.mainLevels[partyState.GetMainLevel()];
            if (partyState.MainHasNextLevel() && DisplayLevelUp)
            {
                LevelStatBlock heroLeveledUp = partyState.mainLevels[partyState.GetMainLevel() + 1];
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + " --> " + heroLeveledUp.Max_HP + "\n" +
                    "ATK: " + hero.Atk + " --> " + heroLeveledUp.Atk + "\n" +
                    "DEF: " + hero.Def + " --> " + heroLeveledUp.Def + "\n" +
                    "AP: " + hero.Max_AP + " --> " + heroLeveledUp.Max_AP + "\n";
            }
            else
            {
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + "\n" +
                    "ATK: " + hero.Atk + "\n" +
                    "DEF: " + hero.Def + "\n" +
                    "AP: " + hero.Max_AP + "\n";
            }
        }
        else if (character == CharacterType.SIDE1)
        {
            LevelStatBlock hero = partyState.side1Levels[partyState.GetSide1Level()];
            if (partyState.Side1HasNextLevel() && DisplayLevelUp)
            {
                LevelStatBlock heroLeveledUp = partyState.side1Levels[partyState.GetSide1Level() + 1];
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + " --> " + heroLeveledUp.Max_HP + "\n" +
                    "ATK: " + hero.Atk + " --> " + heroLeveledUp.Atk + "\n" +
                    "DEF: " + hero.Def + " --> " + heroLeveledUp.Def + "\n" +
                    "AP: " + hero.Max_AP + " --> " + heroLeveledUp.Max_AP + "\n";
            }
            else
            {
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + "\n" +
                    "ATK: " + hero.Atk + "\n" +
                    "DEF: " + hero.Def + "\n" +
                    "AP: " + hero.Max_AP + "\n";
            }
        }
        else if (character == CharacterType.SIDE2)
        {
            LevelStatBlock hero = partyState.side2Levels[partyState.GetSide2Level()];
            if (partyState.Side2HasNextLevel() && DisplayLevelUp)
            {
                LevelStatBlock heroLeveledUp = partyState.side2Levels[partyState.GetSide2Level() + 1];
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + " --> " + heroLeveledUp.Max_HP + "\n" +
                    "ATK: " + hero.Atk + " --> " + heroLeveledUp.Atk + "\n" +
                    "DEF: " + hero.Def + " --> " + heroLeveledUp.Def + "\n" +
                    "AP: " + hero.Max_AP + " --> " + heroLeveledUp.Max_AP + "\n";
            }
            else
            {
                tooltipText.text =
                    "Max HP: " + hero.Max_HP + "\n" +
                    "ATK: " + hero.Atk + "\n" +
                    "DEF: " + hero.Def + "\n" +
                    "AP: " + hero.Max_AP + "\n";
            }
        }
        canvasGroup.alpha = 1;
    }

    private void OnMouseExit()
    {
        canvasGroup.alpha = 0;
    }
}
