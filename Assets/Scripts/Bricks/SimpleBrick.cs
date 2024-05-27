using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBrick : BaseBrick
{
    
}

// Phys Explose Sim

//  float explosionForce = 114f;  
//  float explosionRadius = 5f;   
//  Vector3 explosionPosition = transform.position; 
//
//  for (int i = 0; i < transform.childCount; i++)
//  {
//      Transform chunk = transform.GetChild(i);
//      
//      chunk.gameObject.SetActive(true);
//      Rigidbody rb = chunk.GetComponent<Rigidbody>();
//      if (rb == null)
//      {
//          rb = chunk.gameObject.AddComponent<Rigidbody>();
//      }
//      rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
//  }
//
//  transform.DetachChildren();
//  gameObject.SetActive(false);
