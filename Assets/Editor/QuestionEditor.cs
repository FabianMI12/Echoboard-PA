using UnityEngine;
using UnityEditor;
using System.IO;

public class QuestionEditor : EditorWindow
{
    string raspuns = "";
    string variante = "";
    string corect = "";
    string categorie = "";
    string dificultate = "";
    int punctaj = 100;

    [MenuItem("Jeopardy/Adauga Intrebare")]
    public static void ShowWindow()
    {
        GetWindow<QuestionEditor>("Adauga Intrebare");
    }

    void OnGUI()
    {
        GUILayout.Label("Adauga intrebare noua", EditorStyles.boldLabel);

        raspuns = EditorGUILayout.TextField("Raspuns", raspuns);
        variante = EditorGUILayout.TextField("Variante (;)", variante);
        corect = EditorGUILayout.TextField("Corect", corect);
        categorie = EditorGUILayout.TextField("Categorie", categorie);
        dificultate = EditorGUILayout.TextField("Dificultate", dificultate);
        punctaj = EditorGUILayout.IntField("Punctaj", punctaj);

        if (GUILayout.Button("Salveaza"))
        {
            CreeazaIntrebare();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Importa din fisier"))
        {
            ImportaIntrebari();
        }
    }

    void CreeazaIntrebare()
    {
        var data = ScriptableObject.CreateInstance<QuestionData>();

        data.raspuns = raspuns;
        data.variante = variante;
        data.corect = corect;
        data.categorie = categorie;
        data.dificultate = dificultate;
        data.punctaj = punctaj;

        string safeName = raspuns.Replace(" ", "_").Replace("?", "");
        string path = "Assets/Question_" + safeName + ".asset";

        AssetDatabase.CreateAsset(data, path);
        AssetDatabase.SaveAssets();

        Debug.Log("Intrebare salvata!");
    }

    void ImportaIntrebari()
    {
        string path = Application.dataPath + "/intrebari.txt";

        if (!File.Exists(path))
        {
            Debug.LogError("Nu exista fisierul intrebari.txt!");
            return;
        }

        var lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split('|');

            if (parts.Length < 6)
            {
                Debug.LogError("Linie invalida: " + line);
                continue;
            }

            var data = ScriptableObject.CreateInstance<QuestionData>();

            data.raspuns = parts[0];
            data.variante = parts[1];
            data.corect = parts[2];
            data.categorie = parts[3];
            data.dificultate = parts[4];

            int punct;
            if (!int.TryParse(parts[5], out punct))
                punct = 100;

            data.punctaj = punct;

            string safeName = data.raspuns.Replace(" ", "_").Replace("?", "");
            string savePath = "Assets/Question_" + safeName + ".asset";

            AssetDatabase.CreateAsset(data, savePath);
        }

        AssetDatabase.SaveAssets();

        Debug.Log("Import complet!");
    }
}