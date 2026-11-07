using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableCharacter : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 offset;
    [SerializeField] Canvas canvas;
    [SerializeField] PartyUI PartyUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint
        );

        // Set new position using offset
        rectTransform.anchoredPosition = localPoint + offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Convert mouse position to local UI space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out offset
        );

        // Calculate the offset between the object's position and the click point
        offset = rectTransform.anchoredPosition - offset;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PartyUI.UpdateParty(this.GetComponent<Image>());
    }
}
