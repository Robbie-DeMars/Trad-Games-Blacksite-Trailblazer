using UnityEngine;
using UnityEngine.UI;

public class PsiHeatBar : MonoBehaviour
{

    [SerializeField] Slider slider;

    public void SetHeat(float health)
    {
        slider.value = health;
    }

    public void SetMaxHeat(int value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
}
