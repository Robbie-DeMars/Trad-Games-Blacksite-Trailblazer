using System.Collections;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{

    [SerializeField] Hero hero;
    [SerializeField] HealthBar HPBar;
    private float HP;
    public bool takingTurn = false;

    private void Start()
    {
        HP = hero.MaxHP;
        HPBar.SetMaxHealth(hero.MaxHP);
    }

    /// <summary>
    /// Executes the model's side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public void Attack1(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        hero.Attack1(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the model's side of Attack 2's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public void Attack2(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        hero.Attack2(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the model's side of Attack 3's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    public void Attack3(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        hero.Attack3(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the view's side of Attack 1's behavior
    /// </summary>
    /// <param name="currentPosition"> Current position of attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack1Anim(int currentPosition)
    {
        yield return hero.PlayAttack1Anim(currentPosition);
    }

    /// <summary>
    /// Executes the view's side of Attack 2's behavior
    /// </summary>
    /// <param name="currentPosition"> Current position of attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack2Anim(int currentPosition)
    {
        yield return hero.PlayAttack2Anim(currentPosition);
    }

    /// <summary>
    /// Executes the view's side of Attack 3's behavior
    /// </summary>
    /// <param name="currentPosition"> Current position of attack </param>
    /// <returns></returns>
    public IEnumerator PlayAttack3Anim(int currentPosition)
    {
        yield return hero.PlayAttack3Anim(currentPosition);
    }

    /// <summary>
    /// Increments this hero's hp by the passed ammount and updates its HP bar
    /// </summary>
    /// <param name="ammount"></param>
    public void IncrementHP(float ammount)
    {
        HP += ammount;
        HPBar.SetHealth(HP);
        if (HP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Removes this hero from battle
    /// </summary>
    public void Die()
    {
        HPBar.Die();
        Destroy(this.gameObject);
        Destroy(this);
    }
}
