using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void IncarcaScenaDupaNume(string numeScena)
    {
        // Schimba scena dupa nume
        if (string.IsNullOrEmpty(numeScena))
        {
            Debug.LogWarning("Nu ai pus numele scenei.");
            return;
        }

        SceneManager.LoadScene(numeScena);
    }

    public void IncarcaScenaDupaIndex(int indexScena)
    {
        // Schimba scena dupa indexul din Build Settings
        if (indexScena < 0 || indexScena >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Indexul scenei nu este valid: " + indexScena);
            return;
        }

        SceneManager.LoadScene(indexScena);
    }

    public void ReincarcaScenaCurenta()
    {
        // Restart rapid la scena curenta
        Scene scenaCurenta = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scenaCurenta.buildIndex);
    }

    public void IncarcaUrmatoareaScena()
    {
        // Trece la scena urmatoare din Build Settings
        int indexCurent = SceneManager.GetActiveScene().buildIndex;
        int indexUrmator = indexCurent + 1;

        if (indexUrmator >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Nu exista scena urmatoare in Build Settings.");
            return;
        }

        SceneManager.LoadScene(indexUrmator);
    }

    public void IesireJoc()
    {
        // Merge in build, iar in editor doar afiseaza mesaj
        Debug.Log("Iesire din joc.");

        Application.Quit();
    }
}