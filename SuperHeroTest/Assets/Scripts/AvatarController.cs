using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    [SerializeField] private Transform _player; 
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, 0);
    [SerializeField] private Camera _mainCamera;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        transform.position = _player.position + _offset;

        Vector3 directionToCamera = _mainCamera.transform.position - transform.position;
        transform.forward = directionToCamera.normalized;
    }

    public RectTransform GetRectTransform()
    {
        return _rectTransform;
    }
}
