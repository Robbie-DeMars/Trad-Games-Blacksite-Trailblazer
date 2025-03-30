using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : BattleActorBehavior
{
    //Hero Definition
    [SerializeField] public Hero hero;
    //HP Bar Instance
    [SerializeField] HealthBar HPBar;
    //Instance AP
    private int AP;
    //Flag for whether it is this hero's turn or not
    public bool takingTurn = false;

    //private ref to prefab of damage readout
    [SerializeField] private GameObject damageReadoutPrefab;

    //SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA//SAVE DATA

    //Upon spawining, load save data of stats
    private void Awake()
    {
        hero.LoadStats();
        HPBar.SetMaxHealth(hero.MaxHP);
        AP = hero.MaxAP;
        HP = hero.HP;
        HPBar.SetHealth(HP);
    }

    /// <summary>
    /// Saves the relevant data for this character
    /// </summary>
    public void SaveStats()
    {
        hero.SaveStats(Mathf.RoundToInt(HP));
    }

    /// <summary>
    /// Removes this hero from battle
    /// </summary>
    public override void Die()
    {
        HP = 1;
        SaveStats();
        if (takingTurn)
            GameObject.FindGameObjectWithTag("Model").GetComponent<Model>().PassInitiative();
        HPBar.Die();
        Destroy(this.gameObject);
        Destroy(this);
    }

    public override BattleActor GetBattleActor()
    {
        return hero;
    }

    //ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK//ATTACK

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
    /// Returns the string tooltip of this characters first attack
    /// </summary>
    /// <returns></returns>
    public string GetAttack1Tooltip()
    {
        return hero.GetAttack1Tooltip();
    }

    /// <summary>
    /// Returns the string tooltip of this characters second attack
    /// </summary>
    /// <returns></returns>
    public string GetAttack2Tooltip()
    {
        return hero.GetAttack2Tooltip();
    }

    /// <summary>
    /// Returns the string tooltip of this characters third attack
    /// </summary>
    /// <returns></returns>
    public string GetAttack3Tooltip()
    {
        return hero.GetAttack3Tooltip();
    }

    //STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION//STAT MANIPULATION

    public override void IncrementHP(float ammount)
    {
        //declaring variables for computing total difference in HP, used for Damage Readout.
        float _prevHP = HP;
        float _newHP;
        float _deltaHP;

        if (ammount > 0)
        {
            HP += ammount;
            if (HP > hero.MaxHP) //If being healed
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

        _newHP = HP;

        if (HP <= 0)
        {
            _newHP = 0;
            Die();
        }

        //instantiate damage readout.
        _deltaHP = _newHP - _prevHP;
        GameObject _readout = Instantiate(damageReadoutPrefab, this.gameObject.transform.position, Quaternion.identity);
        DamageReadout _damageReadout = _readout.GetComponent<DamageReadout>();
        _damageReadout.Init(((int)Mathf.Abs(_deltaHP)).ToString(), Vector3.up , 2.5f);
        _damageReadout.Greenify();//TODO REMOVE THIS IS JUST TO TEST AND SEE IF IT WORKS
        if (_deltaHP > 0) { _damageReadout.Greenify(); }
        
    }

    /// <summary>
    /// Sets this hero's HP to max
    /// </summary>
    public void ReplenishHP()
    {
        hero.ReplenishHP();
    }


    /// <summary>
    /// Sets this hero's AP to max
    /// </summary>
    public void ReplenishAP()
    {
        AP = hero.MaxAP;
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
    /// Returns this hero's remaining AP
    /// </summary>
    /// <returns></returns>
    public int GetAP()
    {
        return AP;
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
                if (aEffects[x].ID.Equals(effect.ID))
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
