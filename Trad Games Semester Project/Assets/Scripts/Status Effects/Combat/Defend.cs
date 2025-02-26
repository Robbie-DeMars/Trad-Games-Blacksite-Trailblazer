using UnityEngine;

[CreateAssetMenu(fileName = "Defend", menuName = "Scriptable Objects/Defend")]
public class Defend : AgnosticEffect
{
    public override void ClearEffect(BattleActorBehavior actor)
    {
        actor.GetBattleActor().Def /= (2 * roundsInEffect);
        Destroy(this);
    }

    public override void DoEffect(BattleActorBehavior actor)
    {
        rounds--;
        if (HasMoreRounds())
        {
            ClearEffect(actor);
        }
        actor.GetBattleActor().Def *= 2;
        roundsInEffect++;
    }
}
