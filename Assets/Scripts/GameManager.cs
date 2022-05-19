public static class GameManager
{
    public static bool Multiplayer { get; set; }

    static GameManager()
    {
        Multiplayer = false;
    }
}
