using System.Collections;
using UnityEngine;

public class PawnMovement : MonoBehaviour
{
    [Header("Tile-uri de pe marginea tablei")]
    public Transform[] tileuri;

    [Header("Setari miscare")]
    public float vitezaMiscare = 4f;
    public bool pornestePePrimulTile = true;

    private int indexTileCurent = 0;
    private bool seMisca = false;

    void Start()
    {
        // Punem pionul pe primul tile, daca vrem sa fie lipit perfect de tabla
        if (pornestePePrimulTile && tileuri.Length > 0)
        {
            transform.position = tileuri[0].position;
        }
    }

    public void MutaLaUrmatorulTile()
    {
        // Daca deja se misca, nu il mai trimitem inca o data
        if (seMisca)
            return;

        // Daca nu avem tile-uri puse in Inspector, nu avem ce misca
        if (tileuri == null || tileuri.Length == 0)
        {
            Debug.LogWarning("Pionul nu are tile-uri setate in PawnMovement.");
            return;
        }

        // Daca am ajuns la ultimul tile, ne oprim acolo
        if (indexTileCurent >= tileuri.Length - 1)
        {
            Debug.Log("Pionul este deja pe ultimul tile.");
            return;
        }

        indexTileCurent++;

        StartCoroutine(MiscaPion(tileuri[indexTileCurent].position));
    }

    IEnumerator MiscaPion(Vector3 pozitieFinala)
    {
        // Miscam pionul frumos pana la urmatorul tile
        seMisca = true;

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
        seMisca = false;
    }

    public void ReseteazaPion()
    {
        // Il trimitem inapoi la start
        indexTileCurent = 0;

        if (tileuri != null && tileuri.Length > 0)
        {
            transform.position = tileuri[0].position;
        }
    }
}