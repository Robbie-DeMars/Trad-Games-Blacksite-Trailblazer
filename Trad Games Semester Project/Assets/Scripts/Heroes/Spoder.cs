using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

[CreateAssetMenu(fileName = "Spoder", menuName = "Scriptable Objects/Spoder")]
public class Spoder : Hero
{
    private string name = "Spoder";
    private int HP = 10;
    private int maxHP = 10;
    public override IEnumerator Attack1(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies)
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(name + " performed attack 1!");
    }
    
    public override List<int> DisplayAttack1Targets(GameObject thisHero, int position, GameObject[] enemies)
    {
        return new List<int>() {position};
    }

    public override IEnumerator Attack2(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies)
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(name + " performed Attack 2!");
        
    }
    public override List<int> DisplayAttack2Targets(GameObject thisHero, int position, GameObject[] enemies)
    {
        return new List<int>() { position, position + 1, position - 1};
    }

    public override IEnumerator Attack3(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies)
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log(name + " performed attack 3!");
    }
    public override List<int> DisplayAttack3Targets(GameObject thisHero, int position, GameObject[] enemies)
    {
        return new List<int>() { position };
    }

    public override void LoadCombatStatus(GameObject thisHero)
    {
        Debug.Log("Loading status of " + name + ".");
        HP = 10;
    }

    public override string GetName(GameObject thisHero)
    {
        return name;
    }

    public override int GetHP(GameObject thisHero)
    {
        return HP;
    }
    public override int GetHPMax(GameObject thisHero)
    {
        return maxHP;
    }

    public override void incrementHP(int ammount)
    {
        HP += ammount;
    }
    public override void setHP(int ammount)
    {
        HP = ammount;
    }

    public override void setHPMax(int ammount)
    {
        maxHP = ammount;
    }
}
