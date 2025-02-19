using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : ScriptableObject
{
    //Delegate used to hold attacks from enemies.
    public delegate void Attack(GameObject[] heroes, GameObject[] enemies, int currentPosition);
    
    [SerializeField] public int attacks = 1;
    [SerializeField] public int MaxHP = 10;
    [SerializeField] public int Atk = 5;
    [SerializeField] public int Def = 2;


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

    /// <summary>
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public abstract List<List<int>> GetAttack1Targets(int currentSpace);

    /// <summary>
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side 
    /// </summary>
    /// <param name="currentSpace">Current position of attack</param>
    /// <param name="attack"> Attack to return, defaults to 1</param>
    /// <returns></returns>
    public abstract List<List<int>> GetAttackTargets(int currentSpace, int attack);

    /// <summary>
    /// Returns the number of the given attack, or 0
    /// </summary>
    /// <param name="attack"></param>
    /// <returns></returns>
    public abstract int GetAttackNumber(Attack attack);


}
