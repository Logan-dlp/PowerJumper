using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _axisMovement = Vector3.zero;

    [SerializeField] private float _speedMovement = 5;
    [SerializeField] private float _jumpForce = 10;
    [SerializeField] private float _gravity = 5;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void LateUpdate()
    {
        _characterController.Move(-transform.up * _gravity * Time.deltaTime);
        _characterController.Move(transform.forward * _axisMovement.y * _speedMovement * Time.deltaTime);
        _characterController.Move(transform.right * _axisMovement.x * _speedMovement * Time.deltaTime);
        
        Debug.Log(_characterController.velocity.y);
    }

    public void Jump()
    {
        _characterController.Move(transform.up * _jumpForce * Time.deltaTime * .5f);
    }

    public void SetAxisMovement(Vector2 axis)
    {
        _axisMovement = axis;
    }
}
