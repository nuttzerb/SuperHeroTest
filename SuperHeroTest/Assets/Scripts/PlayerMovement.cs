using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody _rb;
    private float _movementSpeed = 5f;

    private NavMeshAgent _agent;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        ProcessClickToMoveInput();
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

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + new Vector3(_moveInput.x, 0, _moveInput.y) * _movementSpeed * Time.fixedDeltaTime);
    }
    void OnMove(InputValue value)
    {
        _agent.enabled = false;
        _moveInput = value.Get<Vector2>();
        Debug.Log(_moveInput);
    }
}
