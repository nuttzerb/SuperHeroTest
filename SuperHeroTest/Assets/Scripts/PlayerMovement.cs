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
    private float rotationSpeed = 555f; // Rotation speed in degrees per second

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        ProcessClickToMoveInput();
        FaceTarget();
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveInput * _movementSpeed * Time.fixedDeltaTime);
    }
    void OnMove(InputValue value)
    {
        _agent.enabled = false;
        _moveInput = new Vector3(value.Get<Vector2>().x, 0, value.Get<Vector2>().y);
        Debug.Log(_moveInput);
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

    private void FaceTarget()
    {
        if (_agent.enabled)
        {
            Vector3 direction = (_agent.destination - transform.position).normalized;
            if(direction!= Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(_playerVisual.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (_moveInput.magnitude >= 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(_moveInput);
                transform.rotation = Quaternion.Slerp(_playerVisual.gameObject.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
