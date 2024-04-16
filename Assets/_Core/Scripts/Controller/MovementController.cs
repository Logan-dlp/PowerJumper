using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    public bool IsLocked { get; set; }
    
    [SerializeField] private float _speedMovement = 5;
    [SerializeField] private float _jumpForce = 1;
    
    private CharacterController _characterController;
    
    private Vector3 _axisMovement = Vector3.zero;
    private Vector3 _characterVelocity;
    
    private float _gravity = -9.81f;
    private bool _isGrounded = false;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (IsLocked) 
            return;
        
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _characterVelocity.y < 0)
        {
            _characterVelocity.y = 0f;
        }
        
        Vector3 move = transform.forward * _axisMovement.y + transform.right * _axisMovement.x;
        _characterController.Move(move * _speedMovement * Time.fixedDeltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        _characterVelocity.y += _gravity * Time.fixedDeltaTime;
        _characterController.Move(_characterVelocity * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        if (IsLocked)
            return;
        
        if (_characterController.isGrounded)
        {
            _characterVelocity.y += Mathf.Sqrt(_jumpForce * -3.0f * _gravity);
        }
    }

    public void AddUpwardForce(float force)
    {
        _characterVelocity.y += force;
        Debug.Log(_characterVelocity.y);
    }

    public void SetAxisMovement(Vector2 axis)
    {
        _axisMovement = axis;
    }
}
