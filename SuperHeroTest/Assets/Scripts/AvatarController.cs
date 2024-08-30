using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offset = 2.1f;
    [SerializeField] private Camera _mainCamera;

    private void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        transform.position = Camera.main.WorldToScreenPoint(_player.transform.position + Vector3.up * _offset);
    }

}
