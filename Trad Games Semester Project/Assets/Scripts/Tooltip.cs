using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] string tooltip;
    Canvas canvas;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI tooltipText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.FindGameObjectsWithTag("Canvas")[0].GetComponent<Canvas>();

        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseOver()
    {
        canvasGroup.alpha = 1;
        Debug.Log("MOUSE OVER");
    }

    private void OnMouseExit()
    {
        canvasGroup.alpha = 0;
        Debug.Log("Mouse left");
    }

}
