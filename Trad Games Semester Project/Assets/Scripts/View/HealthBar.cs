using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] Slider slider;

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int value)
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
