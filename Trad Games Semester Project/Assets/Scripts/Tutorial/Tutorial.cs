using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] WaterfallText waterfallText;
    [SerializeField] CanvasGroup ui;
    [SerializeField] View view;
    [SerializeField] Controller controller;
    [SerializeField] Button moveButton;
    [SerializeField] Button attackButton;
    [SerializeField] Button defendButton;
    [SerializeField] Button specialButton;
    [SerializeField] Button pturnButton;

    delegate IEnumerator TutorialEvent();

    TutorialEvent[] tutorial;
    int currentEvent = 0;
    bool locked = false;
    bool oneFramePassedEvent = false;
    bool tutorialComplete = false;

    void disableAllButtons()
    {
        moveButton.interactable = false;
        attackButton.interactable = false;
        defendButton.interactable = false;
        specialButton.interactable = false;
        pturnButton.interactable = false;
    }

    void enableAllButtons()
    {
        moveButton.interactable = true;
        attackButton.interactable = true;
        defendButton.interactable = true;
        specialButton.interactable = true;
        pturnButton.interactable = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ui.alpha = 0;
        ui.blocksRaycasts = true;
        tutorialComplete = false;
        disableAllButtons();
        tutorial = new TutorialEvent[]{greeting, pickle, 
            //UI intro
            heroes_intro, taking_a_look, took_a_look, grid_explination, explain_order_bar, 
            //Attacks portion
            explain_attacks, trying_attacks, turn_order_explination, try_attacks_twice_more, trying_attacks, try_attacks_once_more, trying_attacks, explain_looping, explain_AP,
            //Move portion
            introduce_move, instruct_move, pay_attention_turn_order, try_moving, explain_moving_and_turn_order, explain_moving_and_initiative, AP_reminder,
            //Psi heat portion
            bring_attention_psi_heat, introduce_psi_heat, explain_psi_heat, psi_heat_warning, psi_heat_avoid, instruct_pass_turn, try_pass_turn,
            //Enemy portion
            enemy_introduce, enemy_bar_introduce, enemy_telegraph_introduce, enemy_order_explain,
            //Tutorial end
            end_tutorial, completeTutorial};
        ProgressTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Mouse0)) && (!locked && !tutorialComplete))
        {
            ProgressTutorial();
        }
    }

    void ProgressTutorial()
    {
        locked = true;
        oneFramePassedEvent = false;
        ui.blocksRaycasts = true;
        ui.alpha = 1;
        disableAllButtons();
        StartCoroutine(tutorial[currentEvent]());
        currentEvent++;
    }

    private IEnumerator greeting()
    {
        waterfallText.SetText("Welcome to the tutorial!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator pickle()
    {
        waterfallText.SetText("It appears you've found yourself in a pickle, but no worries, this disembodied voice will show you how it's done.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator heroes_intro()
    {
        waterfallText.SetText("On the left side of the battlefield is your ragtag team of humdingers... Take a look!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator taking_a_look()
    {
        ui.alpha = 0;
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator took_a_look()
    {
        waterfallText.SetText("Marvelous");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator grid_explination()
    {
        waterfallText.SetText("Each of your party members and their opponents are placed on a grid, their position dictates where their attacks will land.");

        //Party members
        view.ActivatePartyReticle(0);
        view.ActivatePartyReticle(1);
        view.ActivatePartyReticle(2);
        view.ActivatePartyReticle(3);
        view.ActivatePartyReticle(4);

        //Enemy members
        view.ActivateEnemyReticle(0);
        view.ActivateEnemyReticle(1);
        view.ActivateEnemyReticle(2);
        view.ActivateEnemyReticle(3);
        view.ActivateEnemyReticle(4);

        while (true)
        {
            if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Mouse0)) && oneFramePassedEvent)
            {
                view.ClearReticles();
                break;
            }
            oneFramePassedEvent = true;
            yield return null;
        }
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    [SerializeField] GameObject teamOrderArrow;
    private IEnumerator explain_order_bar()
    {
        waterfallText.SetText("Your turn order is shown by the top left bar with your team displayed on it, the highlighted character is taking their turn.");
        teamOrderArrow.SetActive(true);
        while (true)
        {
            if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Mouse0)) && oneFramePassedEvent)
            {
                teamOrderArrow.SetActive(false);
                break;
            }
            oneFramePassedEvent = true;
            yield return null;
        }
        locked = false;
        yield return null;
    }

    private IEnumerator explain_attacks()
    {
        waterfallText.SetText("Each character can take any number of attack, defend and special actions on your turn, give it a try!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    bool didOneAttack = false;
    public void Attack()
    {
        didOneAttack = true;
    }
    private bool didAttack()
    {
        if (didOneAttack)
        {
            didOneAttack = false;
            return true;
        }
        else
        {
            return false;
        }
    }


    private IEnumerator trying_attacks()
    {
        ui.alpha = 0;
        ui.blocksRaycasts = false;

        //Enable buttons
        attackButton.interactable = true;
        defendButton.interactable = true;
        specialButton.interactable = true;

        //Wait for player to use attack, defend, or special
        while (!didAttack())
        {
            oneFramePassedEvent = true;
            yield return null;
        }
        disableAllButtons();

        yield return new WaitForSeconds(2);

        locked = false;
        yield return null;
    }

    [SerializeField] GameObject arrows;
    private IEnumerator turn_order_explination()
    {
        waterfallText.SetText("Great, as you can see actions progress away from your perspective (to your left) (to your opponents right).");

        //Set arrows active
        arrows.SetActive(true);

        while (true)
        {
            if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Mouse0)) && oneFramePassedEvent)
            {
                //Disable arrows
                arrows.SetActive(false);

                break;
            }
            oneFramePassedEvent = true;
            yield return null;
        }
        locked = false;
        yield return null;
    }

    private IEnumerator try_attacks_twice_more()
    {
        waterfallText.SetText("Try attacking 2 more times!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator try_attacks_once_more()
    {
        waterfallText.SetText("One more time!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator explain_looping()
    {
        waterfallText.SetText("As you can see, your turn hasn't yet ended, instead you've looped back to the beginning.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    [SerializeField] GameObject APArrow;
    private IEnumerator explain_AP()
    {
        waterfallText.SetText("That is because the number of actions each character can take is defined by their AP (Action Points).");
        APArrow.SetActive(true);
        while (true)
        {
            if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Mouse0)) && oneFramePassedEvent)
            {
                APArrow.SetActive(false);
                break;
            }
            oneFramePassedEvent = true;
            yield return null;
        }
        locked = false;
        yield return null;
    }

    private IEnumerator introduce_move()
    {
        waterfallText.SetText("At this point you may have noticed the move button in the bottom left.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator instruct_move()
    {
        waterfallText.SetText("Try toggling the move button, and then swap places with the next character in initiative.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator pay_attention_turn_order()
    {
        waterfallText.SetText("Pay close attention to the turn order while moving...");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator try_moving()
    {
        controller.tutorialMove = true;

        ui.alpha = 0;
        ui.blocksRaycasts = false;

        //Enable buttons
        moveButton.interactable = true;

        //Wait for player to use attack, defend, or special
        while (!didAttack())
        {
            oneFramePassedEvent = true;
            yield return null;
        }

        disableAllButtons();

        controller.tutorialMove = false;
        locked = false;
        yield return null;
    }

    private IEnumerator explain_moving_and_turn_order()
    {
        waterfallText.SetText("Look at that, your turn order changed!");

        teamOrderArrow.SetActive(true);
        while (true)
        {
            if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Mouse0)) && oneFramePassedEvent)
            {
                teamOrderArrow.SetActive(false);
                break;
            }
            oneFramePassedEvent = true;
            yield return null;
        }
        locked = false;
        yield return null;
    }

    private IEnumerator explain_moving_and_initiative()
    {
        waterfallText.SetText("Not only that, the next character's turn has been skipped because you moved past them!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator AP_reminder()
    {
        waterfallText.SetText("(The skipped character also didn't have to spend AP to pass turn!)");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    [SerializeField] GameObject psiHeatArrow;
    private IEnumerator bring_attention_psi_heat()
    {
        waterfallText.SetText("It is also important to note the Psionic Heat bar.");

        //Set arrows active
        psiHeatArrow.SetActive(true);

        while (true)
        {
            if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) || Input.GetKeyDown(KeyCode.Mouse0)) && oneFramePassedEvent)
            {
                //Disable arrows
                psiHeatArrow.SetActive(false);
                break;
            }
            oneFramePassedEvent = true;
            yield return null;
        }

        locked = false;
        yield return null;
    }

    private IEnumerator introduce_psi_heat()
    {
        waterfallText.SetText("The strange properties brought about by this area allow you and your kin to push beyond their normal limits!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator explain_psi_heat()
    {
        waterfallText.SetText("If a character spends all their personal AP, any additional AP spent will go into the Psionic Heat bar.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator psi_heat_warning()
    {
        waterfallText.SetText("Be careful, if Psionic Heat overflows it might leave your team vulnerable, or even pass your turn alltogether!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator psi_heat_avoid()
    {
        waterfallText.SetText("To avoid that from happening, make sure to pass your turn to the enemies when you run low on AP, that will replenish it.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator instruct_pass_turn()
    {
        waterfallText.SetText("Try passing your turn.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator try_pass_turn()
    {
        ui.alpha = 0;
        ui.blocksRaycasts = false;

        //Enable buttons
        pturnButton.interactable = true;

        //Wait for player to use attack, defend, or special
        while (!didAttack())
        {
            oneFramePassedEvent = true;
            yield return null;
        }

        disableAllButtons();
        locked = false;
        yield return null;
    }

    private IEnumerator enemy_introduce()
    {
        waterfallText.SetText("Youch, those enemies can hit hard...");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator enemy_bar_introduce()
    {
        waterfallText.SetText("You may have noticed enemies have their own turn order bar.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator enemy_telegraph_introduce()
    {
        waterfallText.SetText("What's more is that you can see beneath the actual enemies you can hover over what they will do once it's their turn.");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator enemy_order_explain()
    {
        waterfallText.SetText("You can change the order you do actions, but enemies can't, what order you see before you pass turn is what you get, plan wisely!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator end_tutorial()
    {
        waterfallText.SetText("That should cover everything, good luck beating those zombies!");
        locked = false;
        oneFramePassedEvent = true;
        yield return null;
    }

    private IEnumerator completeTutorial()
    {
        tutorialComplete = true;
        enableAllButtons();
        ui.alpha = 0;
        ui.blocksRaycasts = false;
        oneFramePassedEvent = true;
        yield return null;
    }
}
