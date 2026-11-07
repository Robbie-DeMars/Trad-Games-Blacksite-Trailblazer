using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : BattleActorBehavior
{
    //Enemy Definition
    [SerializeField] public Enemy enemy;
    //HP Bar Instance
    [SerializeField] HealthBar HPBar;
    //Prepared Attack List
    List<Enemy.Attack> upcomingAttacks = new List<Enemy.Attack>();
    //Telegraph instances for upcoming attacks
    List<GameObject> currentTelegraphs;
    //Telegraphs for each attack
    [SerializeField] GameObject[] attackTelegraphs;
    //Offset for how the telegraph should appear on each enemy
    [SerializeField] Transform telegraphDisplayPosition;

    //private ref to prefab of damage readout
    [SerializeField] private GameObject damageReadoutPrefab;

    //UI element meant to be them in the turn order
    [SerializeField] public GameObject turnDisplay;

    public bool takingTurn = false;



    private void Start()
    {
        currentTelegraphs = new List<GameObject>();
        PrepareAttacks();
        HP = enemy.MaxHP;
        HPBar.SetMaxHealth(enemy.MaxHP);
    }

    public override void Die()
    {
        HPBar.Die();
        Destroy(turnDisplay);
        Destroy(this.gameObject);
        Destroy(this);
    }

    public override BattleActor GetBattleActor()
    {
        return enemy;
    }

    //TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS//TELEGRAPHS

    /// <summary>
    /// Instantiates the telegraphs above this enemy in the worldspace
    /// </summary>
    private void MakeTelegraphs()
    {
        float totalWidth = (upcomingAttacks.Count - 1) * 1f; // Total width of the telegraphs
        float startX = telegraphDisplayPosition.position.x - (totalWidth / 2); // Center align

        for (int x = 0; x < upcomingAttacks.Count; x++)
        {
            int attack = enemy.GetAttackNumber(upcomingAttacks[x]);
            currentTelegraphs.Add(Instantiate(attackTelegraphs[attack - 1], this.transform));
            currentTelegraphs[currentTelegraphs.Count - 1].GetComponent<AttackTelegraph>().ConnectAttack(attack, this);
            currentTelegraphs[x].transform.position = new Vector3(startX + x * 1f, telegraphDisplayPosition.position.y, telegraphDisplayPosition.position.z);
            currentTelegraphs[x].GetComponent<AttackTelegraph>().tooltip = enemy.GetAttackTooltip(upcomingAttacks[x]);
        }
    }

    /// <summary>
    /// Removes telegraphs from the worldspace
    /// </summary>
    private void DeleteTelegraphs()
    {
        for (int x = 0; x < currentTelegraphs.Count(); x++)
        {
            Destroy(currentTelegraphs[x]);
        }
        currentTelegraphs = new List<GameObject>();
    }

    //ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK

    /// <summary>
    /// Executes the next attack queued for this enemy, if such an attack exists.
    /// </summary>
    /// <param name="heroes"> Current state of heroes </param>
    /// <param name="enemies"> Current state of enemies </param>
    /// <param name="currentActor"> Current position of attacker </param>
    public void NextAttack(GameObject[] heroes, GameObject[] enemies, int currentActor)
    {
        if (!HasMoreAttacks())
            return;

        //Call attack
        upcomingAttacks[0](heroes, enemies, currentActor);

        //Remove last attack
        upcomingAttacks.RemoveAt(0);
    }

    /// <summary>
    /// Returns the next attack that will be executed by NextAttack
    /// </summary>
    /// <returns> Reference to the next attack </returns>
    public Enemy.Attack GetNextAttack()
    {
        return upcomingAttacks[0];
    }

    /// <summary>
    /// Returns true if this enemy can execute more attacks this turn, false if not.
    /// </summary>
    /// <returns></returns>
    public bool HasMoreAttacks()
    {
        return upcomingAttacks.Count > 0;
    }

    /// <summary>
    /// Plays the passed attack at the given position, does nothing if given attack does not exist in this context .
    /// </summary>
    /// <param name="currentActor"> Position of current attacker </param>
    /// <param name="attack"> Reference to the attack executed </param>
    /// <returns></returns>
    public IEnumerator PlayNextAttackAnim(int currentActor, Enemy.Attack attack)
    {
        yield return enemy.PlayAttackAnim(currentActor, attack);
    }

    /// <summary>
    /// Resets the attacks this enemy will execute.
    /// </summary>
    public void PrepareAttacks()
    {
        DeleteTelegraphs();
        upcomingAttacks = enemy.PrepareAttacks();
        MakeTelegraphs();
    }

    //STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION

    public override void IncrementHP(float ammount)
    {
        //declaring variables for computing total difference in HP, used for Damage Readout.
        float _prevHP = HP;
        float _newHP;
        float _deltaHP;

        if (ammount > 0) //If being healed
        {
            HP += ammount;
            if (HP > enemy.MaxHP)
            {
                HP = enemy.MaxHP;
            }
        }
        else if (ammount < 0) //If being hurt
        {
            float temp = HP + ammount + ammount * enemy.Def / 10;
            if (temp < HP)
                HP = temp;
        }
        HPBar.SetHealth(HP);

        _newHP = HP;

        if (HP <= 0)
        {
            _newHP = 0f;
            Die();
        }

        //instantiate damage readout.
        _deltaHP = _newHP - _prevHP;
        GameObject _readout = Instantiate(damageReadoutPrefab, this.gameObject.transform.position, Quaternion.identity);
        DamageReadout _damageReadout = _readout.GetComponent<DamageReadout>();
        _damageReadout.Init(((int)Mathf.Abs(_deltaHP)).ToString(), Vector3.up, 2.5f);
        if (_deltaHP > 0) { _damageReadout.Greenify(); }
    }

    //STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS//STATUS EFFECTS

    public override void SettleEffects()
    {
        int count = aEffects.Count;
        for (int x = 0; x < count; x++)
        {
            int ID = aEffects[x].GetInstanceID();
            aEffects[x].DoEffect(this);
            if (x >= aEffects.Count || ID != aEffects[x].GetInstanceID())
            {
                x--;
                count--;
            }
        }
    }

    public override void GiveEffect(StatusEffect effect)
    {
        if (effect is AgnosticEffect ae)
        {
            if (effect.stackable || (!effect.stackable && !HasStatusEffect(effect)))
            {
                aEffects.Add(ae);
                ae.DoEffect(this);
            }
        }
        UpdateEffectDisplay();
    }

    public override bool HasStatusEffect(StatusEffect effect)
    {
        bool hasStatusEffect = false;
        if (effect is AgnosticEffect ae)
        {
            for (int x = 0; x < aEffects.Count; x++)
            {
                if (aEffects[x].Equals(effect))
                {
                    hasStatusEffect = true;
                }
            }
        }

        return hasStatusEffect;
    }

    public override void ClearEffects()
    {
        for (int x = 0; x < aEffects.Count; x++)
        {
            aEffects[x].ClearEffect(this);
        }
        UpdateEffectDisplay();
    }

    public override void UpdateEffectDisplay()
    {
        float offset = 0.2f;
        float totalWidth = (aEffects.Count - 1) * offset; // Total width of the agnostic effects
        float startX = effectDisplayPosition.position.x - (totalWidth / 2); // Center align

        //Remove existing displays
        for (int x = 0; x < effects.Count; x++)
        {
            Destroy(effects[x]);
        }
        effects.Clear();

        //Remake them with the current information
        for (int x = 0; x < aEffects.Count; x++)
        {
            effects.Add(Instantiate(aEffects[x].effectImage, this.transform));
            effects[x].transform.position = new Vector3(startX + x * offset, effectDisplayPosition.position.y, effectDisplayPosition.position.z);
        }
    }
}
