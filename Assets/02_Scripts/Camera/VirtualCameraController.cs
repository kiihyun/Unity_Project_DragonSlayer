using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{

    CameraTransitionManager _cameraTransitionManager;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _cameraTransitionManager = CameraTransitionManager.Instance;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(_cameraTransitionManager == null)
        {
            _cameraTransitionManager = CameraTransitionManager.Instance;
        }

        if(other.CompareTag("Player"))
        {
            _cameraTransitionManager.ChangeBoundingShape(_collider);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(_cameraTransitionManager == null)
        {
            _cameraTransitionManager = CameraTransitionManager.Instance;
        }

        if(other.CompareTag("Player"))
        {
            if(_cameraTransitionManager.GetCurrentBoundingShape() == _collider)
            {
                _cameraTransitionManager.ChangeBoundingShape(_cameraTransitionManager.GetPreviousBoundingShape());
            }
        }
    }
}
