using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kitsune", menuName = "Scriptable Objects/BattleActors/Enemies/Kitsune")]
public class Kitsune : Enemy
{
    //MAKE SURE TO CHANGE THIS WHEN ADDING NEW ATTACKS
    int totalUniqueAttacks = 1;

    //Implementation-Dependant
    public override void Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        //Hit in front
        if (heroes[currentPosition] != null)
            heroes[currentPosition].GetComponent<HeroBehavior>().IncrementHP(-Atk);
    }


    public override IEnumerator PlayAttack1Anim(int currentPosition)
    {
        View view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        List<List<int>> targets = GetAttack1Targets(currentPosition);

        //Activate particles at target's positions
        for (int i = 0; i < targets[0].Count; i++)
            view.ActivateHeroParticle(targets[0][i]);
        for (int i = 0; i < targets[1].Count; i++)
            view.ActivateEnemyParticle(targets[1][i]);

        yield return new WaitForSeconds(0.2f);
        view.clearParticles();
    }

    public override List<Attack> PrepareAttacks()
    {
        List<Attack> attackList = new List<Attack>();
        int randAttackNum = Random.Range(1, attacks + 1);
        for (int x = 0; x < randAttackNum; x++)
        {
            int chosenAttack = Random.Range(0, totalUniqueAttacks);

            if (chosenAttack == 0)
            {
                attackList.Add(Attack1);
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
        else
            return new List<List<int>>();
    }

    public override List<List<int>> GetAttack1Targets(int currentSpace)
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
        else
        {
            return GetAttack1Targets(currentSpace);
        }
    }

    public override int GetAttackNumber(Attack attack)
    {
        if (attack == Attack1)
            return 1;
        else
            return 0;
    }

    public override string GetAttackTooltip(Attack attack)
    {
        if (attack == Attack1)
            return "Kitsune: (Jab) Deals decent damage to the opposing hero.";
        else
            return "";
    }
}
