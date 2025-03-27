using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Side1", menuName = "Scriptable Objects/BattleActors/Heroes/Side1")]
public class Side1 : Hero
{
    public override void LoadStats()
    {
        HP = PlayerPrefs.GetFloat("Side1HP");
        MaxHP = party.side1Levels[party.GetSide1Level()].Max_HP;
        MaxAP = party.side1Levels[party.GetSide1Level()].Max_AP;
        Atk = party.side1Levels[party.GetSide1Level()].Atk;
        Def = party.side1Levels[party.GetSide1Level()].Def;
    }

    public override void SaveStats(int currentHP)
    {
        if (currentHP <= 0)
            PlayerPrefs.SetFloat("Side1HP", 0);
        else
            PlayerPrefs.SetFloat("Side1HP", currentHP);
        PlayerPrefs.Save();
    }

    public override int Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        if (currentPosition + 1 < 5 && enemies[currentPosition + 1] != null)
            enemies[currentPosition + 1].GetComponent<EnemyBehavior>().IncrementHP(-Atk);
        if (currentPosition - 1 > -1 && enemies[currentPosition - 1] != null)
            enemies[currentPosition - 1].GetComponent<EnemyBehavior>().IncrementHP(-Atk);
        return GetAPOfAttack1();
    }

    public override int Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        heroes[currentPosition].GetComponent<HeroBehavior>().GiveEffect(statusEffectManager.Defend());
        return GetAPOfAttack2();
    }

    public override int Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        GameObject tempEnemy = enemies[currentPosition];
        if (currentPosition > 0 && enemies[currentPosition] != null)
        {
            enemies[currentPosition] = enemies[currentPosition - 1];
            enemies[currentPosition - 1] = tempEnemy;
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
        List<int> enemies = new List<int>() { currentSpace + 1, currentSpace - 1 };
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
        List<int> heroes = new List<int>() { };
        List<int> enemies = new List<int>() { currentSpace };
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

    public override void ReplenishHP()
    {
        PlayerPrefs.SetFloat("Side1HP", party.mainLevels[party.GetMainLevel()].Max_HP);
        HP = PlayerPrefs.GetFloat("Side1HP");
    }

    public override string GetAttack1Tooltip()
    {
        return "AP[" + GetAPOfAttack1() + "]: " + "Deals damage to the left and right opposing enemies.";
    }

    public override string GetAttack2Tooltip()
    {
        return "AP[" + GetAPOfAttack2() + "]: " + "Raise the defense of this hero for one turn.";
    }

    public override string GetAttack3Tooltip()
    {
        return "AP[" + GetAPOfAttack3() + "]: " + "Deals damage to opposing enemy and pushes them rightward.";
    }
}
