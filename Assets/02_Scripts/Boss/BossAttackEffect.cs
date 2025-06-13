using UnityEngine;

public class BossAttackEffect : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public Vector2 direction = Vector2.right;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // 일정 시간 후 파괴
    }

    private void Update()
    {
        transform.Translate(-direction * speed * Time.deltaTime);
    }
}