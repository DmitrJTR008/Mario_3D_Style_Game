using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public abstract class BaseBrick : MonoBehaviour , IBrick
{
    private ParticleSystem VfxDestoroy;
    public virtual void Interact()
    {
        VfxDestoroy = transform.GetChild(0).GetComponent<ParticleSystem>();
        transform.DetachChildren();
        VfxDestoroy.Play();
        gameObject.SetActive(false);

    }
}
