using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class References
{
    public static GameObject thePlayer;
    public static GameObject canvas;
    public static EnemySpawner enemySpawner;
    public static AlarmMode alarmMode;
    public static LayerMask wallsLayer = LayerMask.GetMask("Walls");
    public static LayerMask enemyLayer = LayerMask.GetMask("Enemy");
    public static float maxDistanceInLevel = 1000f;
}
