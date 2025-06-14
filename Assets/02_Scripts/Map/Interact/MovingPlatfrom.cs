using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour
{
    [SerializeField] Transform _targetPosition;
    [SerializeField] float _moveSpeed = 10f;
    private Vector3 initialPosition;
    private bool isMoving = false;
    private bool isReverse = false;
    private Transform player;
    private float interval = 0.001f;

    void Awake()
    {
        initialPosition = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Update()
    {

    }

    public IEnumerator MoveToTarget()
    {
        if(transform.position == _targetPosition.position)
        {
            isMoving = false;
            yield break;
        }
        
        while (isMoving)
        {
            // 현재 위치에서 목표 위치로 이동
            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetPosition.position,
                _moveSpeed * interval
            );

            // 도착 체크 (float 오차 방지 위해 Distance 사용 권장)
            if (Vector3.Distance(transform.position, _targetPosition.position) < 0.01f)
            {
                transform.position = _targetPosition.position; // 정확히 맞춤
                isMoving = false;
                yield break;
            }

            yield return new WaitForSeconds(interval);
        }
    }

    public IEnumerator MoveToInitial()
    {
        if(transform.position == initialPosition)
        {
            isMoving = false;
            yield break;
        }

        while (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                initialPosition,
                _moveSpeed * interval
            );

            // 도착 체크 (float 오차 방지 위해 Distance 사용 권장)
            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                transform.position = initialPosition; // 정확히 맞춤
                isMoving = false;
                yield break;
            }

            yield return new WaitForSeconds(interval);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.transform;
            player.SetParent(transform);
            isMoving = true;

            if(isReverse)
            {
                StartCoroutine(MoveToInitial());
            }else
            {
                StartCoroutine(MoveToTarget());
            }

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            player.SetParent(null);
            player = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _targetPosition.position);
    }

    [ContextMenu("ReverseTest")]
    public void ReverseTest()
    {
        isReverse = !isReverse;
    }
}
