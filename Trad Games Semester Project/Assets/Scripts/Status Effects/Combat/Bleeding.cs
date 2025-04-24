using UnityEngine;

[CreateAssetMenu(fileName = "Bleeding", menuName = "Scriptable Objects/StatusEffects/Bleeding")]
public class Bleeding : AgnosticEffect
{
    public override void ClearEffect(BattleActorBehavior actor)
    {
        actor.RemoveEffect(this);
    }

    public override void DoEffect(BattleActorBehavior actor)
    {
        rounds--;
        if (rounds < 0)
        {
            ClearEffect(actor);
        }
        actor.IncrementHP(-1);
        roundsInEffect++;
    }

    public override bool HasMoreRounds()
    {
        if (rounds < 0)
        {
            return false;
        }
        return true;
    }
}
