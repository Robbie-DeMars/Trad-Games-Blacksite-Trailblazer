using System.Collections;
using UnityEngine;
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
    [SerializeField] PartyState party;
    Level currentLevel;

    [Header("Combat Encounter")]
    [SerializeField] Transform enemyGroup;
    [SerializeField] Transform enemyGroupStartPos;
    [SerializeField] Transform enemyGroupEndPos;
    [SerializeField] WaterfallText endCombat;
    CombatEncounter combatEncounter = null;

    [Header("Dialogue Encounter")]
    [SerializeField] float slideSpeed = 1f;

    [Header("HERO DISPLAY")]
    [SerializeField] Transform slideInStartPos;
    [SerializeField] Transform slideInEndPos;
    [SerializeField] Image characterImage;
    [SerializeField] Image characterImageOut;

    [Header("ENEMY DISPLAY")]
    [SerializeField] Transform slideInStartPos2;
    [SerializeField] Transform slideInEndPos2;
    [SerializeField] Image characterImage2;
    [SerializeField] Image characterImageOut2;

    DialogueEncounter dialogueEncounter = null;
    Coroutine slidingIn;
    Coroutine slidingOut;
    Coroutine slidingIn2;
    Coroutine slidingOut2;

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
        endCombat.gameObject.SetActive(false);
        goToCamp.gameObject.SetActive(false);

        //Prepare the character onscreen sprites
        characterImage.material = Instantiate(characterImage.material);
        characterImageOut.material = Instantiate(characterImageOut.material);
        characterImage.material.SetFloat("_Darkness", 1);
        characterImageOut.material.SetFloat("_Darkness", 1);
        slidingIn = null;
        slidingOut = null;
        slidingIn2 = null;
        slidingOut2 = null;
    }

    private void Update()
    {
        progressSlider.value = currentLevel.GetRemainingEncounters(singleton);
        if(currentLevel.GetRemainingEncounters(singleton) < 0)
            progressSlider.value = 0;
    }


    public void NextButtonClicked() {
        //Heal members 1/10 of HP
        PlayerPrefs.SetFloat("MainHP", PlayerPrefs.GetFloat("MainHP") + (float)party.mainLevels[party.GetMainLevel()].Max_HP / 10);
        if (PlayerPrefs.GetFloat("MainHP") > party.mainLevels[party.GetMainLevel()].Max_HP)
            PlayerPrefs.SetFloat("MainHP", party.mainLevels[party.GetMainLevel()].Max_HP);
        PlayerPrefs.SetFloat("Side1HP", PlayerPrefs.GetFloat("Side1HP") + (float)party.side1Levels[party.GetSide1Level()].Max_HP / 10);
        if (PlayerPrefs.GetFloat("Side1HP") > party.side1Levels[party.GetSide1Level()].Max_HP)
            PlayerPrefs.SetFloat("Side1HP", party.side1Levels[party.GetSide1Level()].Max_HP);
        PlayerPrefs.SetFloat("Side2HP", PlayerPrefs.GetFloat("Side2HP") + (float)party.side2Levels[party.GetSide2Level()].Max_HP / 10);
        if (PlayerPrefs.GetFloat("Side2HP") > party.side2Levels[party.GetSide2Level()].Max_HP)
            PlayerPrefs.SetFloat("Side2HP", party.side2Levels[party.GetSide2Level()].Max_HP);
        StartCoroutine(TravelToNextEncounter());
    }
    public IEnumerator TravelToNextEncounter()
    {
        float duration = 5f;
        float elapsed = 0f;

        float roadSpeed = 25f;
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

        if (!currentLevel.ordered)
        {
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
        }
        //Travel the ordered path
        else
        {
            Encounter e = currentLevel.GetNextEncounter(singleton);
            if (e is CombatEncounter)
                combatEncounter = (CombatEncounter)e;
            if (e is DialogueEncounter)
                dialogueEncounter = (DialogueEncounter)e;
            if (e is ChoiceEncounter)
                choiceEncounter = (ChoiceEncounter)e;
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

        //Delete all in world enemies
        GameObject[] toBeDestroyed = GameObject.FindGameObjectsWithTag("Enemy");
        for (int x = 0; x < toBeDestroyed.Length; x++)
        {
            Destroy(toBeDestroyed[x]);
        }

        if (PlayerPrefs.GetInt("level") == 0)
        {
            SceneManager.LoadScene("Combat Tutorial");
        }
        else if (PlayerPrefs.GetInt("level") == 1)
        {
            SceneManager.LoadScene("Combat Desert");
        }
        else if (PlayerPrefs.GetInt("level") == 2)
        {
            SceneManager.LoadScene("Combat Swamp");
        }
        else if (PlayerPrefs.GetInt("level") == 3)
        {
            SceneManager.LoadScene("Combat");
        }
        else 
        {
            SceneManager.LoadScene("Combat");
        }
    }

    private IEnumerator DoDialogueEncounter(DialogueEncounter d)
    {
        //Set initial text
        d.Init();
        bool firstLeft = true;
        bool firstRight = true;

        //Wait for user input for continuing text
        while (d.HasMoreDialogue())
        {
            if (Input.anyKeyDown && (!Input.GetKeyDown(KeyCode.E) && !Input.GetKeyDown(KeyCode.Space)))
            {
                eventText.SetText(d.GetNextDialogue());
                if (slidingIn != null)
                    StopCoroutine(slidingIn);
                if (slidingIn2 != null)
                    StopCoroutine(slidingIn2);
                if (d.IsVillian())
                {
                    slidingIn2 = StartCoroutine(SlideInFromRight(d.GetNextSprite(), !firstRight, firstRight));
                    firstRight = false;
                }
                else
                {
                    slidingIn = StartCoroutine(SlideInFromLeft(d.GetNextSprite(), !firstLeft, firstLeft));
                    firstLeft = false;
                }
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        //Wait for user confirmation
        while (!(Input.anyKeyDown && (!Input.GetKeyDown(KeyCode.E) && !Input.GetKeyDown(KeyCode.Space))))
        {
            yield return null;
        }
        EndDialogueEncounter();
        yield return null;
    }

    private IEnumerator SlideInFromLeft(Sprite spr, bool slideOut, bool newEncounter)
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
            slidingOut = StartCoroutine(SlideOutFromLeft(characterImage.sprite));
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

    private IEnumerator SlideOutFromLeft(Sprite spr)
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


    private IEnumerator SlideInFromRight(Sprite spr, bool slideOut, bool newEncounter)
    {
        float elapsedTime = 0f;
        float duration = slideSpeed;
        float animPercentage = 0;

        //Do not repeat the animation if the same character is still talking.
        if (spr.name.Equals(characterImage2.sprite.name) && !newEncounter)
        {
            yield break;
        }

        if (slideOut)
        {
            if (slidingOut2 != null)
                StopCoroutine(slidingOut2);
            slidingOut2 = StartCoroutine(SlideOutFromRight(characterImage2.sprite));
        }

        characterImage2.sprite = spr;

        characterImage2.material.SetFloat("_Darkness", 1f - animPercentage);
        characterImage2.transform.position = slideInStartPos2.transform.position;

        while (animPercentage < 1f)
        {
            elapsedTime += Time.deltaTime;
            animPercentage = elapsedTime / duration;
            characterImage2.material.SetFloat("_Darkness", 1f - animPercentage);
            characterImage2.transform.position = Vector3.Lerp(slideInStartPos2.transform.position, slideInEndPos2.transform.position, animPercentage);
            yield return null;
        }
        characterImage2.material.SetFloat("_Darkness", 0);
        yield return null;
    }

    private IEnumerator SlideOutFromRight(Sprite spr)
    {
        characterImageOut2.sprite = spr;
        float elapsedTime = 0f;
        float duration = slideSpeed;
        float animPercentage = 0;

        characterImageOut2.material.SetFloat("_Darkness", animPercentage);
        characterImageOut2.transform.position = slideInEndPos2.transform.position;

        while (animPercentage < 1f)
        {
            elapsedTime += Time.deltaTime;
            animPercentage = elapsedTime / duration;
            characterImageOut2.material.SetFloat("_Darkness", animPercentage);
            characterImageOut2.transform.position = Vector3.Lerp(slideInEndPos2.transform.position, slideInStartPos2.transform.position, animPercentage);
            yield return null;
        }
        characterImageOut2.material.SetFloat("_Darkness", 1);
        yield return null;
    }



    /// <summary>
    /// Ends the dialogue encounter
    /// </summary>
    public void EndDialogueEncounter()
    {
        characterImage.transform.position = slideInStartPos.transform.position;
        characterImage2.transform.position = slideInStartPos2.transform.position;
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
