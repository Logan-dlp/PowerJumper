using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Sensibility { get; set; }
    
    [SerializeField] private float _cameraClamp;
    
    private Vector2 _axisMovement = Vector2.zero;
    
    private float _rotationY;
    private float _rotationX;

    private void Update()
    {
        // The camera takes the position of the movement, and the movement takes the rotation Y of the camera.
        _rotationY -= _axisMovement.x * Sensibility;
        _rotationX += _axisMovement.y * Sensibility;

        _rotationX = Mathf.Clamp(_rotationX, -_cameraClamp, _cameraClamp);
        
        transform.parent.rotation = Quaternion.Euler(0, -_rotationY, 0);
        transform.localRotation = Quaternion.Euler(-_rotationX, 0, 0);
    }

    public void SetAxisMovement(Vector2 axis)
    {
        _axisMovement = axis;
    }
}
