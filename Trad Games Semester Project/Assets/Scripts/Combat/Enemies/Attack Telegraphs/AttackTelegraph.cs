using UnityEngine;

public class AttackTelegraph : MonoBehaviour
{
    EnemyBehavior enemy;
    
    int attack = 0;

    Controller controller;
    Model model;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GameObject.FindGameObjectsWithTag("Controller")[0].GetComponent<Controller>();
        model = GameObject.FindGameObjectsWithTag("Model")[0].GetComponent<Model>();
    }

    public void ConnectAttack(int attack, EnemyBehavior enemy)
    {
        this.attack = attack;
        this.enemy = enemy;
    }


    private void OnMouseEnter()
    {
        controller.PointerEnterEnemyAttack(enemy.enemy.GetAttackTargets(model.GetPositionOfEnemy(enemy.gameObject), attack));
    }

    private void OnMouseExit()
    {
        controller.PointerExit();
    }

}
