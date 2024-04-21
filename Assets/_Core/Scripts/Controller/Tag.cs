using UnityEngine;

public class Tag : MonoBehaviour
{
    [SerializeField] private float _distance = 100;
    [SerializeField] private LayerMask _mask;

    [SerializeField] private GameObject _tagObject;
    
    private Transform _starPoint;

    private void Awake()
    {
        _starPoint = GetComponentInChildren<Camera>().transform;
    }

    public void DrawTag()
    {
        if (Physics.Raycast(_starPoint.position, _starPoint.forward, out RaycastHit hit, _distance, _mask))
        {
            GameObject tag = Instantiate(_tagObject, hit.point + hit.normal * .5f, Quaternion.identity);
            tag.transform.LookAt(tag.transform.position + -hit.normal * 2);
        }
    }
}
