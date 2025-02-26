using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCharacter", menuName = "Scriptable Objects/MainCharacter")]
public class MainCharacter : Hero
{
    public override void LoadStats()
    {
        HP = PlayerPrefs.GetInt("MainHP");
        Debug.Log(HP);
        MaxHP = party.mainStats[1];
        MaxAP = party.mainStats[2];
        Atk = party.mainStats[3];
        Def = party.mainStats[4];
    }

    public override void SaveStats(int currentHP)
    {
        if (currentHP <= 0)
            PlayerPrefs.SetInt("MainHP", 1);
        else
            PlayerPrefs.SetInt("MainHP", currentHP);
        PlayerPrefs.Save();
    }

    public override int Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        if (enemies[currentPosition] != null)
            enemies[currentPosition].GetComponent<EnemyBehavior>().IncrementHP(-Atk);
        return GetAPOfAttack1();
    }

    public override int Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        heroes[currentPosition].GetComponent<HeroBehavior>().GiveEffect(statusEffectManager.Defend());
        return GetAPOfAttack2();
    }

    public override int Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        if (currentPosition != 4)
        {
            while (currentPosition != 4)
            {
                GameObject temp = heroes[currentPosition];
                heroes[currentPosition] = heroes[currentPosition + 1];
                heroes[currentPosition + 1] = temp;
                currentPosition++;
            }
        }
        else
        {
            while (currentPosition != 0)
            {
                GameObject temp = heroes[currentPosition];
                heroes[currentPosition] = heroes[currentPosition - 1];
                heroes[currentPosition - 1] = temp;
                currentPosition--;
            }
        }
        return GetAPOfAttack3();
    }

    public override int GetAPOfAttack1()
    {
        return attack1APCost;
    }

    public override int GetAPOfAttack2()
    {
        return attack2APCost;
    }

    public override int GetAPOfAttack3()
    {
        return attack3APCost;
    }

    public override List<List<int>> GetAttack1Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { };
        List<int> enemies = new List<int>() { currentSpace };
        return new List<List<int>>() { heroes, enemies };
    }

    public override List<List<int>> GetAttack2Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { currentSpace };
        List<int> enemies = new List<int>() { };
        return new List<List<int>>() { heroes, enemies };
    }

    public override List<List<int>> GetAttack3Targets(int currentSpace)
    {
        List<int> heroes;
        if (currentSpace != 4)
            heroes = new List<int>() { currentSpace, currentSpace + 1, currentSpace + 2, currentSpace + 3, currentSpace + 4, currentSpace + 5};
        else
            heroes = new List<int>() { currentSpace, currentSpace - 1, currentSpace - 2, currentSpace - 3, currentSpace - 4, currentSpace - 5 };
        List<int> enemies = new List<int>() { };
        return new List<List<int>>() { heroes, enemies };
    }

    public override IEnumerator PlayAttack1Anim(int currentSpace)
    {
        View view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        List<List<int>> targets = GetAttack1Targets(currentSpace);

        //Activate particles at target's positions
        for (int i = 0; i < targets[0].Count; i++)
            if (targets[0][i] < 5 && targets[0][i] > -1)
                view.ActivateHeroParticle(targets[0][i]);
        for (int i = 0; i < targets[1].Count; i++)
            if (targets[1][i] < 5 && targets[1][i] > -1)
                view.ActivateEnemyParticle(targets[1][i]);

        yield return new WaitForSeconds(0.2f);
        view.clearParticles();
    }


    public override IEnumerator PlayAttack2Anim(int currentSpace)
    {
        View view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        List<List<int>> targets = GetAttack2Targets(currentSpace);

        //Activate particles at target's positions
        for (int i = 0; i < targets[0].Count; i++)
            if (targets[0][i] < 5 && targets[0][i] > -1)
                view.ActivateHeroParticle(targets[0][i]);
        for (int i = 0; i < targets[1].Count; i++)
            if (targets[1][i] < 5 && targets[1][i] > -1)
                view.ActivateEnemyParticle(targets[1][i]);

        yield return new WaitForSeconds(0.2f);
        view.clearParticles();
    }


    public override IEnumerator PlayAttack3Anim(int currentSpace)
    {
        View view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        List<List<int>> targets = GetAttack3Targets(currentSpace);

        //Activate particles at target's positions
        for (int i = 0; i < targets[0].Count; i++)
            if (targets[0][i] < 5 && targets[0][i] > -1)
                view.ActivateHeroParticle(targets[0][i]);
        for (int i = 0; i < targets[1].Count; i++)
            if (targets[1][i] < 5 && targets[1][i] > -1)
                view.ActivateEnemyParticle(targets[1][i]);

        yield return new WaitForSeconds(0.2f);
        view.clearParticles();
    }
}
