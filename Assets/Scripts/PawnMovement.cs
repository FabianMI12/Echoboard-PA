using System;
using System.Collections;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    [Header("Tile-uri de pe marginea tablei")]
    public Transform[] tileuri;

    [Header("Setari miscare")]
    public float vitezaMiscare = 4f;
    public bool pornestePePrimulTile = true;

    public event Action<PawnMovement> OnAjunsLaUltimulTile;
    public event Action<PawnMovement> OnMutareTerminata;

    private int indexTileCurent = 0;
    private bool seMisca = false;
    private int mutariInAsteptare = 0;
    private bool aAnuntatFinalul = false;

    void Start()
    {
        // Punem pionul pe primul tile la start
        if (pornestePePrimulTile && tileuri != null && tileuri.Length > 0)
        {
            transform.position = tileuri[0].position;
        }
    }

    public void MutaLaUrmatorulTile()
    {
        // Daca nu avem traseu, nu avem ce face
        if (tileuri == null || tileuri.Length == 0)
        {
            Debug.LogWarning("Pionul nu are tile-uri setate in PawnMovement.");
            return;
        }

        // Daca suntem deja la final, anuntam jocul
        if (EstePeUltimulTile())
        {
            AnuntaFinalulDacaTrebuie();
            return;
        }

        // Bagam miscarea in coada, ca sa nu pierdem click-uri rapide
        mutariInAsteptare++;

        if (!seMisca)
        {
            StartCoroutine(ProceseazaMutari());
        }
    }

    IEnumerator ProceseazaMutari()
    {
        seMisca = true;

        // Executam toate mutarile primite, una dupa alta
        while (mutariInAsteptare > 0 && !EstePeUltimulTile())
        {
            mutariInAsteptare--;

            indexTileCurent++;

            Vector3 pozitieFinala = tileuri[indexTileCurent].position;

            yield return StartCoroutine(MiscaPion(pozitieFinala));

            OnMutareTerminata?.Invoke(this);

            if (EstePeUltimulTile())
            {
                mutariInAsteptare = 0;
                AnuntaFinalulDacaTrebuie();
                break;
            }
        }

        seMisca = false;
    }

    IEnumerator MiscaPion(Vector3 pozitieFinala)
    {
        // Miscam pionul pana ajunge pe urmatorul tile
        while (Vector3.Distance(transform.position, pozitieFinala) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pozitieFinala,
                vitezaMiscare * Time.deltaTime
            );

            yield return null;
        }

        transform.position = pozitieFinala;
    }

    void AnuntaFinalulDacaTrebuie()
    {
        // Ca sa nu anuntam finalul de mai multe ori
        if (aAnuntatFinalul)
            return;

        aAnuntatFinalul = true;

        Debug.Log(name + " a ajuns la ultimul tile.");
        OnAjunsLaUltimulTile?.Invoke(this);
    }

    public void TrimiteLaStart()
    {
        // Cand un pion este omorat, il trimitem inapoi la tile-ul lui de start
        StopAllCoroutines();

        indexTileCurent = 0;
        mutariInAsteptare = 0;
        seMisca = false;
        aAnuntatFinalul = false;

        if (tileuri != null && tileuri.Length > 0)
        {
            transform.position = tileuri[0].position;
        }

        Debug.Log(name + " a fost trimis la start.");
    }

    public void ReseteazaPion()
    {
        // Reset complet pentru un joc nou
        TrimiteLaStart();
    }

    public bool EstePeUltimulTile()
    {
        if (tileuri == null || tileuri.Length == 0)
            return false;

        return indexTileCurent >= tileuri.Length - 1;
    }

    public int GetIndexTileCurent()
    {
        return indexTileCurent;
    }

    public int GetNumarTileuri()
    {
        if (tileuri == null)
            return 0;

        return tileuri.Length;
    }

    public int GetTotalPasiPanaLaFinal()
    {
        if (tileuri == null || tileuri.Length == 0)
            return 0;

        return tileuri.Length - 1;
    }

    public int GetPasiRamasi()
    {
        int totalPasi = GetTotalPasiPanaLaFinal();
        return NativeGameLogic.CalculeazaPasiRamasi(totalPasi, indexTileCurent);
    }
}