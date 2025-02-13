using UnityEngine;
using UnityEngine.UI;

public class ActionPointBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    public void SetAP(float points)
    {
        slider.value = points;
    }

    public void SetMaxAP(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void Die()
    {
        Destroy(this.gameObject);
        Destroy(this);
    }
}
