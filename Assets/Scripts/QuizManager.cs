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

    [Header("Info joc")]
    public TMP_Text textIntrebariCorecte;
    public TMP_Text textIntrebariGresite;
    public TMP_Text textIntrebariRamase;
    public TMP_Text textScor;

    [Header("Timp")]
    public TMP_Text textTimpIntrebare;
    public TMP_Text textTimpSesiune;

    [Header("Pion")]
    public PawnMovement pawnMovement;

    [Header("Setari joc")]
    public int maximIntrebariPeMapa = 24;

    private List<QuestionData> intrebari;
    private List<QuestionData> intrebariRamase;
    private QuestionData intrebareCurenta;

    private int numarCorecte = 0;
    private int numarGresite = 0;
    private int scorTotal = 0;

    private float timpIntrebare = 0f;
    private float timpSesiune = 0f;

    private bool cronometruIntrebarePornit = false;
    private bool cronometruSesiunePornit = false;

    void Start()
    {
        // Pregatim ecranul cand porneste scena
        AscundePaneluriRezultat();

        // Luam toate intrebarile din Assets/Resources
        intrebari = Resources.LoadAll<QuestionData>("").ToList();

        Debug.Log("Am gasit " + intrebari.Count + " intrebari in baza de date.");

        if (intrebari.Count == 0)
        {
            Debug.LogError("Nu avem intrebari incarcate. Verifica daca asset-urile sunt in Assets/Resources.");
            return;
        }

        // Amestecam toate intrebarile, ca sa fie alta ordine la fiecare play
        intrebari = intrebari
            .OrderBy(q => Random.value)
            .ToList();

        // Lista asta scade cand primim o intrebare noua, ca sa nu se repete
        intrebariRamase = new List<QuestionData>(intrebari);

        // Resetam statisticile pentru jocul nou
        numarCorecte = 0;
        numarGresite = 0;
        scorTotal = 0;

        // Pornim cronometrul sesiunii
        timpSesiune = 0f;
        cronometruSesiunePornit = true;

        // Pregatim cronometrul intrebarii
        timpIntrebare = 0f;
        cronometruIntrebarePornit = false;

        // Resetam pionul la inceputul tablei
        if (pawnMovement != null)
            pawnMovement.ReseteazaPion();

        // Actualizam textele din GameInfo
        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();

        // Bagam prima intrebare
        AfiseazaIntrebareAleatorie();
    }

    void Update()
    {
        // Cronometrul total merge cat timp jocul nu e terminat
        if (cronometruSesiunePornit)
        {
            timpSesiune += Time.deltaTime;
        }

        // Cronometrul intrebarii merge doar pana playerul raspunde
        if (cronometruIntrebarePornit)
        {
            timpIntrebare += Time.deltaTime;
        }

        ActualizeazaTextTimpuri();
    }

    public void AfiseazaIntrebareAleatorie()
    {
        // Resetam UI-ul pentru o intrebare noua
        AscundePaneluriRezultat();
        ActiveazaButoane();

        // Daca playerul a raspuns corect cat trebuie, a terminat mapa
        if (numarCorecte >= maximIntrebariPeMapa)
        {
            // Oprim ambele cronometre
            cronometruSesiunePornit = false;
            cronometruIntrebarePornit = false;

            Debug.Log("Mapa terminata.");

            textRaspuns.text = "GG, ati terminat mapa.";

            foreach (Button buton in butoaneRaspunsuri)
            {
                if (buton != null)
                    buton.gameObject.SetActive(false);
            }

            ActualizeazaInfoJoc();
            ActualizeazaTextTimpuri();
            return;
        }

        // Daca s-au terminat intrebarile din baza, nu mai avem ce afisa
        if (intrebariRamase.Count == 0)
        {
            // Oprim ambele cronometre
            cronometruSesiunePornit = false;
            cronometruIntrebarePornit = false;

            Debug.Log("S-au terminat intrebarile din baza de date.");

            textRaspuns.text = "Nu mai sunt intrebari disponibile.";

            foreach (Button buton in butoaneRaspunsuri)
            {
                if (buton != null)
                    buton.gameObject.SetActive(false);
            }

            ActualizeazaInfoJoc();
            ActualizeazaTextTimpuri();
            return;
        }

        // Alegem random doar din intrebarile care n-au fost folosite
        int indexRandom = Random.Range(0, intrebariRamase.Count);
        intrebareCurenta = intrebariRamase[indexRandom];

        // Scoatem intrebarea aleasa, ca sa nu se repete
        intrebariRamase.RemoveAt(indexRandom);

        // Resetam timpul pentru intrebarea noua
        timpIntrebare = 0f;
        cronometruIntrebarePornit = true;

        // In stil Jeopardy: afisam raspunsul, iar pe butoane punem intrebarile
        textRaspuns.text = intrebareCurenta.raspuns;

        // Spargem variantele dupa ; si curatam spatiile inutile
        List<string> variante = intrebareCurenta.variante
            .Split(';')
            .Select(v => v.Trim())
            .Where(v => !string.IsNullOrEmpty(v))
            .ToList();

        // Amestecam variantele, ca butonul corect sa nu fie mereu in acelasi loc
        variante = variante.OrderBy(v => Random.value).ToList();

        // Punem variantele pe cele 4 butoane
        for (int i = 0; i < butoaneRaspunsuri.Length; i++)
        {
            if (i >= variante.Count)
            {
                // Daca avem mai putine variante, ascundem butoanele extra
                butoaneRaspunsuri[i].gameObject.SetActive(false);
                continue;
            }

            butoaneRaspunsuri[i].gameObject.SetActive(true);

            string variantaAleasa = variante[i];

            TMP_Text textButon = butoaneRaspunsuri[i].GetComponentInChildren<TMP_Text>();

            if (textButon != null)
            {
                textButon.text = variantaAleasa;
            }
            else
            {
                Debug.LogWarning("Butonul " + butoaneRaspunsuri[i].name + " nu are TMP_Text copil.");
            }

            // Curatam click-urile vechi, altfel se aduna de la intrebarile trecute
            butoaneRaspunsuri[i].onClick.RemoveAllListeners();

            // Salvam varianta local, ca Unity sa nu incurce valorile din loop
            string variantaPentruClick = variantaAleasa;

            // Cand apasam butonul, verificam daca e corect
            butoaneRaspunsuri[i].onClick.AddListener(() =>
            {
                VerificaRaspuns(variantaPentruClick);
            });
        }

        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();
    }

    void VerificaRaspuns(string variantaAleasa)
    {
        // Oprim timpul intrebarii imediat ce playerul a raspuns
        cronometruIntrebarePornit = false;

        // Comparam ce a apasat playerul cu varianta corecta din baza noastra
        bool esteCorect = variantaAleasa == intrebareCurenta.corect;

        Debug.Log("Ai ales: " + variantaAleasa);
        Debug.Log("Corect era: " + intrebareCurenta.corect);
        Debug.Log("Timp pe intrebare: " + FormateazaTimp(timpIntrebare));

        if (esteCorect)
        {
            // Daca e bine, crestem corectele, adaugam puncte si mutam pionul
            numarCorecte++;
            scorTotal += intrebareCurenta.punctaj;

            Debug.Log("Corect! +" + intrebareCurenta.punctaj + " puncte");
            Debug.Log("Scor total: " + scorTotal);

            if (pawnMovement != null)
                pawnMovement.MutaLaUrmatorulTile();

            if (panelCorect != null)
                panelCorect.SetActive(true);

            if (panelGresit != null)
                panelGresit.SetActive(false);
        }
        else
        {
            // Daca e gresit, crestem gresitele, dar pionul sta pe loc
            numarGresite++;

            Debug.Log("Gresit. Pionul ramane pe loc.");
            Debug.Log("Scorul ramane: " + scorTotal);

            if (panelGresit != null)
                panelGresit.SetActive(true);

            if (panelCorect != null)
                panelCorect.SetActive(false);
        }

        // Actualizam textele dupa raspuns
        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();

        // Dupa un raspuns, blocam butoanele ca sa nu apese cineva de 100 de ori
        DezactiveazaButoane();
    }

    public void ContinuaJocul()
    {
        // Butonul Continua din panel-uri cheama metoda asta
        AfiseazaIntrebareAleatorie();
    }

    public void ReseteazaJocul()
    {
        // Reamestecam toate intrebarile
        intrebari = Resources.LoadAll<QuestionData>("")
            .OrderBy(q => Random.value)
            .ToList();

        intrebariRamase = new List<QuestionData>(intrebari);

        // Resetam tot ce tine de statistici
        numarCorecte = 0;
        numarGresite = 0;
        scorTotal = 0;

        // Resetam ambele cronometre
        timpIntrebare = 0f;
        timpSesiune = 0f;

        cronometruIntrebarePornit = false;
        cronometruSesiunePornit = true;

        // Resetam pionul
        if (pawnMovement != null)
            pawnMovement.ReseteazaPion();

        foreach (Button buton in butoaneRaspunsuri)
        {
            if (buton != null)
                buton.gameObject.SetActive(true);
        }

        ActualizeazaInfoJoc();
        ActualizeazaTextTimpuri();

        AfiseazaIntrebareAleatorie();
    }

    void ActualizeazaInfoJoc()
    {
        // Scriem informatiile mici din dreapta sus
        if (textIntrebariCorecte != null)
            textIntrebariCorecte.text = "Intrebari Corecte: " + numarCorecte;

        if (textIntrebariGresite != null)
            textIntrebariGresite.text = "Intrebari Gresite: " + numarGresite;

        if (textIntrebariRamase != null)
        {
            // Aici "ramase" inseamna cate raspunsuri corecte mai trebuie pentru a termina mapa
            int corecteRamase = maximIntrebariPeMapa - numarCorecte;

            if (corecteRamase < 0)
                corecteRamase = 0;

            textIntrebariRamase.text = "Intrebari Ramase: " + corecteRamase + "/" + maximIntrebariPeMapa;
        }

        if (textScor != null)
            textScor.text = "Scor: " + scorTotal;
    }

    void ActualizeazaTextTimpuri()
    {
        // Punem pe ecran timpul intrebarii si timpul total al sesiunii
        if (textTimpIntrebare != null)
            textTimpIntrebare.text = "Timp Intrebare: " + FormateazaTimp(timpIntrebare);

        if (textTimpSesiune != null)
            textTimpSesiune.text = "Timp Sesiune: " + FormateazaTimp(timpSesiune);
    }

    string FormateazaTimp(float timp)
    {
        // Transformam secundele in format frumos: minute:secunde
        int minute = Mathf.FloorToInt(timp / 60f);
        int secunde = Mathf.FloorToInt(timp % 60f);

        return minute.ToString("00") + ":" + secunde.ToString("00");
    }

    void AscundePaneluriRezultat()
    {
        // Inchidem ambele panel-uri
        if (panelCorect != null)
            panelCorect.SetActive(false);

        if (panelGresit != null)
            panelGresit.SetActive(false);
    }

    void ActiveazaButoane()
    {
        // Facem butoanele apasabile din nou
        foreach (Button buton in butoaneRaspunsuri)
        {
            if (buton != null)
                buton.interactable = true;
        }
    }

    void DezactiveazaButoane()
    {
        // Blocam butoanele dupa raspuns
        foreach (Button buton in butoaneRaspunsuri)
        {
            if (buton != null)
                buton.interactable = false;
        }
    }
}