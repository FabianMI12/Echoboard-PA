using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        // Luam slider-ul de pe obiectul asta
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("BrightnessSlider trebuie pus pe un obiect care are componenta Slider.");
            return;
        }

        // Setam slider-ul la valoarea salvata
        if (BrightnessManager.Instance != null)
        {
            slider.value = BrightnessManager.Instance.GetLuminozitate();
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("Luminozitate", 1f);
        }

        // Legam slider-ul de manager
        slider.onValueChanged.RemoveListener(SchimbaLuminozitatea);
        slider.onValueChanged.AddListener(SchimbaLuminozitatea);
    }

    void SchimbaLuminozitatea(float valoare)
    {
        if (BrightnessManager.Instance != null)
        {
            BrightnessManager.Instance.SeteazaLuminozitate(valoare);
        }
    }
}