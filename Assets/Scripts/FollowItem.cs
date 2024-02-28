using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowItem : ObjectOnPlanet, InteractItem
{
    public bool IsFollow
    {
        get;
        set;
    }
    public Transform Transform => _transform;

    [SerializeField] private Transform _transform;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool DevugIt;

    public void SetIsRun(bool value)
    {
        if (DevugIt)
            Debug.Log("IsRun - " + value);

        _animator.SetBool("IsRun", value);
    }

    public void Iteract(GameObject gameObject)
    {
        if(gameObject.TryGetComponent<FollowTarget>(out FollowTarget followTarget))
        {
            SceneController.Instance.FollowController.AddFollow(followTarget, this);
        }
    }
}
