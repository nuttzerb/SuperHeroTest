using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _isActiveDragRotate;
    private Vector2 _lastMousePosition;
    private Vector3 _lastCameraRotation;
    // Update is called once per frame
    [SerializeField] private float _cameraRotateSpeed = .3f;
    void Update()
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
}
