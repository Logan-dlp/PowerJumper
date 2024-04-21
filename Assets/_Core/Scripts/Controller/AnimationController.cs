using System;
using UnityEngine;

[RequireComponent
    (typeof(AnimationController),
    typeof(CharacterController))]
public class AnimationController : MonoBehaviour
{
    public Vector2 Axis { get; set; }
    
    private Animator _animator;
    private CharacterController _characterController;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Axis.x != 0 && Mathf.Abs(_characterController.velocity.y) < .01f || Axis.y != 0 && Mathf.Abs(_characterController.velocity.y) < .01f)
        {
            _animator.SetBool("Move", true);
            _animator.SetBool("Jump", false);
        }else if (Mathf.Abs(_characterController.velocity.y) >= .01f)
        {
            _animator.SetBool("Move", false);
            _animator.SetBool("Jump", true);
        }
        else
        {
            _animator.SetBool("Move", false);
            _animator.SetBool("Jump", false);
        }
    }
}
