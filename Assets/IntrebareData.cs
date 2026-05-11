using UnityEngine;

[CreateAssetMenu(fileName = "IntrebareNoua", menuName = "Jeopardy/Intrebare")]
public class IntrebareData : ScriptableObject
{
    public string raspunsAfisat;

    public string[] variante = new string[4];

    public int indexCorect;

    public string categorie;

    public int punctaj;
}