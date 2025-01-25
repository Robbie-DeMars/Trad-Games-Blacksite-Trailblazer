using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Hero", menuName = "Scriptable Objects/Hero")]
public abstract class Hero : ScriptableObject
{
    public abstract IEnumerator Attack1(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies);
    public abstract IEnumerator Attack2(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies);
    public abstract IEnumerator Attack3(GameObject thisHero, CombatManager manager, int position, GameObject[] enemies);
    public abstract List<int> DisplayAttack1Targets(GameObject thisHero, int position, GameObject[] enemies);
    public abstract List<int> DisplayAttack2Targets(GameObject thisHero, int position, GameObject[] enemies);
    public abstract List<int> DisplayAttack3Targets(GameObject thisHero, int position, GameObject[] enemies);
    public abstract void LoadCombatStatus(GameObject thisHero);
    public abstract string GetName(GameObject thisHero);
    public abstract int GetHP(GameObject thisHero);
    public abstract int GetHPMax(GameObject thisHero);
    public abstract void incrementHP(int ammount);
    public abstract void setHP(int ammount);
    public abstract void setHPMax(int ammount);
}
