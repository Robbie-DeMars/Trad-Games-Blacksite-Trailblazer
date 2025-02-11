using System.Collections;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{

    [SerializeField] Hero Hero;
    public bool takingTurn = false;
    /// <summary>
    /// Executes the model's side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public void Attack1(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        Hero.Attack1(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the model's side of Attack 2's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public void Attack2(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        Hero.Attack2(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the model's side of Attack 3's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public void Attack3(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        Hero.Attack3(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the view's side of Attack 1's behavior
    /// </summary>
    /// <param name="currentPosition"> Current position of attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack1Anim(int currentPosition)
    {
        yield return Hero.PlayAttack1Anim(currentPosition);
    }

    /// <summary>
    /// Executes the view's side of Attack 2's behavior
    /// </summary>
    /// <param name="currentPosition"> Current position of attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack2Anim(int currentPosition)
    {
        yield return Hero.PlayAttack2Anim(currentPosition);
    }

    /// <summary>
    /// Executes the view's side of Attack 3's behavior
    /// </summary>
    /// <param name="currentPosition"> Current position of attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack3Anim(int currentPosition)
    {
        yield return Hero.PlayAttack3Anim(currentPosition);
    }
}
