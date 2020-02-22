using Microsoft.Xna.Framework;

public class GameRun
{
    public Map MyMap { get; set; }

    public GameRun()
    {
        MyMap = new Map("map");
    }

    public Main.EnumMainState GameRunUpdate(GameTime pGameTime, Main.EnumMainState pMyState)
    {
        MyMap.MapUpdate(pGameTime);
        return pMyState;
    }

    public void GameRunDraw(GameTime pGameTime)
    {
        MyMap.MapDraw(pGameTime);
    }
}
