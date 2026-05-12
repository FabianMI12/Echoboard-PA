using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    List<QuestionData> intrebari;

    void Start()
    {
        intrebari = Resources.LoadAll<QuestionData>("").ToList();

        Debug.Log("Am gasit " + intrebari.Count + " intrebari");

        AfiseazaIntrebari();
    }

    void AfiseazaIntrebari()
    {
        var random = new System.Random();

        var lista = intrebari.OrderBy(x => random.Next()).ToList();

        foreach (var intrebare in lista)
        {
            Debug.Log("-----");
            Debug.Log("Raspuns: " + intrebare.raspuns);

            var variante = intrebare.variante.Split(';')
                .OrderBy(x => random.Next())
                .ToList();

            foreach (var v in variante)
            {
                Debug.Log(v);
            }

            Debug.Log("Corect: " + intrebare.corect);
        }
    }
}