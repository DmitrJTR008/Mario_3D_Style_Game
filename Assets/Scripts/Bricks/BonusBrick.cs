using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBrick : BaseBrick
{
    [SerializeField] private BaseBonus bonus;
    [SerializeField] private ParticleSystem bonusBrickEffect;
    public override void Interact()
    {
        base.Interact();
        Rigidbody rigidbody = bonus.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        bonus.gameObject.SetActive(true);
        rigidbody.AddForce(Vector3.up * 115f);
        bonusBrickEffect.Stop();
    }
}
