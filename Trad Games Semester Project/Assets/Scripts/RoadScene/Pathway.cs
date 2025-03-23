using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Pathway : MonoBehaviour
{
    [Header("Stage Elements")]
    [SerializeField] Reigon reigon;
    [SerializeField] WaterfallText eventText;
    [SerializeField] Slider progressSlider;
    [SerializeField] Singleton singleton;
    [SerializeField] Transform road;
    [SerializeField] ParticleSystem roadDust;
    Level currentLevel;

    [Header("Combat Encounter")]
    [SerializeField] Transform enemyGroup;
    [SerializeField] Transform enemyGroupStartPos;
    [SerializeField] Transform enemyGroupEndPos;
    [SerializeField] WaterfallText endCombat;
    CombatEncounter combatEncounter = null;

    [Header("Dialogue Encounter")]
    [SerializeField] Transform slideInStartPos;
    [SerializeField] Transform slideInEndPos;
    [SerializeField] float slideSpeed = 1f;
    [SerializeField] Image characterImage;
    [SerializeField] Image characterImageOut;
    [SerializeField] WaterfallText endDialogue;
    DialogueEncounter dialogueEncounter = null;
    Coroutine slidingIn;
    Coroutine slidingOut;

    [Header("Choice Encounter")]
    [SerializeField] WaterfallText choice1;
    [SerializeField] WaterfallText choice2;
    [SerializeField] WaterfallText choice3;
    [SerializeField] WaterfallText choice4;
    ChoiceEncounter choiceEncounter = null;

    [Header("Camp")]
    [SerializeField] WaterfallText goToCamp;

    private void Awake()
    {
        currentLevel = reigon.GetCurrentLevel();
        currentLevel.RollCombat();
        progressSlider.maxValue = currentLevel.GetTotalEncounters(singleton);

        //Disable non-active UI elements
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
        endDialogue.gameObject.SetActive(false);
        endCombat.gameObject.SetActive(false);
        goToCamp.gameObject.SetActive(false);

        //Prepare the character onscreen sprites
        characterImage.material = Instantiate(characterImage.material);
        characterImageOut.material = Instantiate(characterImageOut.material);
        characterImage.material.SetFloat("_Darkness", 1);
        characterImageOut.material.SetFloat("_Darkness", 1);
        slidingIn = null;
        slidingOut = null;
    }

    private void Update()
    {
        progressSlider.value = currentLevel.GetRemainingEncounters(singleton);
        if(currentLevel.GetRemainingEncounters(singleton) < 0)
            progressSlider.value = 0;
    }


    public void NextButtonClicked() {
        StartCoroutine(TravelToNextEncounter());
    }
    public IEnumerator TravelToNextEncounter()
    {
        float duration = 5f;
        float elapsed = 0f;

        float roadSpeed = 100f;
        float tempSpeed = roadSpeed;
        var emission = roadDust.emission;
        emission.enabled = true;

        //Make things happen every frame traveling
        while (elapsed < duration)
        {
            tempSpeed = Mathf.Sin((elapsed / duration) * Mathf.PI) * roadSpeed;
            elapsed += Time.deltaTime;
            road.transform.Rotate(0f, tempSpeed * Time.deltaTime, 0f);
            yield return null;
        }
        emission.enabled = false;
        yield return ProgressToNextEncounter();
    }

    private IEnumerator ProgressToNextEncounter()
    {
        bool moreCombat = currentLevel.HasMoreCombatEncounters(singleton);
        if (!currentLevel.HasMoreDialogueEncounters(singleton) && !currentLevel.HasMoreChoiceEncounters(singleton) && !moreCombat)
        {
            TransitionToCamp();
            yield break;
        }

        bool nextEventFound = false;
        int encounter = 0;
        combatEncounter = null;
        dialogueEncounter = null;
        choiceEncounter = null;

        //Reroll encounter until valid one is found.
        while (!nextEventFound)
        {
            encounter = Random.Range(1, 4);
            //Combat Encounter
            if (encounter == 1)
            {
                if (moreCombat)
                {
                    nextEventFound = true;
                    combatEncounter = currentLevel.GetRandomCombatEncounter(singleton);
                }
            }
            //Dialogue Encounter
            else if (encounter == 2)
            {
                if (currentLevel.HasMoreDialogueEncounters(singleton))
                {
                    nextEventFound = true;
                    dialogueEncounter = currentLevel.GetNextDialogueEncounter(singleton);
                }
            }
            //Choice Encounter
            else
            {
                if (currentLevel.HasMoreChoiceEncounters(singleton))
                {
                    nextEventFound = true;
                    choiceEncounter = currentLevel.GetNextChoiceEncounter(singleton);
                }
            }
        }

        //Load encounter
        //Combat
        if (combatEncounter != null)
        {
            yield return DoCombatEncounter(combatEncounter);
        }
        //Dialogue
        else if (dialogueEncounter != null)
        {
            yield return DoDialogueEncounter(dialogueEncounter);
        }
        //Choice
        else
        {
            yield return DoChoiceEncounter(choiceEncounter);
        }
        yield return null;
    }

    private void TransitionToCamp()
    {
        singleton.choices = 0;
        singleton.dialogues = 0;
        singleton.combats = 0;

        //Make transition to camp button visible
        eventText.SetText("Go to Camp?");
        goToCamp.gameObject.SetActive(true);
        goToCamp.SetText("-->");
    }

    public void EndTransitionToCamp()
    {
        goToCamp.gameObject.SetActive(false);
        SceneManager.LoadScene("Camp");
    }

    private IEnumerator DoCombatEncounter(CombatEncounter c)
    {
        //Make elements visible
        endCombat.gameObject.SetActive(true);
        enemyGroup.position = enemyGroupStartPos.position;
        endCombat.SetText("Battlestations!");

        eventText.SetText("Enemies Approach...");

        PlayerPrefs.SetString("combatEncounter", c.ID);
        PlayerPrefs.Save();

        GameObject[] enemies = c.GetEnemies();
        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x] != null)
                enemies[x] = Instantiate(enemies[x], enemyGroup.GetChild(x).transform);
        }

        //make the enemies slide in
        float duration = 1f;
        float elapsed = 0f;

        Vector3 startPos = enemyGroup.position;
        Vector3 endPos = enemyGroupEndPos.position;

        while (elapsed < duration)
        {
            enemyGroup.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        enemyGroup.position = enemyGroupEndPos.position; 

        yield return null;  
    }

    public void EndCombatEncounter()
    {
        //Make elements invisible
        endCombat.gameObject.SetActive(false);

        //Return enemies to start positiuon
        enemyGroup.position = enemyGroupStartPos.position;

        SceneManager.LoadScene("Combat");
    }

    private IEnumerator DoDialogueEncounter(DialogueEncounter d)
    {
        //Set initial text
        d.Init();
        eventText.SetText(d.GetNextDialogue());
        if (slidingIn != null)
            StopCoroutine(slidingIn);
        slidingIn = StartCoroutine(SlideIn(d.GetNextSprite(), false, true));


        //Wait for user input for continuing text
        while (d.HasMoreDialogue())
        {
            if (Input.GetKey(KeyCode.Return))
            {
                eventText.SetText(d.GetNextDialogue());
                if (slidingIn != null)
                    StopCoroutine(slidingIn);
                slidingIn = StartCoroutine(SlideIn(d.GetNextSprite(), true, false));
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        //Make end dialogue button visible
        endDialogue.gameObject.SetActive(true);
        endDialogue.SetText("OK");
        yield return null;
    }

    private IEnumerator SlideIn(Sprite spr, bool slideOut, bool newEncounter)
    {
        float elapsedTime = 0f;
        float duration = slideSpeed;
        float animPercentage = 0;

        //Do not repeat the animation if the same character is still talking.
        if (spr.name.Equals(characterImage.sprite.name) && !newEncounter)
        {
            yield break;
        }

        if (slideOut) {
            if (slidingOut != null)
                StopCoroutine(slidingOut);
            slidingOut = StartCoroutine(SlideOut(characterImage.sprite));
        }

        characterImage.sprite = spr;

        characterImage.material.SetFloat("_Darkness", 1f - animPercentage);
        characterImage.transform.position = slideInStartPos.transform.position;

        while (animPercentage < 1f)
        {
            elapsedTime += Time.deltaTime;
            animPercentage = elapsedTime / duration;
            characterImage.material.SetFloat("_Darkness", 1f - animPercentage);
            characterImage.transform.position = Vector3.Lerp(slideInStartPos.transform.position, slideInEndPos.transform.position, animPercentage);
            yield return null;
        }
        characterImage.material.SetFloat("_Darkness", 0);
        yield return null;
    }

    private IEnumerator SlideOut(Sprite spr)
    {
        characterImageOut.sprite = spr;
        float elapsedTime = 0f;
        float duration = slideSpeed;
        float animPercentage = 0;

        characterImageOut.material.SetFloat("_Darkness", animPercentage);
        characterImageOut.transform.position = slideInEndPos.transform.position;

        while (animPercentage < 1f)
        {
            elapsedTime += Time.deltaTime;
            animPercentage = elapsedTime / duration;
            characterImageOut.material.SetFloat("_Darkness", animPercentage);
            characterImageOut.transform.position = Vector3.Lerp(slideInEndPos.transform.position, slideInStartPos.transform.position, animPercentage);
            yield return null;
        }
        characterImageOut.material.SetFloat("_Darkness", 1);
        yield return null;
    }



    /// <summary>
    /// Ends the dialogue encounter
    /// </summary>
    public void EndDialogueEncounter()
    {
        //Make relevant elements invisible 
        endDialogue.gameObject.SetActive(false);
        characterImage.transform.position = slideInStartPos.transform.position;

        NextButtonClicked();
    }

    private IEnumerator DoChoiceEncounter(ChoiceEncounter c)
    {
        //Make choice elements visible and other UI elements invisible
        int choiceCount = c.GetChoiceCount();

        eventText.SetText(c.choiceText);

        if (choiceCount == 1)
        {
            choice1.gameObject.SetActive(true);
            choice1.SetText(c.Option1Text());
        }
        else if (choiceCount == 2)
        {
            choice1.gameObject.SetActive(true);
            choice1.SetText(c.Option1Text());
            choice2.gameObject.SetActive(true);
            choice2.SetText(c.Option2Text());
        }
        else if (choiceCount == 3)
        {
            choice1.gameObject.SetActive(true);
            choice1.SetText(c.Option1Text());
            choice2.gameObject.SetActive(true);
            choice2.SetText(c.Option2Text());
            choice3.gameObject.SetActive(true);
            choice3.SetText(c.Option3Text());
        }
        else
        {
            choice1.gameObject.SetActive(true);
            choice1.SetText(c.Option1Text());
            choice2.gameObject.SetActive(true);
            choice2.SetText(c.Option2Text());
            choice3.gameObject.SetActive(true);
            choice3.SetText(c.Option3Text());
            choice4.gameObject.SetActive(true);
            choice4.SetText(c.Option4Text());
        }
        yield return null;
    }

    /// <summary>
    /// Executes the effect of the first choice in the current choice encounter, if any
    /// </summary>
    public void DoChoice1()
    {
        if (choiceEncounter != null)
            choiceEncounter.Choice1();
        NextButtonClicked();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }

    /// <summary>
    /// Executes the effect of the second choice in the current choice encounter, if any
    /// </summary>
    public void DoChoice2()
    {
        if (choiceEncounter != null)
            choiceEncounter.Choice2();
        NextButtonClicked();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }

    /// <summary>
    /// Executes the effect of the third choice in the current choice encounter, if any
    /// </summary>
    public void DoChoice3()
    {
        if (choiceEncounter != null)
            choiceEncounter.Choice3();
        NextButtonClicked();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }

    /// <summary>
    /// Executes the effect of the fourth choice in the current choice encounter, if any
    /// </summary>
    public void DoChoice4()
    {
        if (choiceEncounter != null)
            choiceEncounter.Choice4();
        NextButtonClicked();
        choice1.gameObject.SetActive(false);
        choice2.gameObject.SetActive(false);
        choice3.gameObject.SetActive(false);
        choice4.gameObject.SetActive(false);
    }

}
