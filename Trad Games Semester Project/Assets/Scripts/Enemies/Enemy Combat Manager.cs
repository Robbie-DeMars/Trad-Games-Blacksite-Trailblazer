using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCombatManager : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] GameObject HPBar;
    CombatManager manager;
    GameObject realBar = null;
    TextMeshProUGUI textBox = null;
    public delegate void action();
    int hp;
    int maxHP;
    int nextAttack = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        realBar = Instantiate(HPBar);
        textBox = realBar.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        manager = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<CombatManager>();
        //set HP to what it should be at the start of battle.
        hp = enemy.GetHP(this.gameObject);
        maxHP = enemy.GetHPMax(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        realBar.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
        textBox.text = hp + "/" + maxHP;
    }

    public IEnumerator Attack1()
    {
        yield return enemy.Attack1(this.gameObject, manager, manager.getEnemyPosition(this.gameObject), manager.getHeroes(), manager.getEnemies());
    }

    public List<int> DisplayAttack1Targets()
    {
        return enemy.DisplayAttack1Targets(this.gameObject, manager.getEnemyPosition(this.gameObject), manager.getHeroes());
    }

    public IEnumerator Attack2()
    {
        yield return enemy.Attack2(this.gameObject, manager, manager.getEnemyPosition(this.gameObject), manager.getHeroes(), manager.getEnemies());
    }

    public List<int> DisplayAttack2Targets()
    {
        return enemy.DisplayAttack2Targets(this.gameObject, manager.getEnemyPosition(this.gameObject), manager.getHeroes());
    }

    public IEnumerator Attack3()
    {
        yield return enemy.Attack3(this.gameObject, manager, manager.getEnemyPosition(this.gameObject), manager.getHeroes(), manager.getEnemies());
    }

    public List<int> DisplayAttack3Targets()
    {
        return enemy.DisplayAttack3Targets(this.gameObject, manager.getEnemyPosition(this.gameObject), manager.getHeroes());
    }

    public void incrementHP(int ammount)
    {
        hp += ammount;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
        if (hp <= 0)
        {
            manager.actions.Enqueue(die);
        }
    }

    public IEnumerator die()
    {
        yield return new WaitForSeconds(0.5f); ;
        realBar.SetActive(false);
        this.gameObject.SetActive(false);
        manager.removeFromCombat(this.gameObject);
    }

    public int GetNextAttack()
    {
        return nextAttack;
    }

    public void SetNextAttack(int attack)
    {
        nextAttack = attack;
    }

    public void OnMouseEnter()
    {
        int pos = manager.getEnemyPosition(this.gameObject);
        manager.displayEnemyReticles(pos, nextAttack);
    }

    public void OnMouseExit()
    {
        manager.clearReticles();
    }


}
