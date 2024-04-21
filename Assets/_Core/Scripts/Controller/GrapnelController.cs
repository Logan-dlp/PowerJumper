using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(
    typeof(CharacterController), 
    typeof(MovementController),
    typeof(LineRenderer))]
[RequireComponent(typeof(Animator))]
public class GrapnelController : MonoBehaviour
{
    [SerializeField] private float _grapnelSpeed = 1;
    [SerializeField] private float _cooldownSpeed = 1;
    [SerializeField] private float _distanceStopGrap = 3;
    [SerializeField] private int _distanceToRay = 100;
    [SerializeField] private GameObject _vfxGrapnel;
    [SerializeField] private Transform _lineStart;
    [SerializeField] private LayerMask _layerMaskToGrape;
    [SerializeField] private UnityEvent<bool> _callbaks;
    
    private MovementController _movementController;
    private Animator _animator;
    private LineRenderer _lineRenderer;
    private Transform _startPoint;
    
    private Vector3 _characterPosition;
    private Vector3 _hitPoint;
    private Vector3[] _linePosition = new Vector3[2];
    
    private float _timeLerp;
    private float _cooldownToActivate = 100;
    private bool _goToHitpoint;
    
    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
        _startPoint = GetComponentInChildren<Camera>().transform;
        
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.alignment = LineAlignment.View;

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_goToHitpoint)
        {
            _timeLerp += Time.deltaTime * _grapnelSpeed;
            transform.position = Vector3.Lerp(_characterPosition, _hitPoint, Mathf.Clamp01(_timeLerp));
            
            _linePosition[0] = _lineStart.position;
            _linePosition[1] = _hitPoint;
            _lineRenderer.SetPositions(_linePosition);
            _vfxGrapnel.SetActive(true);
        }
        else
        {
            _vfxGrapnel.SetActive(false);
        }

        if (!_goToHitpoint && _cooldownToActivate < 100)
        {
            _cooldownToActivate += Time.deltaTime * _cooldownSpeed;
        }

        if (Vector3.Distance(transform.position, _hitPoint) < _distanceStopGrap)
        {
            StopGrap();
        }

        if (_cooldownToActivate >= 100)
        {
            _callbaks?.Invoke(true);
        }
    }

    public void Grap()
    {
        if (Physics.Raycast(_startPoint.position, _startPoint.forward, out RaycastHit hit, _distanceToRay, _layerMaskToGrape))
        {
            if (_cooldownToActivate >= 100)
            {
                _hitPoint = hit.point;
                _characterPosition = transform.position;
                _goToHitpoint = true;
                _movementController.IsLocked = true;
                _cooldownToActivate = 0;
                
                _callbaks?.Invoke(false);
                
                _animator.SetBool("GrapnelActive", true);
            }
        }
    }

    public void StopGrap()
    {
        if (_goToHitpoint)
        {
            _goToHitpoint = false;
            _movementController.IsLocked = false;
            _timeLerp = 0;

            _linePosition[0] = _linePosition[1];
            _lineRenderer.SetPositions(_linePosition);
            
            _animator.SetBool("GrapnelActive", false);
        }
    }
}
