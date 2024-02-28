using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FollowTarget
{
    public Transform Transform { get; }
    public float GetSeconds { get; }
}
