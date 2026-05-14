using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    [Header("UI principal")]
    public TMP_Text textRaspuns;
    public Button[] butoaneRaspunsuri;

    [Header("Panel-uri rezultat")]
    public GameObject panelCorect;
    public GameObject panelGresit;

    [Header("Panel final")]
    public GameObject panelFinal;
    public TMP_Text textFinalTimpTotal;
    public TMP_Text textFinalIntrebariCorecte;
    public TMP_Text textFinalIntrebariGresite;
    public TMP_Text textFinalScor;
    public TMP_Text textFinalCastigator;

    [Header("Info joc")]
    public TMP_Text textJucatorCurent;
    public TMP_Text textIntrebariCorecte;
    public TMP_Text textIntrebariGresite;
    public TMP_Text textIntrebariRamase;
    public TMP_Text textScor;

    [Header("Timp")]
    public TMP_Text textTimpIntrebare;
    public TMP_Text textTimpSesiune;

    [Header("Pioni")]
    public PawnMovement[] pioniJucatori;

    [Header("Multiplayer local")]
    public bool omoaraJucatori = true;
    public float distantaOmor = 0.25f;

    [Header("Setari joc")]
    public int maximIntrebariPeMapa = 24;

    [Header("Debug")]
    public TMP_Text textDificultateAleasa;
    public TMP_Text textModJoc;

    private PawnMovement[] pioniActivi;

    private List<QuestionData> intrebari;
    private List<QuestionData> intrebariRamase;
    private QuestionData intrebareCurenta;

    private int indexJucatorCurent = 0;
    private int indexCastigator = -1;

    private int[] corecteJucatori;
    private int[] gresiteJucatori;
    private int[] scorJucatori;

    private float timpIntrebare = 0f;
    private float timpSesiune = 0f;

    private bool cronometruIntrebarePornit = false;
    private bool cronometruSesiunePornit = false;
    private bool jocTerminat = false;
    private bool aRaspunsLaIntrebareaCurenta = false;

    void Start()
    {
        AscundePaneluriRezultat();
        AscundePanelFinal();

        PregatesteModJoc();
        IncarcaIntrebariDupaDificultate();

        if (intrebari == null || intrebari.Count == 0)
        {
            Debug.LogError("Nu avem intrebari pentru dificultatea: " + GameSettings.dificultateAleasa);
            SeteazaTextCuAutoFit(textRaspuns, "Nu exista intrebari pentru dificultatea: " + GameSettings.dificultateAleasa);
            return;
        }

        intrebariRamase = new List<QuestionData>(intrebari);

        timpSesiune = 0f;
        timpIntrebare = 0f;

        cronometruSesiunePornit = true;
        cronometruIntrebarePornit = false;

        jocTerminat = false;
        aRaspunsLaIntrebareaCurenta = false;

        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();
        ActualizeazaPanelFinal();

        AfiseazaIntrebareAleatorie();
    }

    void OnDestroy()
    {
        DezaboneazaEvenimentePioni();
    }

    void Update()
    {
        if (jocTerminat)
            return;

        if (cronometruSesiunePornit)
        {
            timpSesiune += Time.deltaTime;
        }

        if (cronometruIntrebarePornit)
        {
            timpIntrebare += Time.deltaTime;
        }

        ActualizeazaTextTimpuri();
    }

    void PregatesteModJoc()
    {
        DezaboneazaEvenimentePioni();

        if (pioniJucatori == null || pioniJucatori.Length == 0)
        {
            Debug.LogError("Nu ai pus niciun pion in QuizManager -> Pioni Jucatori.");

            pioniActivi = new PawnMovement[0];
            corecteJucatori = new int[0];
            gresiteJucatori = new int[0];
            scorJucatori = new int[0];

            return;
        }

        int numarPioniFolositi = 1;

        if (GameSettings.EsteMultiplayerLocal())
        {
            numarPioniFolositi = GameSettings.numarJucatoriMultiplayer;
        }

        numarPioniFolositi = Mathf.Clamp(numarPioniFolositi, 1, pioniJucatori.Length);

        pioniActivi = new PawnMovement[numarPioniFolositi];

        for (int i = 0; i < pioniJucatori.Length; i++)
        {
            PawnMovement pion = pioniJucatori[i];

            if (pion == null)
                continue;

            bool esteActiv = i < numarPioniFolositi;

            pion.gameObject.SetActive(esteActiv);

            if (esteActiv)
            {
                pioniActivi[i] = pion;
                pion.ReseteazaPion();
            }
        }

        corecteJucatori = new int[numarPioniFolositi];
        gresiteJucatori = new int[numarPioniFolositi];
        scorJucatori = new int[numarPioniFolositi];

        indexJucatorCurent = 0;
        indexCastigator = -1;

        for (int i = 0; i < numarPioniFolositi; i++)
        {
            corecteJucatori[i] = 0;
            gresiteJucatori[i] = 0;
            scorJucatori[i] = 0;
        }

        AboneazaEvenimentePioni();

        if (textModJoc != null)
        {
            if (GameSettings.EsteSingleplayer())
                SeteazaTextCuAutoFit(textModJoc, "Mod: Singleplayer");
            else
                SeteazaTextCuAutoFit(textModJoc, "Mod: Multiplayer local");
        }

        Debug.Log("Mod joc pregatit. Numar pioni activi: " + numarPioniFolositi);
    }

    void AboneazaEvenimentePioni()
    {
        if (pioniActivi == null)
            return;

        foreach (PawnMovement pion in pioniActivi)
        {
            if (pion == null)
                continue;

            // Scoatem intai, ca sa nu dublam abonarea din greseala
            pion.OnAjunsLaUltimulTile -= CandPionAjungeLaFinal;
            pion.OnMutareTerminata -= CandPionTerminaMutarea;

            pion.OnAjunsLaUltimulTile += CandPionAjungeLaFinal;
            pion.OnMutareTerminata += CandPionTerminaMutarea;
        }
    }

    void DezaboneazaEvenimentePioni()
    {
        if (pioniActivi == null)
            return;

        foreach (PawnMovement pion in pioniActivi)
        {
            if (pion == null)
                continue;

            pion.OnAjunsLaUltimulTile -= CandPionAjungeLaFinal;
            pion.OnMutareTerminata -= CandPionTerminaMutarea;
        }
    }

    void IncarcaIntrebariDupaDificultate()
    {
        List<QuestionData> toateIntrebarile = Resources.LoadAll<QuestionData>("").ToList();

        string dificultateAleasa = GameSettings.dificultateAleasa;

        Debug.Log("Dificultate primita din meniu: " + dificultateAleasa);
        Debug.Log("Total intrebari gasite in Resources: " + toateIntrebarile.Count);

        intrebari = toateIntrebarile
            .Where(q => q != null)
            .Where(q => SuntAceeasiDificultate(q.dificultate, dificultateAleasa))
            .OrderBy(q => UnityEngine.Random.value)
            .ToList();

        Debug.Log("Intrebari dupa filtrare (" + dificultateAleasa + "): " + intrebari.Count);

        if (textDificultateAleasa != null)
        {
            SeteazaTextCuAutoFit(textDificultateAleasa, "Dificultate: " + dificultateAleasa);
        }
    }

    bool SuntAceeasiDificultate(string dificultateDinIntrebare, string dificultateAleasa)
    {
        if (string.IsNullOrWhiteSpace(dificultateDinIntrebare))
            return false;

        if (string.IsNullOrWhiteSpace(dificultateAleasa))
            return false;

        return dificultateDinIntrebare.Trim().Equals(
            dificultateAleasa.Trim(),
            StringComparison.OrdinalIgnoreCase
        );
    }

    public void AfiseazaIntrebareAleatorie()
    {
        if (jocTerminat)
            return;

        if (intrebariRamase == null)
            return;

        AscundePaneluriRezultat();
        ActiveazaButoane();

        aRaspunsLaIntrebareaCurenta = false;

        if (intrebariRamase.Count == 0)
        {
            OpresteJocul();

            SeteazaTextCuAutoFit(textRaspuns, "Nu mai sunt intrebari pentru dificultatea: " + GameSettings.dificultateAleasa);

            foreach (Button buton in butoaneRaspunsuri)
            {
                if (buton != null)
                    buton.gameObject.SetActive(false);
            }

            ActualizeazaInfoJoc();
            ActualizeazaTextTimpuri();
            ActualizeazaPanelFinal();

            if (panelFinal != null)
                panelFinal.SetActive(true);

            return;
        }

        int indexRandom = UnityEngine.Random.Range(0, intrebariRamase.Count);
        intrebareCurenta = intrebariRamase[indexRandom];

        intrebariRamase.RemoveAt(indexRandom);

        timpIntrebare = 0f;
        cronometruIntrebarePornit = true;

        SeteazaTextCuAutoFit(textRaspuns, intrebareCurenta.raspuns);

        List<string> variante = intrebareCurenta.variante
            .Split(';')
            .Select(v => v.Trim())
            .Where(v => !string.IsNullOrEmpty(v))
            .ToList();

        variante = variante.OrderBy(v => UnityEngine.Random.value).ToList();

        for (int i = 0; i < butoaneRaspunsuri.Length; i++)
        {
            if (butoaneRaspunsuri[i] == null)
                continue;

            if (i >= variante.Count)
            {
                butoaneRaspunsuri[i].gameObject.SetActive(false);
                continue;
            }

            butoaneRaspunsuri[i].gameObject.SetActive(true);

            string variantaAleasa = variante[i];

            TMP_Text textButon = butoaneRaspunsuri[i].GetComponentInChildren<TMP_Text>();

            if (textButon != null)
            {
                SeteazaTextCuAutoFit(textButon, variantaAleasa);
            }

            butoaneRaspunsuri[i].onClick.RemoveAllListeners();

            string variantaPentruClick = variantaAleasa;

            butoaneRaspunsuri[i].onClick.AddListener(() =>
            {
                VerificaRaspuns(variantaPentruClick);
            });
        }

        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();
        ActualizeazaPanelFinal();
    }

    void VerificaRaspuns(string variantaAleasa)
    {
        if (jocTerminat)
            return;

        if (aRaspunsLaIntrebareaCurenta)
            return;

        if (intrebareCurenta == null)
            return;

        if (pioniActivi == null || pioniActivi.Length == 0)
            return;

        aRaspunsLaIntrebareaCurenta = true;
        cronometruIntrebarePornit = false;

        bool esteCorect = variantaAleasa == intrebareCurenta.corect;

        Debug.Log("Jucator " + GetNumarJucator(indexJucatorCurent) + " a ales: " + variantaAleasa);
        Debug.Log("Corect era: " + intrebareCurenta.corect);

        if (esteCorect)
        {
            corecteJucatori[indexJucatorCurent]++;

            scorJucatori[indexJucatorCurent] = NativeGameLogic.AdaugaScor(
                scorJucatori[indexJucatorCurent],
                intrebareCurenta.punctaj
            );

            PawnMovement pionCurent = GetPionCurent();

            if (pionCurent != null && NativeGameLogic.TrebuieMutatPionul(esteCorect))
                pionCurent.MutaLaUrmatorulTile();

            if (panelCorect != null)
                panelCorect.SetActive(true);

            if (panelGresit != null)
                panelGresit.SetActive(false);
        }
        else
        {
            gresiteJucatori[indexJucatorCurent]++;

            if (panelGresit != null)
                panelGresit.SetActive(true);

            if (panelCorect != null)
                panelCorect.SetActive(false);
        }

        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();
        ActualizeazaPanelFinal();

        DezactiveazaButoane();
    }

    public void ContinuaJocul()
    {
        if (jocTerminat)
            return;

        if (aRaspunsLaIntrebareaCurenta)
        {
            TreciLaUrmatorulJucator();
        }

        AfiseazaIntrebareAleatorie();
    }

    void TreciLaUrmatorulJucator()
    {
        if (GameSettings.EsteSingleplayer())
            return;

        int totalJucatori = GetTotalJucatori();

        if (totalJucatori <= 1)
            return;

        indexJucatorCurent = NativeGameLogic.UrmatorulJucator(indexJucatorCurent, totalJucatori);

        Debug.Log("Urmeaza Jucator " + GetNumarJucator(indexJucatorCurent));
    }

    void CandPionTerminaMutarea(PawnMovement pionCareSaMutat)
    {
        if (jocTerminat)
            return;

        if (GameSettings.EsteMultiplayerLocal())
        {
            VerificaOmorDupaMutare(pionCareSaMutat);
        }

        ActualizeazaInfoJoc();
        ActualizeazaPanelFinal();
    }

    void VerificaOmorDupaMutare(PawnMovement pionCareSaMutat)
    {
        if (!omoaraJucatori)
            return;

        if (pionCareSaMutat == null || pioniActivi == null)
            return;

        int indexAtacator = Array.IndexOf(pioniActivi, pionCareSaMutat);

        if (indexAtacator < 0)
            return;

        for (int i = 0; i < pioniActivi.Length; i++)
        {
            PawnMovement victima = pioniActivi[i];

            if (victima == null)
                continue;

            if (i == indexAtacator)
                continue;

            bool trebuieOmorat = NativeGameLogic.TrebuieOmoratPion(
                pionCareSaMutat.transform.position,
                victima.transform.position,
                distantaOmor
            );

            if (trebuieOmorat)
            {
                Debug.Log("Jucator " + GetNumarJucator(indexAtacator) + " a omorat Jucator " + GetNumarJucator(i));
                victima.TrimiteLaStart();
            }
        }
    }

    void CandPionAjungeLaFinal(PawnMovement pionCastigator)
    {
        if (jocTerminat)
            return;

        indexCastigator = Array.IndexOf(pioniActivi, pionCastigator);

        if (indexCastigator < 0)
            indexCastigator = indexJucatorCurent;

        AfiseazaPanelFinal();
    }

    void AfiseazaPanelFinal()
    {
        if (!jocTerminat)
        {
            OpresteJocul();
        }

        AscundePaneluriRezultat();

        foreach (Button buton in butoaneRaspunsuri)
        {
            if (buton != null)
            {
                buton.interactable = false;
                buton.gameObject.SetActive(false);
            }
        }

        SeteazaTextCuAutoFit(textRaspuns, "Joc terminat!");

        if (panelFinal != null)
            panelFinal.SetActive(true);

        ActualizeazaPanelFinal();
        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();
    }

    void OpresteJocul()
    {
        jocTerminat = true;
        cronometruSesiunePornit = false;
        cronometruIntrebarePornit = false;
    }

    void ActualizeazaPanelFinal()
    {
        if (pioniActivi == null || pioniActivi.Length == 0)
            return;

        int indexPentruFinal = indexCastigator >= 0 ? indexCastigator : indexJucatorCurent;
        indexPentruFinal = Mathf.Clamp(indexPentruFinal, 0, pioniActivi.Length - 1);

        if (textFinalCastigator != null)
        {
            if (GameSettings.EsteSingleplayer())
                SeteazaTextCuAutoFit(textFinalCastigator, "Joc terminat");
            else
                SeteazaTextCuAutoFit(textFinalCastigator, "Castigator: Jucator " + GetNumarJucator(indexPentruFinal));
        }

        if (textFinalTimpTotal != null)
            SeteazaTextCuAutoFit(textFinalTimpTotal, "Timp total: " + FormateazaTimp(timpSesiune));

        if (textFinalIntrebariCorecte != null)
            SeteazaTextCuAutoFit(textFinalIntrebariCorecte, "Intrebari Corecte: " + corecteJucatori[indexPentruFinal]);

        if (textFinalIntrebariGresite != null)
            SeteazaTextCuAutoFit(textFinalIntrebariGresite, "Intrebari Gresite: " + gresiteJucatori[indexPentruFinal]);

        if (textFinalScor != null)
            SeteazaTextCuAutoFit(textFinalScor, "Scor: " + scorJucatori[indexPentruFinal]);
    }

    void AscundePanelFinal()
    {
        if (panelFinal != null)
            panelFinal.SetActive(false);
    }

    void ActualizeazaInfoJoc()
    {
        if (pioniActivi == null || pioniActivi.Length == 0)
            return;

        int j = Mathf.Clamp(indexJucatorCurent, 0, pioniActivi.Length - 1);

        if (textJucatorCurent != null)
        {
            if (GameSettings.EsteSingleplayer())
                SeteazaTextCuAutoFit(textJucatorCurent, "Singleplayer");
            else
                SeteazaTextCuAutoFit(textJucatorCurent, "Jucator curent: " + GetNumarJucator(j));
        }

        if (textIntrebariCorecte != null)
            SeteazaTextCuAutoFit(textIntrebariCorecte, "Intrebari Corecte: " + corecteJucatori[j]);

        if (textIntrebariGresite != null)
            SeteazaTextCuAutoFit(textIntrebariGresite, "Intrebari Gresite: " + gresiteJucatori[j]);

        if (textIntrebariRamase != null)
        {
            PawnMovement pionCurent = GetPionCurent();

            if (pionCurent != null)
            {
                int pasiRamasi = pionCurent.GetPasiRamasi();
                int totalPasi = pionCurent.GetTotalPasiPanaLaFinal();

                SeteazaTextCuAutoFit(textIntrebariRamase, "Intrebari Ramase: " + pasiRamasi + "/" + totalPasi);
            }
            else
            {
                int corecteRamase = NativeGameLogic.CalculeazaIntrebariRamase(maximIntrebariPeMapa, corecteJucatori[j]);
                SeteazaTextCuAutoFit(textIntrebariRamase, "Intrebari Ramase: " + corecteRamase + "/" + maximIntrebariPeMapa);
            }
        }

        if (textScor != null)
            SeteazaTextCuAutoFit(textScor, "Scor: " + scorJucatori[j]);
    }

    void ActualizeazaTextTimpuri()
    {
        if (textTimpIntrebare != null)
            SeteazaTextCuAutoFit(textTimpIntrebare, "Timp Intrebare: " + FormateazaTimp(timpIntrebare));

        if (textTimpSesiune != null)
            SeteazaTextCuAutoFit(textTimpSesiune, "Timp Sesiune: " + FormateazaTimp(timpSesiune));
    }

    string FormateazaTimp(float timp)
    {
        int minute = Mathf.FloorToInt(timp / 60f);
        int secunde = Mathf.FloorToInt(timp % 60f);

        return minute.ToString("00") + ":" + secunde.ToString("00");
    }

    void SeteazaTextCuAutoFit(TMP_Text textTMP, string textNou)
    {
        if (textTMP == null)
            return;

        TMPTextAutoFit autoFit = textTMP.GetComponent<TMPTextAutoFit>();

        if (autoFit != null)
        {
            autoFit.SeteazaText(textNou);
        }
        else
        {
            textTMP.text = textNou;
            textTMP.enableAutoSizing = true;
            textTMP.fontSizeMin = 8f;
            textTMP.textWrappingMode = TextWrappingModes.Normal;
            textTMP.overflowMode = TextOverflowModes.Ellipsis;
            textTMP.ForceMeshUpdate();
        }
    }

    void AscundePaneluriRezultat()
    {
        if (panelCorect != null)
            panelCorect.SetActive(false);

        if (panelGresit != null)
            panelGresit.SetActive(false);
    }

    void ActiveazaButoane()
    {
        foreach (Button buton in butoaneRaspunsuri)
        {
            if (buton != null)
                buton.interactable = true;
        }
    }

    void DezactiveazaButoane()
    {
        foreach (Button buton in butoaneRaspunsuri)
        {
            if (buton != null)
                buton.interactable = false;
        }
    }

    PawnMovement GetPionCurent()
    {
        if (pioniActivi == null)
            return null;

        if (indexJucatorCurent < 0 || indexJucatorCurent >= pioniActivi.Length)
            return null;

        return pioniActivi[indexJucatorCurent];
    }

    int GetTotalJucatori()
    {
        if (pioniActivi == null)
            return 0;

        return pioniActivi.Length;
    }

    int GetNumarJucator(int index)
    {
        return index + 1;
    }

    public void ReseteazaJocul()
    {
        DezaboneazaEvenimentePioni();

        PregatesteModJoc();

        IncarcaIntrebariDupaDificultate();
        intrebariRamase = new List<QuestionData>(intrebari);

        timpIntrebare = 0f;
        timpSesiune = 0f;

        cronometruIntrebarePornit = false;
        cronometruSesiunePornit = true;

        jocTerminat = false;
        aRaspunsLaIntrebareaCurenta = false;

        foreach (Button buton in butoaneRaspunsuri)
        {
            if (buton != null)
                buton.gameObject.SetActive(true);
        }

        AscundePaneluriRezultat();
        AscundePanelFinal();

        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();
        ActualizeazaPanelFinal();

        AfiseazaIntrebareAleatorie();
    }
}