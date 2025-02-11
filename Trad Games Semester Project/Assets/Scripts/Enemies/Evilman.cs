using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Evilman", menuName = "Scriptable Objects/Evilman")]
public class Evilman : Enemy
{
    //MAKE SURE TO CHANGE THIS WHEN ADDING NEW ATTACKS
    int totalUniqueAttacks = 2;

    /// <summary>
    /// Executes the models side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public override void Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        //Hit in front
        if (heroes[currentPosition] != null)
            heroes[currentPosition].GetComponent<HeroBehavior>().IncrementHP(-5);

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
            heroes[currentPosition].GetComponent<HeroBehavior>().IncrementHP(-5);

        //Move towards camera
        if (currentPosition < enemies.Length - 1)
        {
            GameObject temp = enemies[currentPosition];
            enemies[currentPosition] = enemies[currentPosition + 1];
            enemies[currentPosition + 1] = temp;
        }
    }

    /// <summary>
    /// Executes the views side of Attack 1's behavior
    /// </summary>
    /// <param name="currentActor"> Current position of attacker </param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns a list of delegates containing the attacks and in which order this enemy executes them.
    /// </summary>
    /// <returns> List of attack delegates </returns>
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

    /// <summary>
    /// Executes the animation of the passed attack reference at the passed position
    /// </summary>
    /// <param name="currentActor"> Position of attack </param>
    /// <param name="attack"> Reference to attack executed </param>
    /// <returns></returns>
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
}
