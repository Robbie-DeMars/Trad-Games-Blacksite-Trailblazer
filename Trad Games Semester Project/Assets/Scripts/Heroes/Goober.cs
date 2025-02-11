using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Goober", menuName = "Scriptable Objects/Goober")]
public class Goober : Hero 
{
    /// <summary>
    /// Executes the model's side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public override void Attack1(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        if (enemies[currentPosition] !=  null)
            enemies[currentPosition].GetComponent<EnemyBehavior>().IncrementHP(-5);
    }

    /// <summary>
    /// Executes the model's side of Attack 2's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public override void Attack2(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {

    }

    /// <summary>
    /// Executes the model's side of Attack 3's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public override void Attack3(GameObject[] heroes, GameObject[] enemies, int currentPosition)
    {
        
    }

    /// <summary>
    /// Executes the view's side of Attack 1's behavior
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns></returns>
    public override IEnumerator PlayAttack1Anim(int currentSpace)
    {
        Debug.Log("Animation Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Animation Over");
    }

    /// <summary>
    /// Executes the view's side of Attack 2's behavior
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns></returns>
    public override IEnumerator PlayAttack2Anim(int currentSpace)
    {
        Debug.Log("Animation Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Animation Over");
    }

    /// <summary>
    /// Executes the view's side of Attack 3's behavior
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns></returns>
    public override IEnumerator PlayAttack3Anim(int currentSpace)
    {
        Debug.Log("Animation Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("Animation Over");
    }
}
