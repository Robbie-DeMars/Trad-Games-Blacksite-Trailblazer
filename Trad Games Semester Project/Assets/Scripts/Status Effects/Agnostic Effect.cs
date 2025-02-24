using UnityEngine;

[CreateAssetMenu(fileName = "AgnosticEffect", menuName = "Scriptable Objects/AgnosticEffect")]
public abstract class AgnosticEffect : StatusEffect
{
    [SerializeField] public int rounds = 1;
    public int roundsInEffect = 0;
    public abstract void DoEffect(BattleActorBehavior actor);
    public abstract void ClearEffect(BattleActorBehavior actor);
}
