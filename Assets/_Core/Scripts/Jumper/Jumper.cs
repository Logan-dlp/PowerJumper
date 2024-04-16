using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _force;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MovementController movementController))
        {
            movementController.AddUpwardForce(_force);
        }
    }
}
