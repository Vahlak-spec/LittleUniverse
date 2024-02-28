using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public FollowController FollowController => _followController;
    public Vector3 Center => transform.position;

    [SerializeField] private Transform _fakePlanetParent;
    [SerializeField] private Transform _fakePlanet;
    [Space]
    [SerializeField] private float _planetRadius;
    [SerializeField] private ObjectOnPlanet[] _objectOnPlanet;
    [SerializeField] private ObjectOnPlanet[] _rendObject;
    [Space]
    [SerializeField] private FollowController _followController;

    private float _l;

    private void Start()
    {
        _colliders = new Collider[10];
    }

    private void OnValidate()
    {
        _l = 2f * _planetRadius * Mathf.PI;
        foreach (var item in _objectOnPlanet)
        {
            item.SetPlanet(this);
        }
    }

    float _d;
    float _angl;

    Vector3 _center;
    Vector2 _newPoint;

    Vector3 _res;

    public Vector3 GetPoint(Vector3 planeCor)
    {
        _res = Vector3.zero;

        _d = planeCor.x;

        while (_d > _l)
        {
            _d -= _l;
        }

        _angl = (_d / (2f * Mathf.PI * _planetRadius)) * 2f * Mathf.PI;

        _newPoint = new Vector2( _planetRadius * Mathf.Cos(_angl), _planetRadius * Mathf.Sin(_angl));
        _res.y = _newPoint.y;

        _d = planeCor.y;

        while (_d > _l)
        {
            _d -= _l;
        }

        _d = ((2 * Mathf.PI * _newPoint.x) * _d) / _l;
        _angl = (_d / (2 * Mathf.PI * _newPoint.x)) * 2f * Mathf.PI;

        _newPoint = new Vector2(_newPoint.x * Mathf.Cos(_angl), _newPoint.x * Mathf.Sin(_angl));

        _res.x = _newPoint.x;
        _res.z = _newPoint.y;

        _res.Normalize();

        return _center + _res * (_planetRadius + planeCor.z);
    }

    private Collider[] _colliders;
    private int _lenth;
    Vector2 _temp;
    public void PlayerMoveTo(Vector2 velocity, float radius)
    {
        _temp = Vector2.zero;
        _temp.x = velocity.y;
        _temp.y = velocity.x;

        _lenth = Physics.OverlapSphereNonAlloc(GetPoint(_temp), radius, _colliders);

        for (int i = 0; i < _lenth; i++)
        {
            if (_colliders[i].GetComponent<Obtacle>())
            {
                return;
            }
        }

        _fakePlanetParent.eulerAngles = Vector3.zero;
        _fakePlanet.localEulerAngles = transform.eulerAngles;

        _fakePlanetParent.localEulerAngles = new Vector3(_fakePlanetParent.localEulerAngles.x, _fakePlanetParent.localEulerAngles.y + velocity.x, _fakePlanetParent.localEulerAngles.z - velocity.y);
        transform.eulerAngles = _fakePlanet.eulerAngles;
    }
}
