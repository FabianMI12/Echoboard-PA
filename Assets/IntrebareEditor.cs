using UnityEngine;
using UnityEditor;

public class IntrebareEditor : EditorWindow
{
    string raspuns = "";
    string[] variante = new string[4];
    int corect = 0;
    string categorie = "Biologie";
    int punctaj = 100;

    [MenuItem("Jeopardy/Adauga Intrebare")]
    public static void ShowWindow()
    {
        GetWindow<IntrebareEditor>("Intrebare Noua");
    }

    void OnGUI()
    {
        GUILayout.Label("Adauga Intrebare", EditorStyles.boldLabel);

        raspuns = EditorGUILayout.TextField("Raspuns", raspuns);

        for (int i = 0; i < 4; i++)
        {
            variante[i] = EditorGUILayout.TextField("Varianta " + (i + 1), variante[i]);
        }

        corect = EditorGUILayout.IntSlider("Index corect", corect, 0, 3);
        categorie = EditorGUILayout.TextField("Categorie", categorie);
        punctaj = EditorGUILayout.IntField("Punctaj", punctaj);

        if (GUILayout.Button("Salveaza"))
        {
            CreeazaIntrebare();
        }
    }

    void CreeazaIntrebare()
    {
        var intrebare = ScriptableObject.CreateInstance<IntrebareData>();

        intrebare.raspunsAfisat = raspuns;
        intrebare.variante = variante;
        intrebare.indexCorect = corect;
        intrebare.categorie = categorie;
        intrebare.punctaj = punctaj;

        if (!AssetDatabase.IsValidFolder("Assets/Intrebari"))
        {
            AssetDatabase.CreateFolder("Assets", "Intrebari");
        }

        string path = "Assets/Intrebari/" + raspuns.Replace(" ", "_") + ".asset";

        AssetDatabase.CreateAsset(intrebare, path);
        AssetDatabase.SaveAssets();

        Debug.Log("Intrebare salvata!");

        raspuns = "";
        variante = new string[4];
        corect = 0;
    }
}