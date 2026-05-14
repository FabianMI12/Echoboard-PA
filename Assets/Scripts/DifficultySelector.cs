using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public void AlegeUsor()
    {
        // In fisierul de intrebari, dificultatea este scrisa ca "Usoara"
        GameSettings.SeteazaDificultate("Usoara");

        Debug.Log("Dificultate aleasa: Usoara");
    }

    public void AlegeMediu()
    {
        // In fisierul de intrebari, dificultatea este scrisa ca "Medie"
        GameSettings.SeteazaDificultate("Medie");

        Debug.Log("Dificultate aleasa: Medie");
    }

    public void AlegeGreu()
    {
        // In fisierul de intrebari, dificultatea este scrisa ca "Grea"
        GameSettings.SeteazaDificultate("Grea");

        Debug.Log("Dificultate aleasa: Grea");
    }
}