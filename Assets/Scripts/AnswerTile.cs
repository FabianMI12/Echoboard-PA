using UnityEngine;
using UnityEngine.UI;


public class AnswerTile : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject answerBar;
    public GameObject board;

    public void OnClick()
    {
        questionPanel.SetActive(false);
        for (int i = 0; i < answerBar.transform.childCount; i++)
        {
            answerBar.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < board.transform.childCount; i++)
        {
            board.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}