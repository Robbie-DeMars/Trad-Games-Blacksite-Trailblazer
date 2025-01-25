using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //Starting party members and enemies and their desired positions.
    [SerializeField] GameObject[] Heroes;
    [SerializeField] GameObject[] Enemies;

    //Real positions in combat and party members
    GameObject[] HPos;
    GameObject[] EPos;

    //Combat positions real transformations
    [SerializeField] Transform[] HTrans;
    [SerializeField] Transform[] ETrans;

    //Initiative Markers
    [SerializeField] GameObject[] inits;

    //Enemy targets
    [SerializeField] GameObject[] reticles;

    public delegate IEnumerator action();
    private Queue<action> actions = new Queue<action>();

    bool setupPostStart = true;

    int currentSelected = 0;
    string state = "Attacking"; //Other states are 'Moving' 'Executing Queue'

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Initialize real position arrays
        HPos = new GameObject[Heroes.Length];
        EPos = new GameObject[Enemies.Length];

        //Load battlefield blueprint into arrays while instantiating participants
        for (int x = 0; x < Heroes.Length; x++)
        {
            if (Heroes[x] != null)
            {
                HPos[x] = Instantiate(Heroes[x]);
                HPos[x].transform.position = HTrans[x].position;
            }
        }

        for (int x = 0; x < Enemies.Length; x++)
        {
            if (Enemies[x] != null)
            {
                EPos[x] = Instantiate(Enemies[x]);
                EPos[x].transform.position = ETrans[x].position;
            }
        }

        //Select first party member
        while (Heroes[currentSelected] == null)
            currentSelected++;

        clearReticles();
    }

    // Update is called once per frame
    void Update()
    {
        if (actions.Count != 0)
        {
            if (!state.Equals("Executing Queue"))
            {
                toggleActionFlag();
                action a = actions.Dequeue();

                // Start executing the action as a coroutine
                StartCoroutine(ExecuteActionCoroutine(a));
            }
            return;
        }

        if (setupPostStart)
        {
            updateSelection();
            setupPostStart = false;
        }




        if (setupPostStart)
        {
            updateSelection();
            setupPostStart = false;
        }
    }

    IEnumerator ExecuteActionCoroutine(action actionToExecute)
    {
        // Invoke the action and wait for it to complete
        yield return StartCoroutine(actionToExecute());
        toggleActionFlag();
    }

    /**
     * Executes currently selected party member's first attack if able.
     */
    public void Attack1()
    {
        if (state.Equals("Attacking"))
        {
            actions.Enqueue(HPos[currentSelected].GetComponent<HeroCombatManager>().Attack1);
            actions.Enqueue(passInitiative);
        }
    }

    public void DisplayAttack1Targets()
    {
        List<int> targets = HPos[currentSelected].GetComponent<HeroCombatManager>().DisplayAttack1Targets();
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] >= 0 && targets[i] < EPos.Length)
            {
                reticles[targets[i]].SetActive(true);
            }
        }
    }

    /**
     * Executes currently selected party member's second attack if able.
     */
    public void Attack2()
    {
        if (state.Equals("Attacking"))
        {
            actions.Enqueue(HPos[currentSelected].GetComponent<HeroCombatManager>().Attack2);
            actions.Enqueue(passInitiative);
        }
    }

    public void DisplayAttack2Targets()
    {
        List<int> targets = HPos[currentSelected].GetComponent<HeroCombatManager>().DisplayAttack2Targets();
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] >= 0 && targets[i] < EPos.Length)
            {
                reticles[targets[i]].SetActive(true);
            }
        }
    }

    /**
     * Executes currently selected party member's second attack if able.
     */
    public void Attack3()
    {
        if (state.Equals("Attacking"))
        {
            actions.Enqueue(HPos[currentSelected].GetComponent<HeroCombatManager>().Attack3);
            actions.Enqueue(passInitiative);
        }
    }

    public void DisplayAttack3Targets()
    {
        List<int> targets = HPos[currentSelected].GetComponent<HeroCombatManager>().DisplayAttack3Targets();
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] >= 0 && targets[i] < EPos.Length)
            {
                reticles[targets[i]].SetActive(true);
            }
        }
    }

    /**
     * Toggles the current selected party member to be moving.
     */
    public void toggleMoving()
    {
        if (!state.Equals("Moving"))
        {
            state = "Moving";
        }
        else
        {
            state = "Attacking";
        }
    }

    public IEnumerator passInitiative()
    {
        yield return new WaitForSeconds(0.1f);
        //Select first party member
        do
        {
            currentSelected++;
            if (currentSelected >= Heroes.Length)
                currentSelected = 0;
        }
        while (Heroes[currentSelected] == null);

        updateSelection();
        Debug.Log("Initiative Passed");
    }

    /**
     * Returns this managers tracked position of a party member in the combat order.
     */
    public int getHeroPosition(GameObject heroToFind)
    {
        for (int x = 0; x < HPos.Length; x++)
        {
            if (heroToFind == HPos[x])
                return x;
        }
        return -1;
    }

    /**
     * Returns this managers tracked position of a enemy member in the combat order.
     */
    public int getEnemyPosition(GameObject enemyToFind)
    {
        for (int x = 0; x < EPos.Length; x++)
        {
            if (enemyToFind == EPos[x])
            return x;
        }
        return -1;
    }

    /**
     * Returns this manager's list of heroes.
     */
    public GameObject[] getHeroes()
    {
        return HPos;
    }

    /**
     * Returns this manager's list of enemies.
     */
    public GameObject[] getEnemies()
    {
        return EPos;
    }

    public void updateSelection()
    {
        for (int x = 0; x < inits.Length; x++)
        {
            if (inits[x] != null)
            {
                if (currentSelected == x)
                    inits[x].SetActive(true);
                else
                    inits[x].SetActive(false);
            }
        }
    }

    public void toggleActionFlag()
    {
        if (state.Equals("Executing Queue"))
            state = "Attacking";
        else
            state = "Executing Queue";
        Debug.Log(state);
    }

    public void clearReticles()
    {
        for (int x = 0; x < reticles.Length; x++)
        {
            reticles[x].SetActive(false);
        }
    }
}
