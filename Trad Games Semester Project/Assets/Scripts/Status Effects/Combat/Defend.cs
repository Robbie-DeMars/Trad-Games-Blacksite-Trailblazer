using UnityEngine;

[CreateAssetMenu(fileName = "Defend", menuName = "Scriptable Objects/Defend")]
public class Defend : AgnosticEffect
{
    public override void ClearEffect(BattleActorBehavior actor)
    {
        if (roundsInEffect != 0)
            actor.GetBattleActor().Def /= (2 * roundsInEffect);
        actor.RemoveEffect(this);
    }

    public override void DoEffect(BattleActorBehavior actor)
    {
        rounds--;
        if (rounds < 0)
        {
            ClearEffect(actor);
        }
        actor.GetBattleActor().Def *= 2;
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
