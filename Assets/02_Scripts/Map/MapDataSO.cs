using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Map/MapData")]
public class MapDataSO : ScriptableObject
{
    public int mapId;
    public string mapName;
    public List<EnemySpawnArea> enemySpawnAreas;

    [System.Serializable]
    public class EnemySpawnArea
    {
        public Rect spawnArea;
        // public List<EnemyData> enemies;
        public List<GameObject> enemies;
    }
}
