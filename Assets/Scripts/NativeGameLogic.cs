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
    private static extern int GetRemainingSteps(int totalSteps, int currentStep);

    [DllImport(DLL_NAME)]
    private static extern int ClampInt(int value, int min, int max);

    [DllImport(DLL_NAME)]
    private static extern int ShouldMovePawn(int isCorrect);

    [DllImport(DLL_NAME)]
    private static extern int GetTotalAnswered(int correctAnswers, int wrongAnswers);

    [DllImport(DLL_NAME)]
    private static extern int GetNextPlayerIndex(int currentPlayerIndex, int totalPlayers);

    [DllImport(DLL_NAME)]
    private static extern int ShouldKillPawnByDistance(float ax, float ay, float bx, float by, float killDistance);

    public static int AdaugaScor(int scorCurent, int puncte)
    {
        try
        {
            return AddScore(scorCurent, puncte);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback AdaugaScor: " + eroare.Message);
            return scorCurent + puncte;
        }
    }

    public static int CalculeazaIntrebariRamase(int maximIntrebari, int raspunsuriCorecte)
    {
        try
        {
            return GetRemainingCorrectAnswers(maximIntrebari, raspunsuriCorecte);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback CalculeazaIntrebariRamase: " + eroare.Message);

            int ramase = maximIntrebari - raspunsuriCorecte;

            if (ramase < 0)
                ramase = 0;

            return ramase;
        }
    }

    public static int CalculeazaPasiRamasi(int totalPasi, int pasCurent)
    {
        try
        {
            return GetRemainingSteps(totalPasi, pasCurent);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback CalculeazaPasiRamasi: " + eroare.Message);

            int ramase = totalPasi - pasCurent;

            if (ramase < 0)
                ramase = 0;

            return ramase;
        }
    }

    public static int LimiteazaInt(int valoare, int minim, int maxim)
    {
        try
        {
            return ClampInt(valoare, minim, maxim);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback LimiteazaInt: " + eroare.Message);

            if (valoare < minim)
                return minim;

            if (valoare > maxim)
                return maxim;

            return valoare;
        }
    }

    public static bool TrebuieMutatPionul(bool esteCorect)
    {
        try
        {
            int rezultat = ShouldMovePawn(esteCorect ? 1 : 0);
            return rezultat == 1;
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback TrebuieMutatPionul: " + eroare.Message);
            return esteCorect;
        }
    }

    public static int TotalRaspunsuri(int corecte, int gresite)
    {
        try
        {
            return GetTotalAnswered(corecte, gresite);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback TotalRaspunsuri: " + eroare.Message);
            return corecte + gresite;
        }
    }

    public static int UrmatorulJucator(int jucatorCurent, int totalJucatori)
    {
        try
        {
            return GetNextPlayerIndex(jucatorCurent, totalJucatori);
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback UrmatorulJucator: " + eroare.Message);

            if (totalJucatori <= 0)
                return 0;

            return (jucatorCurent + 1) % totalJucatori;
        }
    }

    public static bool TrebuieOmoratPion(Vector3 pozitieAtacator, Vector3 pozitieVictima, float distantaOmor)
    {
        try
        {
            int rezultat = ShouldKillPawnByDistance(
                pozitieAtacator.x,
                pozitieAtacator.y,
                pozitieVictima.x,
                pozitieVictima.y,
                distantaOmor
            );

            return rezultat == 1;
        }
        catch (Exception eroare)
        {
            Debug.LogWarning("NativeGameLogic fallback TrebuieOmoratPion: " + eroare.Message);

            float distanta = Vector2.Distance(
                new Vector2(pozitieAtacator.x, pozitieAtacator.y),
                new Vector2(pozitieVictima.x, pozitieVictima.y)
            );

            return distanta <= distantaOmor;
        }
    }
}