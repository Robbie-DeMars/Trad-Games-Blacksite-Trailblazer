using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Pathway : MonoBehaviour
{
    [SerializeField] Reigon reigon;
    Level currentLevel;

    [SerializeField] WaterfallText eventText;
    [SerializeField] Slider progressSlider;
    [SerializeField] WaterfallText choice1;
    [SerializeField] WaterfallText choice2;
    [SerializeField] WaterfallText choice3;
    [SerializeField] WaterfallText choice4;
    [SerializeField] WaterfallText endDialogue;
    [SerializeField] WaterfallText endCombat;
    [SerializeField] WaterfallText goToCamp;

    [SerializeField] Image characterImage;
    [SerializeField] Image characterImageOut;
    Coroutine slidingIn;
    Coroutine slidingOut;
    [SerializeField] Transform slideInStartPos;
    [SerializeField] Transform slideInEndPos;
    [SerializeField] float slideSpeed = 1f;

    [SerializeField] Singleton singleton;
    CombatEncounter combatEncounter = null;
    DialogueEncounter dialogueEncounter = null;
    ChoiceEncounter choiceEncounter = null;

    bool firstFrameInScene = true;

    private void Awake()
    {
        currentLevel = reigon.GetCurrentLevel();
        currentLevel.Init();

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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    //Primarily for tracking when leaving and re-entering the scene
    private void Update()
    {
       
    }

    public void NextButtonClicked() {

        if (!currentLevel.HasMoreDialogueEncounters(singleton) && !currentLevel.HasMoreChoiceEncounters(singleton))
        {
            Debug.Log("GRAHHH");
            TransitionToCamp();
            return;
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
            if (encounter == 1) {
                nextEventFound = true;
                combatEncounter = currentLevel.GetRandomCombatEncounter();
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
            DoCombatEncounter(combatEncounter);
        }
        //Dialogue
        else if (dialogueEncounter != null)
        {
            StartCoroutine(DoDialogueEncounter(dialogueEncounter));
        }
        //Choice
        else
        {
            DoChoiceEncounter(choiceEncounter);
        }

    }

    private void TransitionToCamp()
    {
        singleton.choices = 0;
        singleton.dialogues = 0;

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

    private void DoCombatEncounter(CombatEncounter c)
    {
        //Make elements visible
        endCombat.gameObject.SetActive(true);
        endCombat.SetText("Battlestations!");

        eventText.SetText("Enemies Approach...");

        PlayerPrefs.SetString("combatEncounter", c.ID);
        PlayerPrefs.Save();
    }

    public void EndCombatEncounter()
    {
        //Make elements invisible
        endCombat.gameObject.SetActive(false);

        SceneManager.LoadScene("Combat");
    }

    private IEnumerator DoDialogueEncounter(DialogueEncounter d)
    {
        //Set initial text
        d.Init();
        eventText.SetText(d.GetNextDialogue());
        if (slidingIn != null)
            StopCoroutine(slidingIn);
        slidingIn = StartCoroutine(SlideIn(d.GetNextSprite(), false));


        //Wait fot user input for continuing text
        while (d.HasMoreDialogue())
        {
            if (Input.GetKey(KeyCode.Return))
            {
                eventText.SetText(d.GetNextDialogue());
                if (slidingIn != null)
                    StopCoroutine(slidingIn);
                slidingIn = StartCoroutine(SlideIn(d.GetNextSprite(), true));
                yield return new WaitForSeconds(0.5f);
            }
            yield return null;
        }

        //Make end dialogue button visible
        endDialogue.gameObject.SetActive(true);
        endDialogue.SetText("OK");
        yield return null;
    }

    private IEnumerator SlideIn(Sprite spr, bool slideOut)
    {
        float elapsedTime = 0f;
        float duration = slideSpeed;
        float animPercentage = 0;

        //Do not repeat the animation if the same character is still talking.
        if (spr.name.Equals(characterImage.sprite.name))
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

    private void DoChoiceEncounter(ChoiceEncounter c)
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
