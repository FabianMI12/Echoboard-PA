using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("VolumeSlider trebuie pus pe un obiect care are componenta Slider.");
            return;
        }

        if (MusicManager.Instance != null)
        {
            slider.value = MusicManager.Instance.GetVolum();
        }
        else
        {
            slider.value = PlayerPrefs.GetFloat("VolumMuzica", 1f);
        }

        slider.onValueChanged.RemoveListener(SchimbaVolumul);
        slider.onValueChanged.AddListener(SchimbaVolumul);
    }

    void SchimbaVolumul(float valoare)
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SeteazaVolum(valoare);
        }
    }
}