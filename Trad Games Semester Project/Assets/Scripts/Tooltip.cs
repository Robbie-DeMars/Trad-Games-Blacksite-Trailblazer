using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string tooltip;
    CanvasGroup canvasGroup;
    TextMeshProUGUI tooltipText;

    public enum TooltipType
    {
        NORMAL,
        COMBAT
    }

    [SerializeField] private TooltipType type;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (type == TooltipType.NORMAL)
        {
            canvasGroup = GameObject.FindGameObjectsWithTag("Normal Tooltip")[0].GetComponent<CanvasGroup>();
            tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        else if (type == TooltipType.COMBAT)
        {
            canvasGroup = GameObject.FindGameObjectsWithTag("Combat Tooltip")[0].GetComponent<CanvasGroup>();
            tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void makeSureCanvasGroupStillHere()
    {
        if (!canvasGroup)
        {
            if (type == TooltipType.NORMAL)
            {
                canvasGroup = GameObject.FindGameObjectsWithTag("Normal Tooltip")[0].GetComponent<CanvasGroup>();
                tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            }
            else if (type == TooltipType.COMBAT)
            {
                canvasGroup = GameObject.FindGameObjectsWithTag("Combat Tooltip")[0].GetComponent<CanvasGroup>();
                tooltipText = canvasGroup.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        makeSureCanvasGroupStillHere();
        tooltipText.text = tooltip;
        canvasGroup.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        makeSureCanvasGroupStillHere();
        canvasGroup.alpha = 0;
    }

    private void OnMouseEnter()
    {
        makeSureCanvasGroupStillHere();
        tooltipText.text = tooltip;
        canvasGroup.alpha = 1;
    }

    private void OnMouseExit()
    {
        makeSureCanvasGroupStillHere();
        canvasGroup.alpha = 0;
    }
}
