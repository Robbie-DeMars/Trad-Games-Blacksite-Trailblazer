using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Goober", menuName = "Scriptable Objects/Goober")]
public class Goober : Hero 
{
    public override int Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        if (enemies[currentPosition] !=  null)
            enemies[currentPosition].GetComponent<EnemyBehavior>().IncrementHP(-Atk);
        return GetAPOfAttack1();
    }

    public override int Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        return GetAPOfAttack2();
    }

    public override int Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        return GetAPOfAttack3();
    }

    public override int GetAPOfAttack1()
    {
        return 3;
    }

    public override int GetAPOfAttack2()
    {
        return 0;
    }

    public override int GetAPOfAttack3()
    {
        return 0;
    }

    public override List<List<int>> GetAttack1Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() {};
        List<int> enemies = new List<int>() { currentSpace };
        return new List<List<int>>() { heroes, enemies };
    }

    public override List<List<int>> GetAttack2Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { };
        List<int> enemies = new List<int>() { };
        return new List<List<int>>() { heroes, enemies };
    }

    public override List<List<int>> GetAttack3Targets(int currentSpace)
    {
        List<int> heroes = new List<int>() { };
        List<int> enemies = new List<int>() { };
        return new List<List<int>>() { heroes, enemies };
    }

    public override IEnumerator PlayAttack1Anim(int currentSpace)
    {
        Debug.Log("Animation Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Animation Over");
    }

   
    public override IEnumerator PlayAttack2Anim(int currentSpace)
    {
        Debug.Log("Animation Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Animation Over");
    }


    public override IEnumerator PlayAttack3Anim(int currentSpace)
    {
        Debug.Log("Animation Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Animation Over");
    }
}
