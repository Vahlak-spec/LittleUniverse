using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<InteractItem>(out InteractItem interactItem))
        {
            interactItem.Iteract(this.gameObject);
        }
    }
}
