using UnityEngine;

[CreateAssetMenu(fileName = "QuestionData", menuName = "Jeopardy/Question")]
public class QuestionData : ScriptableObject
{
    public string raspuns;
    public string variante;
    public string corect;
    public string categorie;
    public string dificultate;   // 🔥 asta lipsea
    public int punctaj;
}