using System.Collections;
using UnityEngine;

public abstract class Hero : ScriptableObject
{

    [SerializeField] public int MaxHP = 10;


    /// <summary>
    /// Executes the model's side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public abstract void Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition);

    /// <summary>
    /// Executes the model's side of Attack 2's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public abstract void Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition);

    /// <summary>
    /// Executes the model's side of Attack 3's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public abstract void Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition);
    
    /// <summary>
    /// Executes the view's side of Attack 1's behavior
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns></returns>
    public abstract IEnumerator PlayAttack1Anim(int currentSpace);

    /// <summary>
    /// Executes the view's side of Attack 2's behavior
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns></returns>
    public abstract IEnumerator PlayAttack2Anim(int currentSpace);

    /// <summary>
    /// Executes the view's side of Attack 3's behavior
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns></returns>
    public abstract IEnumerator PlayAttack3Anim(int currentSpace);
}
