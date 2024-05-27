using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBonus : MonoBehaviour, IBonus
{
    public abstract void Consume(Player player);
    private void OnTriggerEnter(Collider collision)
    {

        if (collision.TryGetComponent(out Player player))
        {
            player.ConsumeBonus(this);
            gameObject.SetActive(false);
        }
    }
}
