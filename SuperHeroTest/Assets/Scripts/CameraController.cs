using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _isActiveDragRotate;
    private Vector2 _lastMousePosition;
    private Vector3 _lastCameraRotation;
    [SerializeField] private float _cameraRotateSpeed = .3f;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineFramingTransposer _cinemachineFramingTransposer;
    [SerializeField] private float _zoomSpeed = 10f;
    private float _minZoomDistance = 2f;
    private float _maxZoomDistance = 10f;
    private float _currentZoomDistance;

    void Start()
    {
        _currentZoomDistance = Vector3.Distance(transform.position, Vector3.zero);
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (_cinemachineVirtualCamera != null)
        {
            _cinemachineFramingTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else
        {
            Debug.LogError("Null CinemachineVirtualCamera");
        }
    }

    void Update()
    {
        DragToRotate();
        WheelToScroll();
    }
    private void WheelToScroll()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        _currentZoomDistance -= scrollInput * _zoomSpeed;
        _currentZoomDistance = Mathf.Clamp(_currentZoomDistance, _minZoomDistance, _maxZoomDistance);
        _cinemachineFramingTransposer.m_CameraDistance = _currentZoomDistance;
    }
    private void DragToRotate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isActiveDragRotate = true;
            _lastMousePosition = Input.mousePosition;
            _lastCameraRotation = gameObject.transform.eulerAngles;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _isActiveDragRotate = false;
        }
        if (_isActiveDragRotate)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - _lastMousePosition;
            transform.eulerAngles = _lastCameraRotation + new Vector3(-1 * mouseMovementDelta.y, mouseMovementDelta.x) * _cameraRotateSpeed;
        }
    }
    public float GetCurrentZoomDistance()
    {
        return _currentZoomDistance;
    }
}
