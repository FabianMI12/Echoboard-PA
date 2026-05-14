using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public static BrightnessManager Instance;

    [Header("Setari luminozitate")]
    [Range(0f, 1f)]
    public float luminozitate = 1f;

    private Canvas canvasOverlay;
    private Image imagineOverlay;

    void Awake()
    {
        // Facem managerul unic, ca sa nu avem 10 overlay-uri cand schimbam scenele
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Luam luminozitatea salvata, default 1 inseamna luminozitate normala
        luminozitate = PlayerPrefs.GetFloat("Luminozitate", 1f);

        CreeazaOverlay();
        AplicaLuminozitate();
    }

    void CreeazaOverlay()
    {
        // Canvas separat peste tot UI-ul
        GameObject canvasObj = new GameObject("BrightnessOverlayCanvas");
        canvasObj.transform.SetParent(transform);

        canvasOverlay = canvasObj.AddComponent<Canvas>();
        canvasOverlay.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasOverlay.sortingOrder = 9999;

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();

        // Imagine neagra care acopera tot ecranul
        GameObject imageObj = new GameObject("BrightnessOverlay");
        imageObj.transform.SetParent(canvasObj.transform);

        imagineOverlay = imageObj.AddComponent<Image>();
        imagineOverlay.color = new Color(0f, 0f, 0f, 0f);
        imagineOverlay.raycastTarget = false;

        RectTransform rect = imageObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;
    }

    public void SeteazaLuminozitate(float valoare)
    {
        // Slider-ul trimite valori intre 0 si 1
        luminozitate = Mathf.Clamp01(valoare);

        PlayerPrefs.SetFloat("Luminozitate", luminozitate);
        PlayerPrefs.Save();

        AplicaLuminozitate();
    }

    void AplicaLuminozitate()
    {
        if (imagineOverlay == null)
            return;

        // Luminozitate 1 = overlay invizibil
        // Luminozitate 0 = ecran foarte intunecat
        float alphaNegru = 1f - luminozitate;

        // Limitam putin ca sa nu devina ecranul complet negru
        alphaNegru = Mathf.Clamp(alphaNegru, 0f, 0.85f);

        imagineOverlay.color = new Color(0f, 0f, 0f, alphaNegru);
    }

    public float GetLuminozitate()
    {
        return luminozitate;
    }
}