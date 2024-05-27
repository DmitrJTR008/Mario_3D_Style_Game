using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTrigger : MonoBehaviour
{
    public event Action OnCrossFinish;
    private bool doOnce;

    private void OnTriggerEnter(Collider other)
    {
        if (doOnce == true) return;
        if (other.TryGetComponent(out Player player))
        {
            OnCrossFinish?.Invoke();
            doOnce = true;
        }
    }
}
