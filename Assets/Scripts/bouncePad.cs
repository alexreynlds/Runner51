using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncePad : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == ("Player")){
            Rigidbody otherRB = other.rigidbody;
            otherRB.AddExplosionForce(1000, other.contacts[0].point, 10);
        }
    }
}
