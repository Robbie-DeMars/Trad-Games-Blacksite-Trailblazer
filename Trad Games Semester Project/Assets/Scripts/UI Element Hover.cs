using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIElementHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] bool Attack1 = false;
    [SerializeField] bool Attack2 = false;
    [SerializeField] bool Attack3 = false;
    CombatManager manager;
    bool overButton = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Attack1)
        {
            manager.DisplayAttack1Targets();
        }
        else if (Attack2)
        {
            manager.DisplayAttack2Targets();
        }
        else if (Attack3)
        {
            manager.DisplayAttack3Targets();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Attack1)
        {
            manager.clearReticles();
        }
        else if (Attack2)
        {
            manager.clearReticles();
        }
        else if (Attack3)
        {
            manager.clearReticles();
        }
    }
}
