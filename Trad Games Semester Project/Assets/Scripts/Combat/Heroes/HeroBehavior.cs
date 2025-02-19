using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{

    [SerializeField] public Hero hero;
    [SerializeField] HealthBar HPBar;
    private float HP;

    private int AP;

    public bool takingTurn = false;

    private void Awake()
    {
        hero.LoadStats();
        HPBar.SetMaxHealth(hero.MaxHP);
        AP = hero.MaxAP;
        HP = hero.HP;
        HPBar.SetHealth(HP);
    }

    public void SaveStats()
    {
        hero.SaveStats(Mathf.RoundToInt(HP));
    }

    /// <summary>
    /// Executes the model's side of Attack 1's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    /// <returns> AP Cost </returns>
    public int Attack1(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        return hero.Attack1(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the model's side of Attack 2's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    /// <returns> AP Cost </returns>
    public int Attack2(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        return hero.Attack2(heroes, enemies, currentActor);
    }

    /// <summary>
    /// Executes the model's side of Attack 3's behavior
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attack </param>
    /// <returns> AP Cost </returns>
    public int Attack3(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        return hero.Attack3(heroes, enemies, currentActor);
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
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public List<List<int>> GetAttack1Targets(int currentSpace)
    {
        return hero.GetAttack1Targets(currentSpace);
    }

    /// <summary>
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public List<List<int>> GetAttack2Targets(int currentSpace)
    {
        return hero.GetAttack2Targets(currentSpace);
    }

    /// <summary>
    /// Returns the positions targeted in the form of a 2d list
    /// int[0] = party side
    /// int[1] = enemy side
    /// </summary>
    /// <param name="currentSpace"> Current position of attack </param>
    /// <returns>The battefield state in the form of a 2D array</returns>
    public List<List<int>> GetAttack3Targets(int currentSpace)
    {
        return hero.GetAttack3Targets(currentSpace);
    }

    /// <summary>
    /// Increments this hero's hp by the passed ammount and updates its HP bar
    /// </summary>
    /// <param name="ammount"></param>
    public void IncrementHP(float ammount)
    {
        if (ammount > 0)
        {
            HP += ammount;
            if (HP > hero.MaxHP)
            {
                HP = hero.MaxHP;
            }
        }
        else if (ammount < 0) //If being hurt
        {
            float temp = HP + ammount - ammount * (float)((float)hero.Def * 0.1f);
            if (temp < HP)
                HP = temp;
        }
        if (HP > hero.MaxHP)
            HP = hero.MaxHP;
        HPBar.SetHealth(HP);
        if (HP <= 0)
        {
            Die();
        }
        
    }
    /// <summary>
    /// Increments this hero's AP by the passed ammount and updates its AP bar
    /// </summary>
    /// <param name="ammount"></param>
    public void IncrementAP(int ammount)
    {
        AP += ammount;
        if (AP <= 0)
        {
            AP = 0;
        }
        else if (AP > hero.MaxAP)
        {
            AP = hero.MaxAP;
        }
    }

    /// <summary>
    /// Sets this hero's AP to max
    /// </summary>
    public void ReplenishAP()
    {
        AP = hero.MaxAP;
    }


    /// <summary>
    /// Returns this hero's remaining AP
    /// </summary>
    /// <returns></returns>
    public int GetAP()
    {
        return AP;
    }

    /// <summary>
    /// Removes this hero from battle
    /// </summary>
    public void Die()
    {
        SaveStats();
        HPBar.Die();
        Destroy(this.gameObject);
        Destroy(this);
    }
}
