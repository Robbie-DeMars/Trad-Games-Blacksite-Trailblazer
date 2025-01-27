using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.Rendering.DebugUI;

public class HeroCombatManager : MonoBehaviour
{
    [SerializeField] Hero hero;
    [SerializeField] GameObject HPBar;
    CombatManager manager;
    GameObject realBar = null;
    TextMeshProUGUI textBox = null;
    public delegate void action();
    int hp;
    int maxHP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        realBar = Instantiate(HPBar);
        textBox = realBar.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        manager = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<CombatManager>();
        hp = hero.GetHP(this.gameObject);
        maxHP = hero.GetHPMax(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        realBar.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, this.gameObject.transform.position.z);
        textBox.text = hero.GetHP(this.gameObject) + "/" + hero.GetHPMax(this.gameObject);
    }

    public IEnumerator Attack1()
    {
        yield return hero.Attack1(this.gameObject, manager, manager.getHeroPosition(this.gameObject), manager.getEnemies());
    }

    public List<int> DisplayAttack1Targets()
    {
        return hero.DisplayAttack1Targets(this.gameObject, manager.getHeroPosition(this.gameObject), manager.getEnemies());
    }

    public IEnumerator Attack2()
    {
        yield return hero.Attack2(this.gameObject, manager, manager.getHeroPosition(this.gameObject), manager.getEnemies());
    }

    public List<int> DisplayAttack2Targets()
    {
        return hero.DisplayAttack2Targets(this.gameObject, manager.getHeroPosition(this.gameObject), manager.getEnemies());
    }

    public IEnumerator Attack3()
    {
        yield return hero.Attack3(this.gameObject, manager, manager.getHeroPosition(this.gameObject), manager.getEnemies());
    }

    public List<int> DisplayAttack3Targets()
    {
        return hero.DisplayAttack3Targets(this.gameObject, manager.getHeroPosition(this.gameObject), manager.getEnemies());
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
    }
}
