using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private MapDataSO _mapData;

    private List<GameObject> _enemies = new List<GameObject>();
    private bool _isCollided = false;

    
    public void OnTriggerEnter2D(Collider2D other)
    {
        _isCollided = true;
        if(other.TryGetComponent<Player>(out Player _))
        {
            if(_enemies.Count > 0)
            {
                return;
            }

            SpawnEnemy();
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        _isCollided = false;
        if(other.TryGetComponent<Player>(out Player _))
        {
            Invoke("DestroyEnemy", 2f);
        }
    }

    public void SpawnEnemy()
    {
        if(_mapData == null || _mapData.enemySpawnAreas == null)
        { return; }
        foreach (var spawnArea in _mapData.enemySpawnAreas)
        {
            Vector3 spawnPosition = new Vector3(spawnArea.spawnArea.center.x, spawnArea.spawnArea.center.y); 
            spawnPosition = new Vector3(spawnPosition.x - spawnArea.spawnArea.width / 2, spawnPosition.y - spawnArea.spawnArea.height / 2 + 2);
            spawnPosition = transform.TransformPoint(spawnPosition);
            
            for(int i = 0; i < spawnArea.enemies.Count; i++)
            {
                // 아래 왼쪽 부터 조금씩 오른쪽으로
                GameObject enemy = Instantiate(spawnArea.enemies[i], spawnPosition, Quaternion.identity, transform);

                _enemies.Add(enemy);
                spawnPosition = new Vector3(spawnPosition.x + 2, spawnPosition.y);
            }
        }
    }

    private void DestroyEnemy()
    {
        if(_isCollided)
        {
            return;
        }

        foreach (var enemy in _enemies)
        {
            Destroy(enemy);
        }
        _enemies.Clear();
    }

    // private void OnDrawGizmosSelected()
    // {
    //     DrawSpawnAreas(0.8f);
    // }

    // private void DrawSpawnAreas(float alpha)
    // {
    //     if (_mapData == null || _mapData.enemySpawnAreas == null)
    //         return;

    //     // 각 스폰 영역을 다른 색상으로 표시
        
    //     for (int i = 0; i < _mapData.enemySpawnAreas.Count; i++)
    //     {
    //         Rect spawnArea = _mapData.enemySpawnAreas[i].spawnArea;
            
    //         Color gizmoColor = Color.red;
    //         gizmoColor.a = alpha;
    //         Gizmos.color = gizmoColor;
            
    //         Vector3 worldCenter = transform.TransformPoint(new Vector3(spawnArea.center.x, spawnArea.center.y));
    //         Vector3 worldSize = new Vector3(spawnArea.width, spawnArea.height);
            
    //         Gizmos.DrawWireCube(worldCenter, worldSize);
            
    //         gizmoColor.a = alpha * 0.2f;
    //         Gizmos.color = gizmoColor;
    //         Gizmos.DrawCube(worldCenter, worldSize);
            
    //         UnityEditor.Handles.Label(worldCenter, $"Spawn Area {i}");
    //     }
    // }
}
