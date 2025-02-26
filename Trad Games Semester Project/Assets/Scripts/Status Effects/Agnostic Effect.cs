using UnityEngine;

[CreateAssetMenu(fileName = "AgnosticEffect", menuName = "Scriptable Objects/AgnosticEffect")]
public abstract class AgnosticEffect : StatusEffect
{
    //Ammount of rounds this will take effect (note the times this will affect the player is rounds + 1)
    [SerializeField] public int rounds = 1;

    //Tracks the number of rounds this effect has been in play
    public int roundsInEffect = 0;

    //The ingame display for the effect
    [SerializeField] public GameObject effectImage;

    /// <summary>
    /// Does the effect of this attack to this Battle Actor
    /// </summary>
    /// <param name="actor"></param>
    public abstract void DoEffect(BattleActorBehavior actor);
    /// <summary>
    /// Undoes the effect of this buff / debuff if nessesary, then destoys this instance
    /// </summary>
    /// <param name="actor"></param>
    public abstract void ClearEffect(BattleActorBehavior actor);
    /// <summary>
    /// Returns false if rounds left is less than 0, implying this efects lifetime is up
    /// </summary>
    /// <returns></returns>
    public virtual bool HasMoreRounds()
    {
        if (rounds < 0)
        { 
            return false;
        }
        return true;
    }
}
