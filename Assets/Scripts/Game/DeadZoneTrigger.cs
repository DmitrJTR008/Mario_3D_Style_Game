using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
            player.KillPlayer();
    }
}
