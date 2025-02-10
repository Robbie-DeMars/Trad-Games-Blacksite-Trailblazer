using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    enum State {ATTACK, MOVE, PAUSED}
    private State currentState;

    [SerializeField] View view;

    [SerializeField] PartyState party;

    GameObject[] heroes;

    [SerializeField] Transform[] heroBattefieldPositions;

    [SerializeField] CombatEncounter encounter;

    GameObject[] enemies;

    [SerializeField] Transform[] enemyBattefieldPositions;

    delegate IEnumerator Queueable(IEnumerator e);

    int currentActor = 0;
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
        view.highlightPartyPosition(currentActor);
        if (heroes[currentActor] == null)
            passInitiative();
    }

    private void Update()
    {
        if (firstFrame)
        {
            firstFrame = false;
            //Highlight first party member
            view.highlightPartyPosition(currentActor);
            if (heroes[currentActor] == null)
                passInitiative();
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
        GameObject savedHero = heroes[currentActor];
        heroes = heroes[currentActor].GetComponent<HeroBehavior>().Attack1(heroes, enemies, currentActor);
        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null && heroes[x].Equals(savedHero))
                currentActor = x;
        }
        

        //Execute Animation in view
        StartCoroutine(pauseWhileExecuting(view.playAttack1Anim(heroes[currentActor].GetComponent<HeroBehavior>(), heroes, enemies, currentActor)));

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


        //TODO
    }

    /// <summary>
    /// Executes the third attack of the currently selected party member.
    /// </summary>
    public void Attack3()
    {
        if (currentState != State.ATTACK)
            return;
        //TODO
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
        //TODO
    }

    /// <summary>
    /// Passes the turn to the enemies.
    /// </summary>
    public void PassTurn()
    {
        //TODO
    }

    /// <summary>
    /// Passes initiative to the next hero in turn order.
    /// </summary>
    public void passInitiative()
    {
        //Find next party member
        currentActor++;
        if (currentActor >= heroes.Length)
            currentActor = 0;
        
        while (heroes[currentActor] == null) {
            currentActor++;
            if (currentActor >= heroes.Length)
                currentActor = 0;
        }

        //Tell view to update
        view.clearPartyHighlights();
        view.highlightPartyPosition(currentActor);
    }



}
