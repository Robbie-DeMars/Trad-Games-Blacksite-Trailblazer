using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "Evilman", menuName = "Scriptable Objects/Evilman")]
public class Evilman : Enemy
{
    //MAKE SURE TO CHANGE THIS WHEN ADDING NEW ATTACKS
    int totalUniqueAttacks = 2;

    
    public override void Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        //Hit in front
        if (heroes[currentPosition] != null)
            heroes[currentPosition].GetComponent<HeroBehavior>().IncrementHP(-Atk);

        //Move towards camera
        if (currentPosition > 0) {
            GameObject temp = enemies[currentPosition];
            enemies[currentPosition] = enemies[currentPosition - 1];
            enemies[currentPosition - 1] = temp;
        }
    }

    /// <summary>
    /// Executes the models side of Attack 2's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public void Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        //Hit in front
        if (heroes[currentPosition] != null)
            heroes[currentPosition].GetComponent<HeroBehavior>().IncrementHP(-Atk);

        //Move towards camera
        if (currentPosition < enemies.Length - 1)
        {
            GameObject temp = enemies[currentPosition];
            enemies[currentPosition] = enemies[currentPosition + 1];
            enemies[currentPosition + 1] = temp;
        }
    }

    
    public override IEnumerator PlayAttack1Anim(int currentPosition)
    {
        Debug.Log("Enemy Executing attack 1");
        yield return new WaitForSeconds(1f);
        Debug.Log("Its so joever");
    }

    /// <summary>
    /// Executes the views side of Attack 2's behavior
    /// </summary>
    /// <param name="currentActor"> Current position of attacker </param>
    /// <returns></returns>
    public IEnumerator PlayAttack2Anim(int currentPosition)
    {
        Debug.Log("Enemy Executing attack 2");
        yield return new WaitForSeconds(1f);
        Debug.Log("Its so joever");
    }

    public override List<Attack> PrepareAttacks()
    {
        List<Attack> attackList = new List<Attack>();
        for (int x = 0; x < attacks; x++)
        {
            int chosenAttack = Random.Range(0, totalUniqueAttacks);

            if (chosenAttack == 0)
            {
                attackList.Add(Attack1);
            }
            else if (chosenAttack == 1)
            {
                attackList.Add(Attack2);
            }
        }

        return attackList;
    }


    public override IEnumerator PlayAttackAnim(int currentPosition, Attack attack)
    {
        if (attack == Attack1)
        {
            yield return PlayAttack1Anim(currentPosition);
        }
        else if (attack == Attack2)
        {
            yield return PlayAttack2Anim(currentPosition);
        }
        else
        {
            yield return null;
        }
    }

    /// <summary>
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side
    /// </summary>
    /// <param name="currentPosition"></param>
    /// <param name="attack"></param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public List<List<int>> GetAttackTargets(int currentPosition, Attack attack)
    {
        if (attack == Attack1)
            return GetAttack1Targets(currentPosition);
        else if (attack == Attack2)
            return GetAttack2Targets(currentPosition);
        else
            return new List<List<int>>();
    }

    public override List<List<int>> GetAttack1Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { currentSpace };
        List<int> enemies = new List<int>() { };
        return new List<List<int>>() { heroes, enemies };
    }

    /// <summary>
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side
    /// </summary>
    /// <param name="currentSpace"></param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public List<List<int>> GetAttack2Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { currentSpace };
        List<int> enemies = new List<int>() { };
        return new List<List<int>>() { heroes, enemies };
    }

    public override List<List<int>> GetAttackTargets(int currentSpace, int attack)
    {
        if (attack == 1)
        {
            return GetAttack1Targets(currentSpace);
        }
        else if (attack == 2)
        {
            return GetAttack2Targets(currentSpace);
        }
        else
        {
            return GetAttack1Targets(currentSpace);
        }
    }

    public override int GetAttackNumber(Attack attack)
    {
        if (attack == Attack1)
            return 1;
        else if (attack == Attack2)
            return 2;
        else
            return 0;
    }
}
