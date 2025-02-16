using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PsiHeatBar : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    public void SetHeat(float heat)
    {
        slider.value = heat;
        text.text = slider.maxValue + " / " + slider.value;
    }

    public void SetMaxHeat(int value)
    {
        slider.maxValue = value;
        slider.value = value;
        text.color = Color.red;
        text.text = slider.maxValue + " / " + slider.value;
    }
}
