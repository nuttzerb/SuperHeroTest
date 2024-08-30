using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChatTextPopup : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offset = 2.1f;
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private Text _text;

    private float _disapearTimer;
    private float _timeToDisapear = 5f;


    private void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
    }

    public void InitTextPopup(string text)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        _disapearTimer = _timeToDisapear;
        _text.text = text;

    }
    void Update()
    {
        if (_disapearTimer >= 0)
        {
            _disapearTimer -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);

        }
    }
    void LateUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.position = Camera.main.WorldToScreenPoint(_player.transform.position + Vector3.up * _offset);
        }
    }
}
