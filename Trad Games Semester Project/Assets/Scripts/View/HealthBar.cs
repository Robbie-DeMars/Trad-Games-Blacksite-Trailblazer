using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    public void SetHealth(float health)
    {
        slider.value = health;
        text.text = slider.maxValue + " / " + slider.value;
    }

    public void SetMaxHealth(int value)
    {
        slider.maxValue = value;
        slider.value = value;
        text.color = Color.blue;
        text.text = slider.maxValue + " / " + slider.value;
    }

    public void Die()
    {
        Destroy(this.gameObject);
        Destroy(this);
    }

}
