public static class GameSettings
{
    public static string dificultateAleasa = "Usoara";

    public static bool multiplayerLocal = false;

    public static int numarJucatoriMultiplayer = 2;

    public static void SeteazaDificultate(string dificultateNoua)
    {
        dificultateAleasa = dificultateNoua;
    }

    public static void SeteazaSingleplayer()
    {
        multiplayerLocal = false;
    }

    public static void SeteazaMultiplayerLocal(int numarJucatori)
    {
        multiplayerLocal = true;
        numarJucatoriMultiplayer = numarJucatori;
    }

    public static bool EsteSingleplayer()
    {
        return multiplayerLocal == false;
    }

    public static bool EsteMultiplayerLocal()
    {
        return multiplayerLocal == true;
    }
}