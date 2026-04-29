using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public Color normalColor = Color.white;
    public Color highlightedColor = Color.black;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = highlightedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }
}