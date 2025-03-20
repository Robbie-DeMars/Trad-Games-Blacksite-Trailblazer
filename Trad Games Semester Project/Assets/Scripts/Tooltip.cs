using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string tooltip;
    CanvasGroup canvasGroup;
    TextMeshProUGUI tooltipText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GameObject.FindGameObjectsWithTag("Normal Tooltip")[0].GetComponent<CanvasGroup>();
        tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.text = tooltip;
        canvasGroup.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        canvasGroup.alpha = 0;
    }

    private void OnMouseEnter()
    {
        tooltipText.text = tooltip;
        canvasGroup.alpha = 1;
    }

    private void OnMouseExit()
    {
        canvasGroup.alpha = 0;
    }
}
