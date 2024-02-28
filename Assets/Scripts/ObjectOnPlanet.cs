using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnPlanet : MonoBehaviour
{
    [SerializeField] protected Vector3 _cor;

    protected Planet _planet;

    private void OnValidate()
    {
        SetWorldPosition();
    }
    protected void SetWorldPosition()
    {
        if (_planet == null) return;

        transform.position = _planet.GetPoint(_cor);
        transform.rotation = Quaternion.FromToRotation(-transform.up, _planet.Center - transform.position) * transform.rotation;
    }
    public virtual void SetPlanet(Planet planet)
    {
        _planet = planet;
    }
}
