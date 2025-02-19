using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Model : StateMachine
{
    //Reference to the game view
    [SerializeField] View view;
    //Scriptableobject holding all nessesary party data
    [SerializeField] PartyState party;
    //Instances of all heroes
    public GameObject[] heroes;
    //Hero positions in battle
    [SerializeField] Transform[] heroBattefieldPositions;
    //Reigon scriptableobject containing all combat encounters
    [SerializeField] Reigon reigon;
    //Instances of all enemies
    public GameObject[] enemies;
    //Enemy battle positions
    [SerializeField] Transform[] enemyBattefieldPositions;
    //First frame - used to do things after start has been called
    bool firstFrame = true;
    //Psionic heat level - should start at 0
    int psiHeat = 0;
    //Max psionic heat before it rolls over
    [SerializeField] int maxHeat = 10;

    void Start()
    {
        currentState = State.ATTACK;

        //Instantiate heroes
        heroes = party.GetCurrentPartyForCombat();
        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null)
                heroes[x] = Instantiate(heroes[x], heroBattefieldPositions[x]);
        }

        //Instantiate enemies
        CombatEncounter encounter = reigon.GetRandomCombatEncounter();
        enemies = encounter.GetEnemies();
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
            view.HighlightPartyPosition(GetCurrentActor());
            view.SetMaxHeat(maxHeat);
            view.SetHeat(psiHeat);
            HeroBehavior currentHero = heroes[GetCurrentActor()].GetComponent<HeroBehavior>();
            view.SetMaxAP(currentHero.hero.MaxAP);
            view.SetAP(currentHero.GetAP());
        }


        //Check to see if game is won or lost
        bool allyAlive = false;
        for (int x = 0; x < heroes.Length; x++)
            if (heroes[x] != null)
                allyAlive = true;

        bool enemyAlive = false;
        for (int x = 0; x < enemies.Length; x++)
            if (enemies[x] != null)
                enemyAlive = true;

        if (!allyAlive)
            currentState = State.LOSE;
        else if (!enemyAlive)
            currentState = State.WIN;

        if (currentState == State.WIN)
        {
            //Save current HP of enemies
            for (int x = 0; x < heroes.Length; x++)
            {
                if (heroes[x] != null)
                    heroes[x].GetComponent<HeroBehavior>().SaveStats();
            }
            SceneManager.LoadScene("RoadScene");
        }
    }

    /// <summary>
    /// Ensures no player functions may happen while passed function is executing.
    /// </summary>
    /// <param name="funcToCall"> Function to pause game until finished. </param>
    /// <returns></returns>
    private IEnumerator PauseWhileExecuting(IEnumerator funcToCall)
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

        HeroBehavior currentHero = heroes[GetCurrentActor()].GetComponent<HeroBehavior>();

        //Execute attack in model
        int APCost = currentHero.Attack1(heroes, enemies, GetCurrentActor());

        //Adjust AP or Heat
        DecrementHeroAP(currentHero, APCost);

        //Execute Animation in view
        StartCoroutine(PauseWhileExecuting(PassInitiativeAfter(view.PlayAttack1Anim(heroes[GetCurrentActor()].GetComponent<HeroBehavior>(), heroes, enemies, GetCurrentActor()))));
    }

    /// <summary>
    /// Executes the second attack of the currently selected party member.
    /// </summary>
    public void Attack2()
    {
        if (currentState != State.ATTACK)
            return;

        HeroBehavior currentHero = heroes[GetCurrentActor()].GetComponent<HeroBehavior>();

        //Execute attack in model
        int APCost = heroes[GetCurrentActor()].GetComponent<HeroBehavior>().Attack2(heroes, enemies, GetCurrentActor());

        //Adjust AP or Heat
        DecrementHeroAP(currentHero, APCost);

        //Execute Animation in view
        StartCoroutine(PauseWhileExecuting(PassInitiativeAfter(view.PlayAttack2Anim(heroes[GetCurrentActor()].GetComponent<HeroBehavior>(), heroes, enemies, GetCurrentActor()))));
    }

    /// <summary>
    /// Executes the third attack of the currently selected party member.
    /// </summary>
    public void Attack3()
    {
        if (currentState != State.ATTACK)
            return;

        HeroBehavior currentHero = heroes[GetCurrentActor()].GetComponent<HeroBehavior>();

        //Execute attack in model
        int APCost = heroes[GetCurrentActor()].GetComponent<HeroBehavior>().Attack3(heroes, enemies, GetCurrentActor());

        //Adjust AP or Heat
        DecrementHeroAP(currentHero, APCost);

        //Execute Animation in view
        StartCoroutine(PauseWhileExecuting(PassInitiativeAfter(view.PlayAttack3Anim(heroes[GetCurrentActor()].GetComponent<HeroBehavior>(), heroes, enemies, GetCurrentActor()))));
    }

    /// <summary>
    /// Manages correct translation between the AP meter and the Psi Heat Meter
    /// </summary>
    /// <param name="currentHero"></param>
    /// <param name="APCost"></param>
    public void DecrementHeroAP(HeroBehavior currentHero, int APCost)
    {
        if (currentHero.GetAP() < APCost)
        {
            IncrementPsiHeat(APCost - currentHero.GetAP());
        }
        currentHero.IncrementAP(-APCost);
        view.SetAP(currentHero.GetAP());
    }

    /// <summary>
    /// Manages the effects of psi heat
    /// </summary>
    public void IncrementPsiHeat(int ammount)
    {
        psiHeat += ammount;

        if (psiHeat > maxHeat)
        {
            psiHeat = 0;
            //TODO: Add negative effects, for now, will just hurt all party members
            for (int x = 0; x < heroes.Length; x++)
            {
                if (heroes[x] != null)
                    heroes[x].GetComponent<HeroBehavior>().IncrementHP(-5);
            }
            StartCoroutine(PauseWhileExecuting(view.PlayPsiHeatAnimation()));
        }

        view.SetHeat(psiHeat);
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
            HeroBehavior currentHero = heroes[to].GetComponent<HeroBehavior>();
            DecrementHeroAP(currentHero, 2);
            StartCoroutine(PauseWhileExecuting(view.UpdateBattlePositions(heroes, enemies)));
        }
    }


    /// <summary>
    /// Passes the turn to the enemies.
    /// </summary>
    public void PassTurn()
    {
        if (currentState == State.PAUSED || (currentState == State.WIN || currentState == State.LOSE))
            return;
        //Replenish AP of all party members
        for (int x = 0; x < heroes.Length; x++)
        {
            if (heroes[x] != null)
            {
                heroes[x].GetComponent<HeroBehavior>().ReplenishAP();
            }
        }
        HeroBehavior currentHero = heroes[GetCurrentActor()].GetComponent<HeroBehavior>();
        view.SetAP(currentHero.GetAP());
        view.SetMaxAP(currentHero.hero.MaxAP);
        StartCoroutine(PauseWhileExecuting(PassTurnCoroutine()));
    }

    /// <summary>
    /// Executes enemy attacks and attack animations in order from left to right.
    /// </summary>
    /// <returns></returns>
    private IEnumerator PassTurnCoroutine()
    {
        //Load each enemy into an array as their positions may change
        List<GameObject> enemiesList = new List<GameObject>();
        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x] != null)
                enemiesList.Add(enemies[x]);
        }

        while (enemiesList.Count != 0) {

            EnemyBehavior currentEnemyBehavior = enemiesList[0].GetComponent<EnemyBehavior>();
            GameObject currentEnemy = enemiesList[0];
            enemiesList.RemoveAt(0);

            while (currentEnemyBehavior.HasMoreAttacks()) {

                Enemy.Attack currentAttack = currentEnemyBehavior.GetNextAttack();
                for (int pos = 0; pos < enemies.Length; pos++)
                {
                    if (enemies[pos].Equals(currentEnemy))
                    {
                        currentEnemyBehavior.NextAttack(heroes, enemies, pos);
                        yield return view.PlayEnemyAttackAnim(currentEnemyBehavior, heroes, enemies, pos, currentAttack);
                    }
                }
            }
            currentEnemyBehavior.PrepareAttacks();

        }
        yield return null;
    }

    /// <summary>
    /// Returns the position of the enemy or 0 of missing
    /// </summary>
    /// <param name="enemy"> Enemy to be found </param>
    /// <returns>Position if found, 0 if not</returns>
    public int GetPositionOfEnemy(GameObject enemy)
    {
        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x] == enemy)
                return x;
        }
        return 0;
    }

    /// <summary>
    /// Passes initiative to the next hero in turn order.
    /// </summary>
    public void PassInitiative()
    {
        //Find next party member
        int pos = GetCurrentActor();
        heroes[pos].GetComponent<HeroBehavior>().takingTurn = false;
        pos++;
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
        view.ClearPartyHighlights();
        view.HighlightPartyPosition(pos);
        HeroBehavior currentHero = heroes[GetCurrentActor()].GetComponent<HeroBehavior>();
        view.SetAP(currentHero.GetAP());
        view.SetMaxAP(currentHero.hero.MaxAP);
        
    }

    /// <summary>
    /// Passes the initiative after a 0.5 second delay.
    /// </summary>
    /// <param name="passInitiativeAfterThis"></param>
    /// <returns></returns>
    public IEnumerator PassInitiativeAfter(IEnumerator passInitiativeAfterThis)
    {
        yield return passInitiativeAfterThis;
        yield return new WaitForSeconds(0.5f);
        PassInitiative();
    }

    /// <summary>
    /// Returns the position on the hero's side of who is currently taking their turn.
    /// 
    /// Defaults to 0 if none found.
    /// </summary>
    /// <returns></returns>
    public int GetCurrentActor()
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
