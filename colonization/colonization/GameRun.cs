using Microsoft.Xna.Framework;

class GameRun
{
    public Map MyMap { get; set; }

    public GameRun()
    {
        MyMap = new Map("testMapBitMap", "wallsTopDownTileSet", 96, 96);
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
