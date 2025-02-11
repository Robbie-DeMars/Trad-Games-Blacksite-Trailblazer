using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State {ATTACK, MOVE, PAUSED, WIN, LOSE}
    protected static State currentState;
}
