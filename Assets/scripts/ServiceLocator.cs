using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static GameManager gm;

    public static void SetGameManager(GameManager GM) { gm = GM; }
    public static GameManager GetGameManager() { return gm; }

    private static Player player;

    public static void SetPlayer(Player PL) { player = PL; }
    public static Player GetPlayer() { return player; }

    private static LevelGen lg;
    public static void SetLevelGen(LevelGen LG) {lg = LG; }
    public static LevelGen GetLevelGen() { return lg; }


}
