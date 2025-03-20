using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : BattleActor
{
    //Max AP of character
    public int MaxAP = 10;
    //Party state
    public PartyState party;
    //Status effect definitions
    [SerializeField] public StatusEffectManager statusEffectManager;
    public int attack1APCost = 0;
    public int attack2APCost = 0;
    public int attack3APCost = 0;

    /// <summary>
    /// Loads the stats into this character from the party state.
    /// </summary>
    public abstract void LoadStats();

    /// <summary>
    /// Replenishes this hero's HP and saves it.
    /// </summary>
    public abstract void ReplenishHP();

    /// <summary>
    /// Loads the stats into this character to the party state.
    /// </summary>
    public abstract void SaveStats(int currentHP);

    /// <summary>
    /// Executes the model's side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    /// <returns> AP Cost </returns>
    public abstract int Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition);

    /// <summary>
    /// Executes the model's side of Attack 2's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    /// <returns> AP Cost </returns>
    public abstract int Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition);

    /// <summary>
    /// Executes the model's side of Attack 3's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    /// <returns> AP Cost </returns>
    public abstract int Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition);

    /// <summary>
    /// Returns the AP cost of executing Attack 1
    /// </summary>
    /// <returns> AP Cost </returns>
    public abstract int GetAPOfAttack1();

    /// <summary>
    /// Returns the AP cost of executing Attack 1
    /// </summary>
    /// <returns> AP Cost </returns>
    public abstract int GetAPOfAttack2();

    /// <summary>
    /// Returns the AP cost of executing Attack 1
    /// </summary>
    /// <returns> AP Cost </returns>
    public abstract int GetAPOfAttack3();

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
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public abstract List<List<int>> GetAttack2Targets(int currentSpace);

    /// <summary>
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public abstract List<List<int>> GetAttack3Targets(int currentSpace);


}
