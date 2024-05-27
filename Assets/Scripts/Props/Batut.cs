using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batut : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            if(player.GetCharacterController().velocity.y <0)
                player.ForceJump(4.5f);
        }
    }
}
