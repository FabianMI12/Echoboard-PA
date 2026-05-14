using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TMPTextAutoFit : MonoBehaviour
{
    [Header("Marimi text")]
    public float marimeMaxima = 28f;
    public float marimeMinima = 10f;

    [Header("Setari text")]
    public bool permiteRanduriMultiple = true;
    public TextOverflowModes modOverflow = TextOverflowModes.Ellipsis;

    private TMP_Text textTMP;

    void Awake()
    {
        // Luam componenta TMP de pe obiectul asta
        textTMP = GetComponent<TMP_Text>();

        PregatesteTextul();
    }

    void OnEnable()
    {
        // Cand se reactiveaza obiectul, ne asiguram ca setarile raman bune
        if (textTMP == null)
            textTMP = GetComponent<TMP_Text>();

        PregatesteTextul();
    }

    public void PregatesteTextul()
    {
        if (textTMP == null)
            return;

        // TextMeshPro se ocupa singur sa micsoreze textul daca nu incape
        textTMP.enableAutoSizing = true;

        // Limitele intre care are voie sa schimbe marimea textului
        textTMP.fontSizeMax = marimeMaxima;
        textTMP.fontSizeMin = marimeMinima;

        // Unity 6 / TMP nou: folosim textWrappingMode in loc de enableWordWrapping
        textTMP.textWrappingMode = permiteRanduriMultiple
            ? TextWrappingModes.Normal
            : TextWrappingModes.NoWrap;

        // Daca tot nu incape, nu il lasam sa iasa urat din chenar
        textTMP.overflowMode = modOverflow;

        // Fortam TMP sa recalculeze imediat
        textTMP.ForceMeshUpdate();
    }

    public void SeteazaText(string textNou)
    {
        if (textTMP == null)
            textTMP = GetComponent<TMP_Text>();

        // Setam textul si refacem auto-size-ul
        textTMP.text = textNou;
        PregatesteTextul();
    }
}