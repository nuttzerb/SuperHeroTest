using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _moveInput;
    private Rigidbody _rb;
    [SerializeField] private float _movementSpeed = 5f;
    private NavMeshAgent _agent;
    [SerializeField] private PlayerVisual _playerVisual;
    private float _rotationSpeed = 555f; // Rotation speed in degrees per second
    private Vector3 _agentMoveDirection;
    private Vector3 _inputMoveDirection;

    [SerializeField] CameraController _cameraController;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        ProcessClickToMoveInput();
        UpdatePlayerRotation();
        UpdateRunningAnimation();

        if (_agent.isStopped)
        {
            _inputMoveDirection = _cameraController.transform.forward * _moveInput.z + _cameraController.transform.right * _moveInput.x;
            _inputMoveDirection.y = 0;
            _inputMoveDirection.Normalize();
            _agent.Move(_inputMoveDirection * _agent.speed * Time.deltaTime);
        }
    }
    void OnMove(InputValue value)
    {
        if (ChatUIManager.isChatInputFocused) return;
        _moveInput = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
        _agent.isStopped = true;
    }
    private void ProcessClickToMoveInput()
    {
        if (ChatUIManager.onMousePointer) return;

        if (Input.GetMouseButtonDown(0))
        {
            _agent.isStopped = false;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo))
            {
                _agent.destination = hitInfo.point;
            }
        }

    }

    private void UpdatePlayerRotation()
    {
        if (_agent.isStopped == false)
        {
            _agentMoveDirection = (_agent.steeringTarget - transform.position).normalized;
            if (_agentMoveDirection != Vector3.zero)
            {

                FaceTarget(_agentMoveDirection);
            }
        }
        else
        {
            if (_moveInput.magnitude >= 0.1f)
            {
                FaceTarget(_inputMoveDirection);
            }
        }
    }

    private void FaceTarget(Vector3 rotation)
    {
        Quaternion lookRotation = Quaternion.LookRotation(rotation);
        _playerVisual.gameObject.transform.rotation = Quaternion.Slerp(_playerVisual.gameObject.transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }

    private void UpdateRunningAnimation()
    {
        if (_agent.isStopped == false)
        {
            _playerVisual.SetRunningAnimation(_agentMoveDirection != Vector3.zero);
        }
        else
        {
            _playerVisual.SetRunningAnimation(_moveInput != Vector3.zero);
        }
    }
}
