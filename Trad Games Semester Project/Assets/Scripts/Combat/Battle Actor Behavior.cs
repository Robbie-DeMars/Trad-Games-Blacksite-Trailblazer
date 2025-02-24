using System;
using UnityEngine;

public abstract class BattleActorBehavior : MonoBehaviour
{
    /// <summary>
    /// Increments this actor's hp by the passed ammount and updates its HP bar
    /// </summary>
    /// <param name="ammount"></param>
    public abstract void IncrementHP(float ammount);
    /// <summary>
    /// Removes this actor from battle
    /// </summary>
    public abstract void Die();
    /// <summary>
    /// Returns the hero or enemy of this actor casted to a BattleActor
    /// </summary>
    /// <returns></returns>
    public abstract BattleActor GetBattleActor();
    /// <summary>
    /// Gives the passed debuff to the actor.
    /// </summary>
    /// <param name="effect"></param>
    public abstract void GiveEffect(StatusEffect effect);
    /// <summary>
    /// Ticks the debuffs one turn forward
    /// </summary>
    public abstract void SettleEffects();

    /// <summary>
    /// Removes all non persistant effects
    /// </summary>
    public abstract void ClearEffects();
}
