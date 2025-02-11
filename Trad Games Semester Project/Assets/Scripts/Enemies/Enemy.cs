using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : ScriptableObject
{
    //Delegate used to hold attacks from enemies.
    public delegate void Attack(GameObject[] heroes, GameObject[] enemies, int currentPosition);
    
    [SerializeField] public int attacks = 1;
    [SerializeField] public int MaxHP = 10;
    

    /// <summary>
    /// Executes the model's side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public abstract void Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition);

    /// <summary>
    /// Resets the attacks this enemy will execute.
    /// </summary>
    /// <returns></returns>
    public abstract List<Attack> PrepareAttacks();

    /// <summary>
    /// Executes the views side of Attack 1's behavior
    /// </summary>
    /// <param name="currentActor"> Current position of attacker </param>
    /// <returns></returns>
    public abstract IEnumerator PlayAttack1Anim(int currentPosition);

    /// <summary>
    /// Executes the animation of the passed attack reference at the passed position
    /// </summary>
    /// <param name="currentActor"> Position of attack </param>
    /// <param name="attack"> Reference to attack executed </param>
    /// <returns></returns>
    public abstract IEnumerator PlayAttackAnim(int currentPosition, Attack attack);
}
