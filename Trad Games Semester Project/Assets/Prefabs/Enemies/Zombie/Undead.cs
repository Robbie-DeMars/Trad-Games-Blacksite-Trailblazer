using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

[CreateAssetMenu(fileName = "Undead", menuName = "Scriptable Objects/BattleActors/Enemies/Undead")]
public class Undead : Enemy
{
    //MAKE SURE TO CHANGE THIS WHEN ADDING NEW ATTACKS
    int totalUniqueAttacks = 3;

    //Implementation-Dependant
    public override void Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        //Hit in front
        if (heroes[currentPosition] != null)
            heroes[currentPosition].GetComponent<HeroBehavior>().IncrementHP(-Atk);
    }

    public void Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        //Move left
        GameObject thisEnemy = enemies[currentPosition];
        if (currentPosition != 0)
        {
            enemies[currentPosition] = enemies[currentPosition - 1];
            enemies[currentPosition - 1] = thisEnemy;
        }
    }

    public void Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        //Move right
        GameObject thisEnemy = enemies[currentPosition];
        if (currentPosition != enemies.Length)
        {
            enemies[currentPosition] = enemies[currentPosition + 1];
            enemies[currentPosition + 1] = thisEnemy;
        }
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

    public IEnumerator PlayAttack2Anim(int currentPosition)
    {
        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator PlayAttack3Anim(int currentPosition)
    {
        yield return new WaitForSeconds(0.2f);
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
            else if (chosenAttack == 1)
            {
                attackList.Add(Attack2);
            }
            else if (chosenAttack == 2)
            {
                attackList.Add(Attack3);
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
        else if (attack == Attack3)
        {
            yield return PlayAttack3Anim(currentPosition);
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
        else if (attack == Attack3)
            return GetAttack3Targets(currentPosition);
        else
            return new List<List<int>>();
    }

    public override List<List<int>> GetAttack1Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { currentSpace };
        List<int> enemies = new List<int>() { };
        return new List<List<int>>() { heroes, enemies };
    }

    public List<List<int>> GetAttack2Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { };
        List<int> enemies = new List<int>() { currentSpace, currentSpace - 1 };
        return new List<List<int>>() { heroes, enemies };
    }

    public List<List<int>> GetAttack3Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { };
        List<int> enemies = new List<int>() { currentSpace, currentSpace + 1 };
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
            return GetAttack1Targets(currentSpace);
        }
        else if (attack == 3)
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
        else if (attack == Attack2)
            return 2;
        else if (attack == Attack3)
            return 3;
        else
            return 0;
    }

    public override string GetAttackTooltip(Attack attack)
    {
        if (attack == Attack1)
            return "Undead: [Reave] Deals decent damage to the opposing hero.";
        else if (attack == Attack2)
            return "Undead: [Shamble] Undead moves left.";
        else if (attack == Attack3)
            return "Undead: [Hobble] Undead moves right.";
        else
            return "";
    }
}
