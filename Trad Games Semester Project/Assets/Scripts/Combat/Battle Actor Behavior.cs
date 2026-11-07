using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleActorBehavior : MonoBehaviour
{
    //Position where effects are to be displayed
    [SerializeField] public Transform effectDisplayPosition;
    //List of buffs/debuffs - tick at the start of every turn.
    public List<AgnosticEffect> aEffects = new List<AgnosticEffect>();
    //The display for the effects in the worldspace
    public List<GameObject> effects = new List<GameObject>();
    //Instance HP
    public float HP = 10;

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
    /// <summary>
    /// Makes the displays for the effects attached to this actor in the worldspace
    /// </summary>
    public abstract void UpdateEffectDisplay();

    /// <summary>
    /// Removes references to this effect from this hero
    /// 
    /// MODIFIES THE AEFFECTS LIST
    /// </summary>
    /// <param name="effect"></param>
    public virtual void RemoveEffect(StatusEffect effect)
    {
        int count = aEffects.Count;
        for (int x = 0; x < count; x++)
            if (aEffects[x] == effect)
            {
                aEffects.RemoveAt(x);
                x--;
                count--;
            }
        UpdateEffectDisplay();
    }

    /// <summary>
    /// Returns true of the passed status effect type exists on this character already.
    /// </summary>
    /// <param name="effect"></param>
    /// <returns></returns>
    public abstract bool HasStatusEffect(StatusEffect effect);
}
