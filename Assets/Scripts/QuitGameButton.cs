using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public void QuitGame()
    {
        // In build, this closes the game
        Application.Quit();

        // In Unity Editor, Application.Quit() does nothing, so this helps us see it worked
        Debug.Log("Quit button pressed. Game would close in build.");
    }
}