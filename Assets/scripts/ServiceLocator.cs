using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static GameManager gm;

    public static void SetGameManager(GameManager GM) { gm = GM; }
    public static GameManager GetGameManager() { return gm; }

}
