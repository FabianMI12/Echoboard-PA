using UnityEngine;

public class GameModeSelector : MonoBehaviour
{
    [Header("Setari multiplayer")]
    public int numarJucatoriMultiplayer = 2;

    public void AlegeSingleplayer()
    {
        GameSettings.SeteazaSingleplayer();

        Debug.Log("Mod ales: Singleplayer");
    }

    public void AlegeMultiplayerLocal()
    {
        GameSettings.SeteazaMultiplayerLocal(numarJucatoriMultiplayer);

        Debug.Log("Mod ales: Multiplayer local cu " + numarJucatoriMultiplayer + " jucatori");
    }

    public void SeteazaMultiplayerCu2Jucatori()
    {
        GameSettings.SeteazaMultiplayerLocal(2);

        Debug.Log("Mod ales: Multiplayer local cu 2 jucatori");
    }

    public void SeteazaMultiplayerCu3Jucatori()
    {
        GameSettings.SeteazaMultiplayerLocal(3);

        Debug.Log("Mod ales: Multiplayer local cu 3 jucatori");
    }

    public void SeteazaMultiplayerCu4Jucatori()
    {
        GameSettings.SeteazaMultiplayerLocal(4);

        Debug.Log("Mod ales: Multiplayer local cu 4 jucatori");
    }
}