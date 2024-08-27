using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody _rb;
    private float _movementSpeed = 5f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + new Vector3(_moveInput.x, 0, _moveInput.y) * _movementSpeed * Time.fixedDeltaTime);
    }
    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
}
