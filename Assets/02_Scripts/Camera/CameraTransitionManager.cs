using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using DG.Tweening;

public class CameraTransitionManager : MonoBehaviour
{
    public static CameraTransitionManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("Camera Settings")]
    [SerializeField] private List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
    [SerializeField] private int activeCameraIndex = 0;
    
    [Header("Bounding Settings")]
    [SerializeField] private List<Collider2D> boundingShapes = new List<Collider2D>();
    [SerializeField] private CinemachineConfiner2D cameraConfiner;
    
    private CinemachineVirtualCamera currentActiveCamera;

    private int previousCameraIndex = 0;
    private Collider2D previousBoundingShape;
    private Transform _player;
    
    void Start()
    {
        InitializeCameras();
        _player = currentActiveCamera.Follow;
    }

    
    private void InitializeCameras()
    {
        if (virtualCameras.Count == 0)
        {
            Debug.LogWarning("Virtual Camera 리스트가 비어있습니다!");
            return;
        }
        
        // 모든 카메라의 Priority를 0으로 설정
        foreach (var camera in virtualCameras)
        {
            if (camera != null)
            {
                camera.Priority = 0;
            }
        }
        
        // 첫 번째 카메라를 활성화
        if (activeCameraIndex >= 0 && activeCameraIndex < virtualCameras.Count)
        {
            SetActiveCamera(activeCameraIndex);
        }
    }

    public void SwitchToCamera(int cameraIndex)
    {
        SetActiveCamera(cameraIndex);
    }
    
    private void SetActiveCamera(int cameraIndex)
    {
        if (currentActiveCamera != null)
        {
            currentActiveCamera.Priority = 0;
        }
        
        currentActiveCamera = virtualCameras[cameraIndex];
        currentActiveCamera.Priority = 10;
        previousCameraIndex = activeCameraIndex;
        activeCameraIndex = cameraIndex;
        
        Debug.Log($"카메라 전환: {currentActiveCamera.name}");
    }

    public Collider2D GetBoundingShape(int cameraIndex)
    {
        return boundingShapes[cameraIndex];
    }
    
    public void ChangeBoundingShape(Collider2D newBoundingShape)
    {
        if (cameraConfiner != null && newBoundingShape != null)
        {
            previousBoundingShape = cameraConfiner.m_BoundingShape2D;
            cameraConfiner.m_BoundingShape2D = newBoundingShape;
        }
    }
    
    public int GetActiveCameraIndex()
    {
        return activeCameraIndex;
    }

    public int GetPreviousMapId()
    {
        return previousCameraIndex;
    }

    public Collider2D GetPreviousBoundingShape()
    {
        return previousBoundingShape;
    }
    
    public CinemachineVirtualCamera GetActiveCamera()
    {
        return currentActiveCamera;
    }
    
    public Collider2D GetCurrentBoundingShape()
    {
        return cameraConfiner?.m_BoundingShape2D;
    }

    public void ShakeCamera(int duration)
    {
        currentActiveCamera.GetComponent<CameraShake>().ShakeCamera(duration, 2f, 2f);
    }

    public void ChangeCameraTargetForDuration(Transform target, float duration)
    {
        currentActiveCamera.Follow = target;
        DOVirtual.DelayedCall(duration, () =>
        {
            currentActiveCamera.Follow = _player;
        });
    }

    [ContextMenu("CameraTransitionTest")]
    public void CameraTransitionTest()
    {
        if(GetActiveCameraIndex() == 0)
        {
            SwitchToCamera(1);
        }
        else
        {
            SwitchToCamera(0);
        }
    }
} 