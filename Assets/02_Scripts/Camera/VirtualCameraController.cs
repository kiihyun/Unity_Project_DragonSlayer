using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{

    CameraManager _cameraTransitionManager;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _cameraTransitionManager = CameraManager.Instance;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(_cameraTransitionManager == null)
        {
            _cameraTransitionManager = CameraManager.Instance;
        }

        if(other.TryGetComponent<Player>(out Player player))
        {
            _cameraTransitionManager.ChangeBoundingShape(_collider);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(_cameraTransitionManager == null)
        {
            _cameraTransitionManager = CameraManager.Instance;
        }

        if(other.TryGetComponent<Player>(out Player player))
        {
            if(_cameraTransitionManager.GetCurrentBoundingShape() == _collider)
            {
                _cameraTransitionManager.ChangeBoundingShape(_cameraTransitionManager.GetPreviousBoundingShape());
            }
        }
    }
}
