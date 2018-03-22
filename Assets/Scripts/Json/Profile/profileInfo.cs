using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class profileInfo
{
    public string name;
    public int highScore;
    public string currentRocket;
    public int coins;
    public int rank;
    public Stats  stats;

}
[System.Serializable]
public class Stats
{

    public int challengesWon;
    public int challengesLost;
    public int rocketsUnlocked;
    public int teleportsUsed;
    public int coinsCollected;
    public int coinsSpent;
    public int mostCoinsSingleGame;
    public int rowsWithoutTeleport;
}

