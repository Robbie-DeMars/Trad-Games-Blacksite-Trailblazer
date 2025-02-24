using UnityEngine;

[CreateAssetMenu(fileName = "Defend", menuName = "Scriptable Objects/Defend")]
public class Defend : AgnosticEffect
{
    public override void ClearEffect(BattleActorBehavior actor)
    {
        actor.GetBattleActor().Def /= (2 * roundsInEffect);
    }

    public override void DoEffect(BattleActorBehavior actor)
    {
        rounds--;
        if (rounds < 0)
        {
            ClearEffect(actor);
            Destroy(this);
        }
        actor.GetBattleActor().Def *= 2;
        roundsInEffect++;
    }
}
