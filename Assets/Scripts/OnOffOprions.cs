using UnityEngine;

public class OnOffOptions : MonoBehaviour
{
    public GameObject optionMenu;

    public void OnCick()
    {   
        if(optionMenu.activeInHierarchy) optionMenu.SetActive(false);
        else optionMenu.SetActive(true);
    }
}
