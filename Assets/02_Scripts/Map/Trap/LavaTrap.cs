using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    [SerializeField] private float _maxLaserRange = 20f;

    [SerializeField] private GameObject _beamPrefab;
    [SerializeField] private LayerMask _levelLayerMask;
    [SerializeField] private float _startDelay = 0f;
    [SerializeField] private float _switchDuration = 2f;

    private float _laserRange = 0f;
    private Transform _beamTransform;

    private bool _isActive = true;

    
    private void OnEnable()
    {
        _isActive = true;
    }

    private void OnDisable()
    {
        _isActive = false;
    }

    public void Start()
    {
        _beamTransform = _beamPrefab.transform;

        _laserRange = _maxLaserRange;

        _beamPrefab.SetActive(false);

        StartCoroutine(SwitchLazer(_switchDuration));
    }

    public IEnumerator SwitchLazer(float duration)
    {
        yield return new WaitForSeconds(_startDelay);
        
        while(_isActive)
        {
            _beamPrefab.SetActive(true);

            yield return new WaitForSeconds(duration);

            _beamPrefab.SetActive(false);

            yield return new WaitForSeconds(duration);
        }
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_beamTransform.position, -transform.up, _maxLaserRange, _levelLayerMask);

        if(hit.collider != null && _levelLayerMask.value == (_levelLayerMask.value | (1 << hit.collider.gameObject.layer)))
        {
            _laserRange = hit.distance;
            Debug.DrawRay(_beamTransform.position, -transform.up * _laserRange, Color.red);

            Vector3 newScale = _beamPrefab.transform.localScale;
            newScale.x = _laserRange;
            _beamPrefab.transform.localScale = newScale;
        }        
        else
        {
            Debug.DrawRay(_beamTransform.position, -transform.up * _maxLaserRange, Color.green);
            Vector3 newScale = _beamPrefab.transform.localScale;
            newScale.x = _maxLaserRange;
            _beamPrefab.transform.localScale = newScale;
        }
    }

}
