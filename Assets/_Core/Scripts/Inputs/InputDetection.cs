using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputDetection : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _callbaks;
    
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_playerInput.currentActionMap == _playerInput.actions.actionMaps[1])
        {
            if (_playerInput.currentControlScheme == "Keyboard")
            {
                _callbaks?.Invoke(true);
            }
            else
            {
                _callbaks?.Invoke(false);
            }
        }
        else
        {
            _callbaks.Invoke(true);
        }
    }
}
