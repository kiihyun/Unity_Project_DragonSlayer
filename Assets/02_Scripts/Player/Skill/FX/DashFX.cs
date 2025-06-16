using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFX : MonoBehaviour
{
    [SerializeField] public GameObject afterImagePrefab;
    [SerializeField] private float spawnRate = 0.05f;
    [SerializeField] private float afterImageLifetime = 0.3f;

    private float timeSinceLastSpawn;

    public void UseDash()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnRate)
        {
            SpawnAfterImage();
            timeSinceLastSpawn = 0f;
        }
    }


    private void SpawnAfterImage()
    {
        GameObject clone = Instantiate(afterImagePrefab, transform.position, transform.rotation);
        SpriteRenderer sr = clone.GetComponentInParent<SpriteRenderer>();
        sr.sprite = GetComponentInParent<SpriteRenderer>().sprite;
        sr.flipX = GetComponentInParent<SpriteRenderer>().flipX;
        Destroy(clone, afterImageLifetime);
    }
}
