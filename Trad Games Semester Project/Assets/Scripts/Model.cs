using UnityEngine;

public class Model : MonoBehaviour
{
    enum State {ATTACK, MOVE, ANIMATING}
    private State currentState;

    [SerializeField] GameObject[] heroes;
    [SerializeField] GameObject[] enemies;

    int currentActor = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = State.ATTACK;
        Instantiate(heroes[0]);
    }

    public void Attack1()
    {
        if (currentState != State.ATTACK)
            return;
        heroes[currentActor].GetComponent<HeroBehavior>().Attack1();
        currentState = State.ANIMATING;
        StartCoroutine(heroes[currentActor].GetComponent<HeroBehavior>().PlayAttack1Anim());
        //TODO
    }

    public void Attack2()
    {
        if (currentState != State.ATTACK)
            return;
        //TODO
    }

    public void Attack3()
    {
        if (currentState != State.ATTACK)
            return;
        //TODO
    }

    public void Move()
    {
        //TODO
    }

    public void PassTurn()
    {
        //TODO
    }



}
