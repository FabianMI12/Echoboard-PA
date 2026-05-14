using System;
using System.Runtime.InteropServices;
using UnityEngine;

public static class NativeGameLogic
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private const string DLL_NAME = "GameLogic";
#else
    private const string DLL_NAME = "__Internal";
#endif

    [DllImport(DLL_NAME)]
    private static extern int AddScore(int currentScore, int points);

    [DllImport(DLL_NAME)]
    private static extern int GetRemainingCorrectAnswers(int maxQuestions, int correctAnswers);

    [DllImport(DLL_NAME)]
    private static extern int ClampInt(int value, int min, int max);

    [DllImport(DLL_NAME)]
    private static extern int ShouldMovePawn(int isCorrect);

    [DllImport(DLL_NAME)]
    private static extern int GetTotalAnswered(int correctAnswers, int wrongAnswers);

    public static int AdaugaScor(int scorCurent, int puncte)
    {
        // Incercam sa folosim codul C; daca DLL-ul lipseste, jocul tot merge cu fallback C#
        try
        {
            return AddScore(scorCurent, puncte);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic: folosesc fallback C# pentru AdaugaScor. " + eroare.Message);
            return scorCurent + puncte;
        }
    }

    public static int CalculeazaIntrebariRamase(int maximIntrebari, int raspunsuriCorecte)
    {
        // Calculul principal vine din C, fallback-ul e doar ca sa nu crape jocul daca DLL-ul nu e pus
        try
        {
            return GetRemainingCorrectAnswers(maximIntrebari, raspunsuriCorecte);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic: folosesc fallback C# pentru CalculeazaIntrebariRamase. " + eroare.Message);

            int ramase = maximIntrebari - raspunsuriCorecte;

            if (ramase < 0)
                ramase = 0;

            return ramase;
        }
    }

    public static int LimiteazaInt(int valoare, int minim, int maxim)
    {
        // Functie mica utila pentru valori care nu trebuie sa iasa din interval
        try
        {
            return ClampInt(valoare, minim, maxim);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic: folosesc fallback C# pentru LimiteazaInt. " + eroare.Message);

            if (valoare < minim)
                return minim;

            if (valoare > maxim)
                return maxim;

            return valoare;
        }
    }

    public static bool TrebuieMutatPionul(bool esteCorect)
    {
        // C-ul decide daca pionul se muta sau nu
        try
        {
            int rezultat = ShouldMovePawn(esteCorect ? 1 : 0);
            return rezultat == 1;
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic: folosesc fallback C# pentru TrebuieMutatPionul. " + eroare.Message);
            return esteCorect;
        }
    }

    public static int TotalRaspunsuri(int corecte, int gresite)
    {
        // Momentan nu e obligatoriu in UI, dar e util pentru statistici mai tarziu
        try
        {
            return GetTotalAnswered(corecte, gresite);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic: folosesc fallback C# pentru TotalRaspunsuri. " + eroare.Message);
            return corecte + gresite;
        }
    }
}