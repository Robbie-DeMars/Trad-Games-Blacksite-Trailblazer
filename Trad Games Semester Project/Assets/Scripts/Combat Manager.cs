using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

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
    [SerializeField] GameObject[] EReticles;
    [SerializeField] GameObject[] HReticles;

    //Camera
    [SerializeField] GameObject camera;

    public delegate IEnumerator action();
    public Queue<action> actions = new Queue<action>();

    bool setupPostStart = true;

    int currentSelected = 0;
    int altSelection = -1;

    string state = "Attacking"; //Other states are 'Moving' 'Executing Queue'

    //Psionic Heat
    int psionicHeat = 0;
    [SerializeField] TextMeshProUGUI heatText;
    bool heatIncrementing = false;

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
        int heroesChecked = 0;
        while (Heroes[currentSelected] == null && heroesChecked < HPos.Length)
        {
            currentSelected++;
            heroesChecked++;
        }
        

        clearReticles();
        randomizeEnemyAttacks();
    }

    // Update is called once per frame
    void Update()
    {
        if (actions.Count != 0)
        {
            if (!state.Equals("Executing Queue"))
            {
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

        if (state.Equals("Moving"))
        {
            //Find closest position to mouse pointer
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(camera.transform.position.z))); //This could break in the future if we move the camera
            float closestVal = 1;
            altSelection = -1;
            for (int x = 0; x < HTrans.Length; x++)
            {
                float newVal = Vector3.Distance(mousePosition, HTrans[x].transform.position);
                if (newVal < closestVal && newVal < 1) { 
                    altSelection = x;
                    closestVal = newVal;
                }
            }

            //Activate that initiative marker to signify
            if (altSelection != -1)
                inits[altSelection].SetActive(true);
            else
                updateSelection();

            if (Input.GetKeyDown(KeyCode.Mouse0) && altSelection != -1)
            {
                swapPartyMembers(currentSelected, altSelection);
            }
        }
    }

    IEnumerator ExecuteActionCoroutine(action actionToExecute)
    {
        // Invoke the action and wait for it to complete
        toggleActionFlag();
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
                EReticles[targets[i]].SetActive(true);
            }
            else if (targets[i] - 100 >= 0 && targets[i] - 100 < EPos.Length)
            {
                HReticles[targets[i] - 100].SetActive(true); 
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
                EReticles[targets[i]].SetActive(true);
            }
            else if (targets[i] - 100 >= 0 && targets[i] - 100 < EPos.Length)
            {
                HReticles[targets[i] - 100].SetActive(true);
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
                EReticles[targets[i]].SetActive(true);
            }
            else if (targets[i] - 100 >= 0 && targets[i] - 100 < EPos.Length)
            {
                HReticles[targets[i] - 100].SetActive(true);
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
        Debug.Log(state);
    }


    public void randomizeEnemyAttacks()
    {
        for (int x = 0; x < EPos.Length; x++)
            randomizeEnemyAttack(x);
    }

    public void randomizeEnemyAttack(int pos)
    {
        if (EPos[pos] != null)
        {
            int rand = Random.Range(1, 4);
            EPos[pos].GetComponent<EnemyCombatManager>().SetNextAttack(rand);
        }
    }

    public void passToEnemies()
    {
        if (!state.Equals("Executing Queue")) {
            for (int x = 0; x < EPos.Length; x++)
            {
                if (EPos[x] != null)
                {
                    if (EPos[x].GetComponent<EnemyCombatManager>().GetNextAttack() == 1)
                        actions.Enqueue(EPos[x].GetComponent<EnemyCombatManager>().Attack1);
                    else if (EPos[x].GetComponent<EnemyCombatManager>().GetNextAttack() == 2)
                        actions.Enqueue(EPos[x].GetComponent<EnemyCombatManager>().Attack2);
                    else if (EPos[x].GetComponent<EnemyCombatManager>().GetNextAttack() == 3)
                        actions.Enqueue(EPos[x].GetComponent<EnemyCombatManager>().Attack3);
                }
            }
            randomizeEnemyAttacks();
            incrementHeat(-psionicHeat / 2);
        }
    }

    public void incrementHeat(int ammount)
    {
        StartCoroutine(incrementHeatHelper(ammount));
    } 

    private IEnumerator incrementHeatHelper(int ammount)
    {
        while (heatIncrementing) { yield return null; };
        heatIncrementing = true;
        int targetHeat = psionicHeat + ammount;
        int startHeat = psionicHeat;
        float elapsedTime = 0;
        float time = 0.01f * Mathf.Abs(targetHeat - startHeat);
        while (elapsedTime <= time)
        {
            Debug.Log(elapsedTime);
            psionicHeat = (int)Mathf.Lerp(startHeat, targetHeat, elapsedTime / time);
            heatText.text = "Psionic Heat: " + psionicHeat + "%";
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        psionicHeat = targetHeat;
        heatText.text = "Psionic Heat: " + psionicHeat + "%";
        heatIncrementing = false;
    }

    public IEnumerator passInitiative()
    {
        yield return new WaitForSeconds(0.1f);
        //Select first party member
        int stopLoop = 0;
        do
        {
            stopLoop++;
            currentSelected++;
            if (currentSelected >= HPos.Length)
                currentSelected = 0;
            Debug.Log("Uh oh 2");
        }
        while (HPos[currentSelected] == null && stopLoop <= EPos.Length);

        updateSelection();
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
        for (int x = 0; x < EReticles.Length; x++)
        {
            EReticles[x].SetActive(false);
        }

        for (int x = 0; x < HReticles.Length; x++)
        {
            HReticles[x].SetActive(false);
        }
    }

    public void swapPartyMembers(int startPos, int endPos)
    {
        while (startPos != endPos)
        {
            if (startPos < endPos)
            {
                startPos++;
                actions.Enqueue(moveSelectedRight);
            }
            else
            {
                startPos--;
                actions.Enqueue(moveSelectedLeft);
            }
        }
    }




    
    public IEnumerator moveSelectedRight()
    {
        //Swap position in array
        GameObject temp = HPos[currentSelected];
        HPos[currentSelected] = HPos[currentSelected + 1];
        HPos[currentSelected + 1] = temp;
        currentSelected++;
        updateSelection();

        //Move animation
        float elapsedTime = 0f;
        float duration = 0.3f; //Move over 1 second
        while (elapsedTime < duration) 
        {
            if (HPos[currentSelected] != null)
            {
                HPos[currentSelected].transform.position = Vector3.Lerp(HPos[currentSelected].transform.position, HTrans[currentSelected].transform.position, elapsedTime / duration);
            }

            if (currentSelected - 1 < HPos.Length && HPos[currentSelected - 1] != null)
            {
                HPos[currentSelected - 1].transform.position = Vector3.Lerp(HPos[currentSelected - 1].transform.position, HTrans[currentSelected - 1].transform.position, elapsedTime / duration);
            }


            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator moveSelectedLeft()
    {
        //Swap position in array
        GameObject temp = HPos[currentSelected];
        HPos[currentSelected] = HPos[currentSelected - 1];
        HPos[currentSelected - 1] = temp;
        currentSelected--;
        updateSelection();

        //Move animation
        float elapsedTime = 0f;
        float duration = 0.3f; //Move over 1 second
        while (elapsedTime < duration)
        {
            if (HPos[currentSelected] != null)
            {
                HPos[currentSelected].transform.position = Vector3.Lerp(HPos[currentSelected].transform.position, HTrans[currentSelected].transform.position, elapsedTime / duration);
            }

            if (currentSelected + 1 >= 0 && HPos[currentSelected + 1] != null)
            {
                HPos[currentSelected + 1].transform.position = Vector3.Lerp(HPos[currentSelected + 1].transform.position, HTrans[currentSelected + 1].transform.position, elapsedTime / duration);
            }


            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    public void displayEnemyReticles(int position, int attack)
    {
        List<int> targets = new List<int>();
        if (attack == 1)
            targets = EPos[position].GetComponent<EnemyCombatManager>().DisplayAttack1Targets();
        else if (attack == 2)
            targets = EPos[position].GetComponent<EnemyCombatManager>().DisplayAttack2Targets();
        else if (attack == 3)
            targets = EPos[position].GetComponent<EnemyCombatManager>().DisplayAttack3Targets();

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] >= 0 && targets[i] < EPos.Length)
            {
                EReticles[targets[i]].SetActive(true);
            }
            else if (targets[i] - 100 >= 0 && targets[i] - 100 < EPos.Length)
            {
                HReticles[targets[i] - 100].SetActive(true);
            }
        }
    }


    public void removeFromCombat(GameObject toBeRemoved)
    {
        for (int x = 0; x < EPos.Length; x++)
        {
            if (EPos[x] == toBeRemoved)
                EPos[x] = null;
        }
        for (int x = 0; x < HPos.Length; x++)
        {
            if (HPos[x] == toBeRemoved)
                HPos[x] = null;
        }
        if (HPos[currentSelected] == null)
            actions.Enqueue(passInitiative);
    }



}
