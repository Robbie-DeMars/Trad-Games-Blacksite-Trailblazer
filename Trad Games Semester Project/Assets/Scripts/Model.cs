using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.UIElements;

public class Model : StateMachine
{
    [SerializeField] View view;

    [SerializeField] PartyState party;

    GameObject[] heroes;

    [SerializeField] Transform[] heroBattefieldPositions;

    [SerializeField] CombatEncounter encounter;

    GameObject[] enemies;

    [SerializeField] Transform[] enemyBattefieldPositions;

    delegate IEnumerator Queueable(IEnumerator e);

    bool firstFrame = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.ATTACK;

        //Instantiate heroes
        heroes = party.getCurrentPartyForCombat();
        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null)    
                heroes[x] = Instantiate(heroes[x], heroBattefieldPositions[x]);
        }

        //Instantiate enemies
        enemies = encounter.getEnemies();
        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x] != null)
                enemies[x] = Instantiate(enemies[x], enemyBattefieldPositions[x]);
        }

        //Highlight first party member
        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null)
            {
                heroes[x].GetComponent<HeroBehavior>().takingTurn = true;
                break;
            }

        }
        
    }

    private void Update()
    {
        if (firstFrame)
        {
            firstFrame = false;
            //Highlight first party member
            view.highlightPartyPosition(getCurrentActor());
        }
    }

    /// <summary>
    /// Ensures no player functions may happen while passed function is executing.
    /// </summary>
    /// <param name="funcToCall"> Function to pause game until finished. </param>
    /// <returns></returns>
    private IEnumerator pauseWhileExecuting(IEnumerator funcToCall)
    {
        currentState = State.PAUSED;
        yield return StartCoroutine(funcToCall);
        currentState = State.ATTACK;
    }

    /// <summary>
    /// Executes the first attack of the currently selected party member.
    /// </summary>
    public void Attack1()
    {
        if (currentState != State.ATTACK)
            return;

        //Execute attack in model
        heroes[getCurrentActor()].GetComponent<HeroBehavior>().Attack1(heroes, enemies, getCurrentActor());

        //Execute Animation in view
        StartCoroutine(pauseWhileExecuting(view.playAttack1Anim(heroes[getCurrentActor()].GetComponent<HeroBehavior>(), heroes, enemies, getCurrentActor())));

        //Go to next avalible character
        passInitiative();
    }

    /// <summary>
    /// Executes the second attack of the currently selected party member.
    /// </summary>
    public void Attack2()
    {
        if (currentState != State.ATTACK)
            return;

        //Execute attack in model
        heroes[getCurrentActor()].GetComponent<HeroBehavior>().Attack2(heroes, enemies, getCurrentActor());

        //Execute Animation in view
        StartCoroutine(pauseWhileExecuting(view.playAttack2Anim(heroes[getCurrentActor()].GetComponent<HeroBehavior>(), heroes, enemies, getCurrentActor())));

        //Go to next avalible character
        passInitiative();
    }

    /// <summary>
    /// Executes the third attack of the currently selected party member.
    /// </summary>
    public void Attack3()
    {
        if (currentState != State.ATTACK)
            return;

        //Execute attack in model
        heroes[getCurrentActor()].GetComponent<HeroBehavior>().Attack3(heroes, enemies, getCurrentActor());

        //Execute Animation in view
        StartCoroutine(pauseWhileExecuting(view.playAttack3Anim(heroes[getCurrentActor()].GetComponent<HeroBehavior>(), heroes, enemies, getCurrentActor())));

        //Go to next avalible character
        passInitiative();
    }

    /// <summary>
    /// Begins the process of moving one character to annother position.
    /// </summary>
    public void Move()
    {
        if (currentState == State.MOVE)
            currentState = State.ATTACK;
        else if (currentState != State.PAUSED)
            currentState = State.MOVE;
    }

    /// <summary>
    /// Moves hero at position from to position to, then updates the UI
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    public void MoveHeroFromTo(int from, int to)
    {
        Debug.Log("Moving from " + from + " to " + to);
        if (from < to)
        {
            GameObject temp = heroes[from];
            heroes[from] = heroes[from + 1];
            heroes[from + 1] = temp;
            MoveHeroFromTo(from + 1, to);
        }
        else if (from > to)
        {
            GameObject temp = heroes[from];
            heroes[from] = heroes[from - 1];
            heroes[from - 1] = temp;
            MoveHeroFromTo(from - 1, to);
        }
        else
        {
            StartCoroutine(pauseWhileExecuting(view.updateBattlePositions(heroes, enemies)));
        }
    }


    /// <summary>
    /// Passes the turn to the enemies.
    /// </summary>
    public void PassTurn()
    {
        StartCoroutine(pauseWhileExecuting(PassTurnCoroutine()));
    }

    /// <summary>
    /// Executes enemy attacks and attack animations in order from left to right.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PassTurnCoroutine()
    {
        //For each enemy spot
        for (int x = 0; x < enemies.Length; x++)
        {
            //If there is an enemy there
            if (enemies[x] != null)
            {
                EnemyBehavior currentEnemy = enemies[x].GetComponent<EnemyBehavior>();

                //For every queued attack, execute and then animate it
                while (currentEnemy.HasMoreAttacks())
                {
                    Enemy.Attack currentAttack = currentEnemy.GetNextAttack();
                    currentEnemy.NextAttack(heroes, enemies, x);

                    yield return view.playEnemyAttackAnim(currentEnemy, heroes, enemies, x, currentAttack);
                }
                currentEnemy.prepareAttacks();
            }
        }
        yield return null;
    }

    /// <summary>
    /// Passes initiative to the next hero in turn order.
    /// </summary>
    public void passInitiative()
    {
        //Find next party member
        int pos = getCurrentActor();
        heroes[pos].GetComponent<HeroBehavior>().takingTurn = false;
        if (pos >= heroes.Length)
            pos = 0;
        while (heroes[pos] == null)
        {
            pos++;
            if (pos >= heroes.Length)
                pos = 0;
        }
        heroes[pos].GetComponent<HeroBehavior>().takingTurn = true;

        //Tell view to update
        view.clearPartyHighlights();
        view.highlightPartyPosition(pos);
    }

    /// <summary>
    /// Returns the position on the hero's side of who is currently taking their turn.
    /// 
    /// Defaults to 0 if none found.
    /// </summary>
    /// <returns></returns>
    public int getCurrentActor()
    {
        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null && heroes[x].GetComponent<HeroBehavior>().takingTurn)
            {
                return x;
            }
        }
        return 0;
    }

}
