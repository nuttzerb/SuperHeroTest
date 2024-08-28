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
    private float _movementSpeed = 5f;

    private NavMeshAgent _agent;

    [SerializeField] private PlayerVisual _playerVisual;
    private float _rotationSpeed = 555f; // Rotation speed in degrees per second
    private Vector3 _agentMoveDirection;

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
    }

    void FixedUpdate()
    {
        if (_moveInput.magnitude > 0)
        {
            _rb.MovePosition(_rb.position + _moveInput * _movementSpeed * Time.fixedDeltaTime);
        }
    }
    void OnMove(InputValue value)
    {
        if (_agent.enabled)
        {
            _agent.enabled = false;
        }
        _moveInput = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);

    }
    private void ProcessClickToMoveInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _agent.enabled = true;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo))
            {
                Vector3 randomOffset = Random.insideUnitCircle * 0.5f;
                randomOffset.y = 0;
                _agent.destination = hitInfo.point + randomOffset;
            }
        }
    }

    private void UpdatePlayerRotation()
    {
        if (_agent.enabled)
        {
            _agentMoveDirection = (_agent.destination - transform.position).normalized;
            if (_agentMoveDirection != Vector3.zero)
            {
                FaceTarget(new Vector3(_agentMoveDirection.x, 0, _agentMoveDirection.z));
            }
        }
        else
        {
            if (_moveInput.magnitude >= 0.1f)
            {
                FaceTarget(_moveInput);
            }
        }
    }

    private void FaceTarget(Vector3 rotation)
    {
        Quaternion lookRotation = Quaternion.LookRotation(rotation);
        transform.rotation = Quaternion.Slerp(_playerVisual.gameObject.transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }

    private void UpdateRunningAnimation()
    {
        if (_agent.enabled)
        {
            _playerVisual.SetRunningAnimation(_agentMoveDirection != Vector3.zero);
        }
        else
        {
            _playerVisual.SetRunningAnimation(_moveInput != Vector3.zero);
        }
    }
}
