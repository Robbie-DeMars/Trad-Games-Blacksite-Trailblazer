using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HoverExpand : MonoBehaviour
{
    private Button thisElement;
    private bool Hovered = false;

    public float unHoveredScale = 1f;
    public float hoveredScale = 1.2f;
    public float duration = 0.4f;

    private float t = 1;

    private void Start()
    {
        thisElement = this.gameObject.GetComponent<Button>();
    }
    // Update is called once per frame
    void Update()
    {
        if (thisElement.interactable == false) { t = duration; thisElement.transform.localScale = Vector3.one; return; }
        t += Time.deltaTime;
        float x = t / duration;
        float step = 0f;
        if (Hovered)
        {
            step = Mathf.SmoothStep(unHoveredScale, hoveredScale, x);
        }
        else
        {
            step = Mathf.SmoothStep(hoveredScale, unHoveredScale, x);
        }
        thisElement.transform.localScale = new Vector3(step, step, step);
    }
    public void UnHover() {
        t = 0f;
        Hovered = false;
    }

    public void Hover() {
        t = 0f;
        Hovered = true;
    }
}
