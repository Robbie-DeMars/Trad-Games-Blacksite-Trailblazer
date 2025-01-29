using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public abstract class Enemy : ScriptableObject
{
    public abstract IEnumerator Attack1(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies, GameObject[] allies);
    public abstract IEnumerator Attack2(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies, GameObject[] allies);
    public abstract IEnumerator Attack3(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies, GameObject[] allies);
    public abstract List<int> DisplayAttack1Targets(GameObject thisHero, int position, GameObject[] enemies);
    public abstract List<int> DisplayAttack2Targets(GameObject thisHero, int position, GameObject[] enemies);
    public abstract List<int> DisplayAttack3Targets(GameObject thisHero, int position, GameObject[] enemies);
    public abstract string GetName(GameObject thisHero);
    public abstract int GetHP(GameObject thisHero);
    public abstract int GetHPMax(GameObject thisHero);
    public abstract void SetHP(int ammount);
    public abstract void SetHPMax(int ammount);
}

