using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State {ATTACK, MOVE, PAUSED}
    protected static State currentState;
}
