using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackTelegraph : MonoBehaviour
{
    EnemyBehavior enemy;
    
    int attack = 0;

    Controller controller;
    Model model;
    public string tooltip = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if ((!SceneManager.GetActiveScene().name.Equals("Combat") && !SceneManager.GetActiveScene().name.Equals("Combat Tutorial")) && (!SceneManager.GetActiveScene().name.Equals("Combat Swamp") && !SceneManager.GetActiveScene().name.Equals("Combat Desert")))
        {
            this.gameObject.SetActive(false);
            return;
        }
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

        //Find combat tooltip box
        CanvasGroup canvasGroup = GameObject.FindGameObjectsWithTag("Combat Tooltip")[0].GetComponent<CanvasGroup>();
        TextMeshProUGUI tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tooltipText.text = tooltip;

        //Clear attack descriptor
        canvasGroup.alpha = 1;
    }

    private void OnMouseExit()
    {
        controller.PointerExit();
    }

}
