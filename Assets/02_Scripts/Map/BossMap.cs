using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class BossMap : MonoBehaviour
{
    [SerializeField] private MapDataSO _mapData;

    private GameObject _boss;
    private BossEnemy _bossEnemy;
    [SerializeField] private int _stage = 1;

    private bool _isCollided = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        _isCollided = true;
        if(other.CompareTag("Player"))
        {
            if(_boss != null)
            {
                return;
            }

            StartCoroutine(ShakeCamera());
        }
    }

    public IEnumerator ShakeCamera()
    {
        CameraManager.Instance.ShakeCamera(3);
        yield return new WaitForSeconds(3f);
        SpawnEnemy();
        CameraManager.Instance.ChangeCameraTargetForDuration(_boss.transform, 2f);
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        _isCollided = false;
        if(other.CompareTag("Player"))
        {
            Invoke("DestroyEnemy", 2f);
        }
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.Label(gameObject.transform.position, gameObject.name);
        DrawSpawnAreas(0.3f);
    }

    private void OnDrawGizmosSelected()
    {
        DrawSpawnAreas(0.8f);
    }

    public void SpawnEnemy()
    {
        foreach (var spawnArea in _mapData.EnemySpawnAreas)
        {
            Vector3 spawnPosition = new Vector3(spawnArea.spawnArea.center.x, spawnArea.spawnArea.center.y); 
            spawnPosition = new Vector3(spawnPosition.x - spawnArea.spawnArea.width / 2, spawnPosition.y - spawnArea.spawnArea.height / 2 + 2);
            spawnPosition = transform.TransformPoint(spawnPosition);
            
            for(int i = 0; i < spawnArea.enemies.Count; i++)
            {
                // 아래 왼쪽 부터 조금씩 오른쪽으로
                GameObject enemy = Instantiate(spawnArea.enemies[i], spawnPosition, Quaternion.identity, transform);

                _boss = enemy;
                _bossEnemy = _boss.GetComponent<BossEnemy>();
                _bossEnemy.OnBossDie += OnBossDie;
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

        Destroy(_boss);
        _boss = null;
    }

    private void DrawSpawnAreas(float alpha)
    {
        if (_mapData == null || _mapData.EnemySpawnAreas == null)
            return;

        // 각 스폰 영역을 다른 색상으로 표시
        
        for (int i = 0; i < _mapData.EnemySpawnAreas.Count; i++)
        {
            Rect spawnArea = _mapData.EnemySpawnAreas[i].spawnArea;
            
            Color gizmoColor = Color.red;
            gizmoColor.a = alpha;
            Gizmos.color = gizmoColor;
            
            Vector3 worldCenter = transform.TransformPoint(new Vector3(spawnArea.center.x, spawnArea.center.y));
            Vector3 worldSize = new Vector3(spawnArea.width, spawnArea.height);
            
            Gizmos.DrawWireCube(worldCenter, worldSize);
            
            gizmoColor.a = alpha * 0.2f;
            Gizmos.color = gizmoColor;
            Gizmos.DrawCube(worldCenter, worldSize);
            
            UnityEditor.Handles.Label(worldCenter, $"Spawn Area {i}");
        }
    }

    public void OnBossDie()
    {
        StageManager.Instance.SetMaxClearStage(_stage);
        _bossEnemy.OnBossDie -= OnBossDie;
    }
}
