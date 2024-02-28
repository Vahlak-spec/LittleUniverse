using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovebleObject : ObjectOnPlanet
{
    [SerializeField] private float _speed;
    [Space]
    [SerializeField] private Transform _velocityPoint;
    [SerializeField] private Transform _velocityRotateTransform;

    private static Vector2 _devVelocity = new Vector2(1, 0);

    private Vector2 _cashWorldVelocity;
    private Vector2 _planetMoveVelocity;

    private Vector2 _xVelocity;
    private Vector2 _yVelocity;

    private float _planetL;

    private void Start()
    {
        _xVelocity = new Vector2(1, 0);
        _yVelocity = new Vector2(0, 1);
    }

    public void SetVelocity(Vector2 velocity)
    {
        if (velocity == _cashWorldVelocity) return;

        _cashWorldVelocity = velocity;

        _velocityRotateTransform.localEulerAngles = new Vector3(
            Vector2.Angle(velocity, _devVelocity),
            _velocityRotateTransform.localEulerAngles.y,
            _velocityRotateTransform.localEulerAngles.z);


    }

    private void Update()
    {
        
    }
}
