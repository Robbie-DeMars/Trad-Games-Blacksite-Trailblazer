using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using Unity.Properties;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //delegate GameObject[] Attack(GameObject[] heroes, GameObject[] enemies, int currentActor);
    [SerializeField] public Enemy enemy;
    [SerializeField] HealthBar HPBar;
    List<Enemy.Attack> upcomingAttacks = new List<Enemy.Attack>();
    private float HP;

    [SerializeField] GameObject[] attackTelegraphs;
    List<GameObject> currentTelegraphs;


    private void Start()
    {
        currentTelegraphs = new List<GameObject>();
        PrepareAttacks();
        HP = enemy.MaxHP;
        HPBar.SetMaxHealth(enemy.MaxHP);
    }

    /// <summary>
    /// Instantiates the telegraphs above this enemy in the worldspace
    /// </summary>
    private void MakeTelegraphs()
    {
        float totalWidth = (upcomingAttacks.Count - 1) * 1f; // Total width of the telegraphs
        float startX = HPBar.transform.position.x - (totalWidth / 2); // Center align

        for (int x = 0; x < upcomingAttacks.Count; x++)
        {
            int attack = enemy.GetAttackNumber(upcomingAttacks[x]);
            currentTelegraphs.Add(Instantiate(attackTelegraphs[attack - 1], this.transform));
            currentTelegraphs[currentTelegraphs.Count - 1].GetComponent<AttackTelegraph>().ConnectAttack(attack, this);
            currentTelegraphs[x].transform.position = new Vector3(startX + x * 1f, HPBar.transform.position.y + 1, HPBar.transform.position.z);
        }
    }

    /// <summary>
    /// Removes telegraphs from the worldspace
    /// </summary>
    private void DeleteTelegraphs()
    {
        for (int x = 0; x < currentTelegraphs.Count(); x++)
        {
            Destroy(currentTelegraphs[x]);
        }
        currentTelegraphs = new List<GameObject>();
    }

    /// <summary>
    /// Returns true if this enemy can execute more attacks this turn, false if not.
    /// </summary>
    /// <returns></returns>
    public bool HasMoreAttacks()
    {
        return upcomingAttacks.Count > 0;
    }

    /// <summary>
    /// Plays the passed attack at the given position, does nothing if given attack does not exist in this context .
    /// </summary>
    /// <param name="currentActor"> Position of current attacker </param>
    /// <param name="attack"> Reference to the attack executed </param>
    /// <returns></returns>
    public IEnumerator PlayNextAttackAnim(int currentActor, Enemy.Attack attack)
    {
        yield return enemy.PlayAttackAnim(currentActor, attack);
    }

    /// <summary>
    /// Executes the next attack queued for this enemy, if such an attack exists.
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attacker </param>
    public void NextAttack(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        if (!HasMoreAttacks())
            return;

        //Call attack
        upcomingAttacks[0](heroes, enemies, currentActor);

        //Remove last attack
        upcomingAttacks.RemoveAt(0);
    }

    /// <summary>
    /// Returns the next attack that will be executed by NextAttack
    /// </summary>
    /// <returns> Reference to the next attack </returns>
    public Enemy.Attack GetNextAttack()
    {
        return upcomingAttacks[0];
    }

    /// <summary>
    /// Resets the attacks this enemy will execute.
    /// </summary>
    public void PrepareAttacks()
    {
        DeleteTelegraphs();
        upcomingAttacks = enemy.PrepareAttacks();
        MakeTelegraphs();
    }

    /// <summary>
    /// Increments this enemy's hp by the passed ammount and updates its HP bar
    /// </summary>
    /// <param name="ammount"></param>
    public void IncrementHP(float ammount)
    {
        if (ammount > 0)
        {
            HP += ammount;
            if (HP > enemy.MaxHP)
            {
                HP = enemy.MaxHP;
            }
        }
        else if (ammount < 0) //If being hurt
        {
            float temp = HP + ammount + enemy.Def;
            if (temp < HP)
                HP = temp;
        }
        HPBar.SetHealth(HP);
        if (HP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Removes this enemy from battle
    /// </summary>
    public void Die()
    {
        HPBar.Die();
        Destroy(this.gameObject);
        Destroy(this);
    }
}
