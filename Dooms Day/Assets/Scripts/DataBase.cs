using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataBase
{
    public static float tracespeed = 6f, AIattacktime = 0.65f, AImovetime = 0.65f;
    public static bool isPause = false, isHell = false, PauseMenuAvailable = false;

    public static int characterID = 0, mapID = 0, AILevel = 1;

    // Online
    public static int playerID = 1;
    public static string WinnerName = "?";
    public static bool isOpt = false;

    public static string NickName = "";

    // Settings
    public static float FullVolume = 1.0f, BackgroundVolume = 0.2f, EffectVolume1 = 1.0f, EffectVolume2 = 0.5f;
    public static bool isFullScreen = true;
}
