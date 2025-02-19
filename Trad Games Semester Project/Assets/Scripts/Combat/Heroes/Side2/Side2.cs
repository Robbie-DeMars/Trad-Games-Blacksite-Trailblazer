using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Side2", menuName = "Scriptable Objects/Side2")]
public class Side2 : Hero
{
    public override void LoadStats()
    {
        HP = PlayerPrefs.GetInt("Side2HP");
        MaxHP = party.side2Stats[1];
        MaxAP = party.side2Stats[2];
        Atk = party.side2Stats[3];
        Def = party.side2Stats[4];
    }

    public override void SaveStats(int currentHP)
    {
        if (currentHP <= 0)
            PlayerPrefs.SetInt("Side2HP", 0);
        else
            PlayerPrefs.SetInt("Side2HP", currentHP);
        PlayerPrefs.Save();
    }

    public override int Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        if ((currentPosition >= 0 && currentPosition < enemies.Length) && enemies[currentPosition] != null)
            enemies[currentPosition].GetComponent<EnemyBehavior>().IncrementHP(-Atk);
        if ((currentPosition + 1 >= 0 && currentPosition + 1 < enemies.Length) && enemies[currentPosition + 1] != null)
            enemies[currentPosition + 1].GetComponent<EnemyBehavior>().IncrementHP(-Atk);
        if ((currentPosition - 1 >= 0 && currentPosition - 1 < enemies.Length) && enemies[currentPosition - 1] != null)
            enemies[currentPosition - 1].GetComponent<EnemyBehavior>().IncrementHP(-Atk);

        if (currentPosition > 0 && currentPosition < enemies.Length - 1) {
            GameObject temp = enemies[currentPosition];
            enemies[currentPosition] = enemies[currentPosition + 1];
            enemies[currentPosition + 1] = enemies[currentPosition - 1];
            enemies[currentPosition - 1] = temp;
        }
        else if (currentPosition == enemies.Length - 1)
        {
            GameObject temp = enemies[currentPosition];
            enemies[currentPosition] = enemies[currentPosition - 1];
            enemies[currentPosition - 1] = temp;
        }
        else if (currentPosition == 0) {
            GameObject temp = enemies[currentPosition];
            enemies[currentPosition] = enemies[currentPosition + 1];
            enemies[currentPosition + 1] = temp;
        }

        return GetAPOfAttack1();
    }

    public override int Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        heroes[currentPosition].GetComponent<HeroBehavior>().IncrementHP(2);
        return GetAPOfAttack2();
    }

    public override int Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        List<GameObject> temp = new List<GameObject>();
        int enemyCount = 0;
        for (int x = 0; x < enemies.Length; x++)
        {
            if (enemies[x] != null)
            {
                temp.Add(enemies[x]);
                enemies[x] = null;
                enemyCount++;
            }
        }
        if (enemyCount == 1) { 
            temp.Insert(0, null);
            temp.Insert(0, null);
            temp.Insert(0, null);
        }
        if (enemyCount == 2)
        {
            temp.Insert(0, null);
            temp.Insert(0, null);
        }
        if (enemyCount == 3) {
            temp.Insert(0, null);
        }
        for (int x = 0; x < temp.Count; x++)
        {
            enemies[x] = temp[x];
        }
        return GetAPOfAttack3();
    }

    public override int GetAPOfAttack1()
    {
        return 4;
    }

    public override int GetAPOfAttack2()
    {
        return 10;
    }

    public override int GetAPOfAttack3()
    {
        return 6;
    }

    public override List<List<int>> GetAttack1Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { };
        List<int> enemies = new List<int>() { currentSpace, currentSpace + 1, currentSpace - 1 };
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
        List<int> enemies = new List<int>() {currentSpace, currentSpace + 1, currentSpace + 2, currentSpace + 3, currentSpace + 4, currentSpace + 5, currentSpace - 1, currentSpace - 2, currentSpace - 3, currentSpace - 4, currentSpace - 5 };
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
