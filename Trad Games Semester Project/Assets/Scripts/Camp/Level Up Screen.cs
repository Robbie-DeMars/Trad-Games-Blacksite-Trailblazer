using System.Collections;
using TMPro;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI mainCost;
    [SerializeField] TextMeshProUGUI side1Cost;
    [SerializeField] TextMeshProUGUI side2Cost;
    [SerializeField] PartyState party;
    [SerializeField] CanvasGroup canvasGroup;

    private Coroutine coroutine1;
    private Coroutine coroutine2;
    private Coroutine coroutine3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCost.text = "XP: " + party.GetMainLevelUpCost();
        side1Cost.text = "XP: " + party.GetSide1LevelUpCost();
        side2Cost.text = "XP: " + party.GetSide2LevelUpCost();
    }

    // Update is called once per frame
    void Update()
    {
        mainCost.text = "XP: " + party.GetMainLevelUpCost();
        side1Cost.text = "XP: " + party.GetSide1LevelUpCost();
        side2Cost.text = "XP: " + party.GetSide2LevelUpCost();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canvasGroup.alpha == 0)
            {
                canvasGroup.alpha = 1;
            }
            else
            {
                canvasGroup.alpha = 0;
            }
        }

    }

    /// <summary>
    /// Levels up main, and gives visual feedback.
    /// </summary>
    public void LevelUpMain()
    {
        if (!party.LevelUpMain())
        {
            if (coroutine1 != null)
                StopCoroutine(coroutine1);
            StartCoroutine(GiveMainBadFeedback());
        }
        else
        {
            if (coroutine1 != null)
                StopCoroutine(coroutine1);
            StartCoroutine(GiveMainGoodFeedback());
        }
    }

    /// <summary>
    /// Makes it visible that leveling up main failed for whatever reason.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveMainBadFeedback()
    {
        mainCost.color = Color.red;
        yield return new WaitForSeconds(1);
        mainCost.color = Color.black;
    }

    /// <summary>
    /// Makes it visible that leveling up main failed for whatever reason.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveMainGoodFeedback()
    {
        mainCost.color = Color.green;
        yield return new WaitForSeconds(1);
        mainCost.color = Color.black;
    }

    /// <summary>
    /// Levels up main, and gives visual feedback.
    /// </summary>
    public void LevelUpSide1()
    {
        if (!party.LevelUpSide1())
        {
            if (coroutine2 != null)
                StopCoroutine(coroutine2);
            StartCoroutine(GiveSide1BadFeedback());
        }
        else
        {
            if (coroutine2 != null)
                StopCoroutine(coroutine2);
            StartCoroutine(GiveSide1GoodFeedback());
        }
    }

    /// <summary>
    /// Makes it visible that leveling up main failed for whatever reason.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveSide1BadFeedback()
    {
        side1Cost.color = Color.red;
        yield return new WaitForSeconds(1);
        side1Cost.color = Color.black;
    }

    /// <summary>
    /// Makes it visible that leveling up main failed for whatever reason.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveSide1GoodFeedback()
    {
        side1Cost.color = Color.green;
        yield return new WaitForSeconds(1);
        side1Cost.color = Color.black;
    }

    /// <summary>
    /// Levels up main, and gives visual feedback.
    /// </summary>
    public void LevelUpSide2()
    {
        if (!party.LevelUpSide2())
        {
            if (coroutine3 != null)
                StopCoroutine(coroutine3);
            StartCoroutine(GiveSide2BadFeedback());
        }
        else
        {
            if (coroutine3 != null)
                StopCoroutine(coroutine3);
            StartCoroutine(GiveSide2GoodFeedback());
        }
    }

    /// <summary>
    /// Makes it visible that leveling up main failed for whatever reason.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveSide2BadFeedback()
    {
        side2Cost.color = Color.red;
        yield return new WaitForSeconds(1);
        side2Cost.color = Color.black;
    }

    /// <summary>
    /// Makes it visible that leveling up main failed for whatever reason.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GiveSide2GoodFeedback()
    {
        side2Cost.color = Color.green;
        yield return new WaitForSeconds(1);
        side2Cost.color = Color.black;
    }

}
