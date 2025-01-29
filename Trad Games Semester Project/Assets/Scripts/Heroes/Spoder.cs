using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

[CreateAssetMenu(fileName = "Spoder", menuName = "Scriptable Objects/Spoder")]
public class Spoder : Hero
{
    private string name = "Spoder";
    public int HP = 5;
    public int maxHP = 5;
    /*
     * Defines the behavior of this specific character's first attack.
     */
    public override IEnumerator Attack1(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies, GameObject[] allies)
    {
        yield return new WaitForSeconds(0.5f);

        manager.incrementHeat(15);

        if ((position >= 0 && position < enemies.Length) && enemies[position] != null)
            enemies[position].GetComponent<CharacterCombatManager>().incrementHP(-1);

        Debug.Log(name + " performed attack 1!");
    }
    /*
     * Sends the positions this character's first attack targets, enemies are based on position, heroes are based on position + 100 
     */
    public override List<int> DisplayAttack1Targets(GameObject thisHero, int position, GameObject[] enemies)
    {
        return new List<int>() {position};
    }
    /*
     * Defines the behavior of this specific character's second attack.
     */
    public override IEnumerator Attack2(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies, GameObject[] allies)
    {
        yield return new WaitForSeconds(0.5f);

        manager.incrementHeat(50);

        if ((position >= 0 && position < enemies.Length) && enemies[position] != null)
            enemies[position].GetComponent<CharacterCombatManager>().incrementHP(-2);
        if ((position + 1 >= 0 && position + 1 < enemies.Length) && enemies[position + 1] != null)
            enemies[position + 1].GetComponent<CharacterCombatManager>().incrementHP(-2);
        if ((position - 1 >= 0 && position - 1 < enemies.Length) && enemies[position - 1] != null)
            enemies[position - 1].GetComponent<CharacterCombatManager>().incrementHP(-2);

        Debug.Log(name + " performed Attack 2!");
    }
    /*
     * Sends the positions this character's second attack targets, enemies are based on position, heroes are based on position + 100 
     */
    public override List<int> DisplayAttack2Targets(GameObject thisHero, int position, GameObject[] enemies)
    {
        return new List<int>() { position, position + 1, position - 1};
    }
    /*
     * Defines the behavior of this specific character's third attack.
     */
    public override IEnumerator Attack3(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies, GameObject[] allies)
    {
        yield return new WaitForSeconds(0.5f);

        manager.incrementHeat(60);

        if ((position + 1 >= 0 && position + 1 < allies.Length) && allies[position + 1] != null)
            allies[position + 1].GetComponent<CharacterCombatManager>().incrementHP(2);
        if ((position - 1 >= 0 && position - 1 < allies.Length) && allies[position - 1] != null)
            allies[position - 1].GetComponent<CharacterCombatManager>().incrementHP(2);

        Debug.Log(name + " performed attack 3!");
    }
    /*
     * Sends the positions this character's third attack targets, enemies are based on position, heroes are based on position + 100 
     */
    public override List<int> DisplayAttack3Targets(GameObject thisHero, int position, GameObject[] enemies)
    {
        return new List<int>() { position + 101, position + 99 };
    }

    public override void LoadCombatStatus(GameObject thisHero)
    {
        Debug.Log("Loading status of " + name + ".");
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

    public override void SetHP(int ammount)
    {
        HP = ammount;
    }

    public override void SetHPMax(int ammount)
    {
        maxHP = ammount;
    }
}
