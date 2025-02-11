using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "Evilman", menuName = "Scriptable Objects/Evilman")]
public class Evilman : Enemy
{
    //MAKE SURE TO CHANGE THIS WHEN ADDING NEW ATTACKS
    int totalUniqueAttacks = 1;

    /// <summary>
    /// Executes the models side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public override void Attack1(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        Debug.Log("Attack 1!");
    }

    /// <summary>
    /// Executes the views side of Attack 1's behavior
    /// </summary>
    /// <param name="currentActor"> Current position of attacker </param>
    /// <returns></returns>
    public override IEnumerator PlayAttack1Anim(int currentActor)
    {
        Debug.Log("Enemy Executing attack 1");
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
        }

        return attackList;
    }

    /// <summary>
    /// Executes the animation of the passed attack reference at the passed position
    /// </summary>
    /// <param name="currentActor"> Position of attack </param>
    /// <param name="attack"> Reference to attack executed </param>
    /// <returns></returns>
    public override IEnumerator PlayAttackAnim(int currentActor, Attack attack)
    {
        if (attack == Attack1)
        {
            yield return PlayAttack1Anim(currentActor);
        }
        else
        {
            yield return null;
        }
    }
}
