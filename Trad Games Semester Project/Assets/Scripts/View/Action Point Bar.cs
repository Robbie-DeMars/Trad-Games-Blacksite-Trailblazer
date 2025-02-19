using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionPointBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    public void SetAP(float points)
    {
        slider.value = points;
        text.color = Color.red;
        text.text = slider.value + "\n--\n" + slider.maxValue;
    }

    public void SetMaxAP(int value)
    {
        slider.maxValue = value;
        slider.value = value;
        text.color = Color.red;
        text.text = slider.value + "\n--\n" + slider.maxValue;
    }

    public void Die()
    {
        Destroy(this.gameObject);
        Destroy(this);
    }


}
