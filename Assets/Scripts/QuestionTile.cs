using UnityEngine;
using UnityEngine.UI;


public class QuestionTile : MonoBehaviour
{
    public GameObject questionPanel;
    public GameObject answerBar;

    public void OnClick()
    {
        questionPanel.SetActive(true);
        for (int i = 0; i < questionPanel.transform.childCount; i++)
        {
            questionPanel.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < answerBar.transform.childCount; i++)
        {
            answerBar.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}