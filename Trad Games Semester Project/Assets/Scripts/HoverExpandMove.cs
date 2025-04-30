using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class HoverExpandMove : MonoBehaviour
{
    private GameObject thisElement;
    private bool Hovered = false;

    public Model model;//evil hacky method of getting information regarding state

    public float unHoveredScale = 1f;
    public float hoveredScale = 1.2f;
    public float duration = 0.4f;
    public float frequency = 4f;
    public float strength = .1f;

    private float t = 1f;
    private float s = 0f;//sinwave time

    private void Start()
    {
        model = FindAnyObjectByType<Model>();
        thisElement = this.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        s += Time.deltaTime;
        t += Time.deltaTime;
        float x = t / duration;//percent 0-1 (will go above 1 but smoothstep handles it)
        float target = 0f;
        if (Hovered)
        {
            target = Mathf.SmoothStep(unHoveredScale, hoveredScale, x);
        }
        else
        {
            target = Mathf.SmoothStep(hoveredScale, unHoveredScale, x);
        }
        if (model.GetCurrentState() == StateMachine.State.MOVE) { target += Mathf.Sin(s * frequency) * strength; }
        thisElement.transform.localScale = new Vector3(target, target, target);
    }
    public void UnHover()
    {
        t = 0f;
        s = s % 2*Mathf.PI;//brings it closer to 0 range. If the fight goes on for too long we could start getting floating point precision issues.
        Hovered = false;
    }

    public void Hover()
    {
        t = 0f;
        s = s % 2*Mathf.PI;//read comment on UnHover. 
        Hovered = true;
    }
}
