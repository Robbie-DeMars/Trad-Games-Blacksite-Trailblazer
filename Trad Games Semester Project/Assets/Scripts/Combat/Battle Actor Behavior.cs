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
    public virtual void UpdateEffectDisplay()
    {
        float offset = 0.2f;
        float totalWidth = (aEffects.Count - 1) * offset; // Total width of the agnostic effects
        float startX = effectDisplayPosition.position.x - (totalWidth / 2); // Center align

        //Remove existing displays
        for (int x = 0; x < effects.Count; x++)
        {
            Destroy(effects[x]);
        }
        effects.Clear();

        //Remake them with the current information
        for (int x = 0; x < aEffects.Count; x++)
        {
            effects.Add(Instantiate(aEffects[x].effectImage, this.transform));
            effects[x].transform.position = new Vector3(startX + x * offset, effectDisplayPosition.position.y, effectDisplayPosition.position.z);
        }
    }
}
