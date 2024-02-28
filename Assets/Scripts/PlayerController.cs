using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, FollowTarget
{
    [SerializeField] private Transform _rotateTransform;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private float _speed;
    [SerializeField] private float _radius;
    [Space]
    [SerializeField] private float _followSeconds;

    private Vector2 _direction;
    private Planet _targetPlanet;

    private Coroutine _procces;

    private int _curFollow;
    public float GetSeconds
    {
        get
        {
            _curFollow++;
            return _curFollow * _followSeconds;
        }
    }

    public Transform Transform => _rotateTransform;
    public void SetDirection(Vector2 direction) => _direction = direction;

    public void Init(Planet planet)
    {
        _targetPlanet = planet;
    }

    private void FixedUpdate()
    {
        if (_direction != Vector2.zero)
        {

            _animator.SetBool("IsRun", true);
            _targetPlanet.PlayerMoveTo(_direction * _speed, _radius);
            _rotateTransform.localEulerAngles = new Vector3(0, Vector2.SignedAngle(_direction, Vector2.up), 0);
        }
        else
        {
            _animator.SetBool("IsRun", false);
        }
    }
}
