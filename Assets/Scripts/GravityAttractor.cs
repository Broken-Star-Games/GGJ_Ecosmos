using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    private float gravity;
    private float force;

    
    public void Attract(Transform body, Rigidbody rigidbody) {
        Vector3 gravityUp = (body.position - transform.position).normalized;
        Vector3 bodyLocalUp = body.up;
        
        // float force = rigidbody.mass * this.GetComponent<Rigidbody>().mass  / (this.transform.position - body.position).magnitude;
        
        gravity = 10000 * this.GetComponent<Rigidbody>().mass / ((this.transform.position - body.position).magnitude * (this.transform.position - body.position).magnitude);
        force = rigidbody.mass * gravity;

        //print(gravity);

        //gravity = Gravitational Constant * Mass of planet / Radius^2 (Distance from planet's centre^2) 
        //force = Mass * gravity

        rigidbody.AddForce(gravityUp * -force);

        Quaternion targetRotation = Quaternion.FromToRotation(bodyLocalUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
