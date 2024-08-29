using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    [SerializeField] private Transform _player; 
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, 0);
    [SerializeField] private Camera _mainCamera;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        transform.position = _player.position + _offset;

        Vector3 directionToCamera = _mainCamera.transform.position - transform.position;
     //   directionToCamera.y = 0; 
        transform.forward = directionToCamera.normalized;
    }
}
