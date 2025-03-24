using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{   
    public GravityAttractor planet;
    private Transform myTransform;
    
    private Rigidbody _rigidbody;

    void Start() {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        _rigidbody.useGravity = false;
        myTransform = transform;
    }
    void FixedUpdate() {
        planet.Attract(myTransform, _rigidbody);
    }
}
