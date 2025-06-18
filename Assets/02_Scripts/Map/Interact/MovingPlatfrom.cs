using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatfrom : MonoBehaviour, IInteractableTarget
{
    [SerializeField] Transform _targetPosition;
    [SerializeField] float _moveSpeed = 10f;

    private Vector3 _initialPosition;
    private Transform _player;
    private float _interval = Constants.Interaction.MOVING_PLATFORM_INTERVAL;

    private void Awake()
    {
        _initialPosition = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void FixedUpdate()
    {
        if(_player != null)
        {
            if(!IsArrive())
            {
                MoveToTarget();
            }
        }else       
        {
            if(!IsInitialPosition())
            {
                MoveToInitial();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player _))
        {
            _player = other.gameObject.transform;
            _player.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.TryGetComponent<Player>(out Player _))
        {
            _player.SetParent(null);
            _player = null;
        }
    }

    public void MoveToTarget()
    {
        if(transform.position == _targetPosition.position)
        {
            return;
        }
        
        // 현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetPosition.position,
            _moveSpeed * _interval
        );

        // 도착 체크 (float 오차 방지 위해 Distance 사용 권장)
        if (Vector3.Distance(transform.position, _targetPosition.position) < Constants.Interaction.MOVING_PLATFORM_ARRIVE_THRESHOLD)
        {
            transform.position = _targetPosition.position; // 정확히 맞춤
        }

    }

    public void MoveToInitial()
    {
        if(transform.position == _initialPosition)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _initialPosition,
            _moveSpeed * _interval
        );

        // 도착 체크 (float 오차 방지 위해 Distance 사용 권장)
        if (Vector3.Distance(transform.position, _initialPosition) < Constants.Interaction.MOVING_PLATFORM_ARRIVE_THRESHOLD)
        {
            transform.position = _initialPosition; // 정확히 맞춤
        }

    }

    public bool IsArrive()
    {
        if(transform.position == _targetPosition.position)
        {
            return true;
        }
        return false;
    }

    public bool IsInitialPosition()
    {
        if(transform.position == _initialPosition)
        {
            return true;
        }
        return false;
    }
    public void Reverse()
    {
        Vector3 temp = _initialPosition;
        _initialPosition = _targetPosition.position;
        _targetPosition.position = temp;
    }

    public void OnLeverActivated()
    {
        Reverse();
    }

    public void OnLeverDeactivated()
    {
        Reverse();
    }
}
